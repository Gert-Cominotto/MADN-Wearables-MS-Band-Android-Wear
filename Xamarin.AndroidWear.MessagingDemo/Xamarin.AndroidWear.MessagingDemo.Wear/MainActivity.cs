using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Java.Interop;
using Android.Views.Animations;

namespace Xamarin.AndroidWear.MessagingDemo
{
    [Activity(Label = "Xamarin.AndroidWear.MessagingDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity,
        GoogleApiClient.IConnectionCallbacks,
        GoogleApiClient.IOnConnectionFailedListener,
        IDataApiDataListener
    {
        private const string TAG = "MyTag";
        private GoogleApiClient _googleApiClient;
        const string MessagePath = "/MadnDemo/Data";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _googleApiClient = new GoogleApiClient.Builder(this)
                             .AddApi(WearableClass.API)
                             .Build();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var v = FindViewById<WatchViewStub>(Resource.Id.watch_view_stub);
            v.LayoutInflated += delegate
            {

                // Get our button from the layout resource,
                // and attach an event to it
                Button button = FindViewById<Button>(Resource.Id.myButton);

                button.Click += delegate
                {
                    SendData();
                };
            };
        }

        public void SendData()
        {
            //Things to keep in mind:
            // *The path should always starts with forward-slash(/).
            // * Timestamps is a must when sending data because the OnDataChanged() event only gets called when the data really changes.
            //   Adding the Timestamp to the data will make sure that the method gets called.
            var request = PutDataMapRequest.Create(MessagePath);
            var map = request.DataMap;
            map.PutString("Message", "Hello from Wearable!");
            map.PutLong("UpdatedAt", DateTime.UtcNow.Ticks);
            WearableClass.DataApi.PutDataItem(_googleApiClient, request.AsPutDataRequest());
        }

        protected override void OnStart()
        {
            base.OnStart();
            _googleApiClient.Connect();
        }
        public void OnConnected(Bundle p0)
        {
            WearableClass.DataApi.AddListener(_googleApiClient, this);
        }

        public void OnConnectionSuspended(int reason)
        {
            Android.Util.Log.Error(TAG, "GMSonnection suspended " + reason);
            //WearableClass.DataApi.RemoveListener(_googleApiClient, this);
        }

        public void OnConnectionFailed(Android.Gms.Common.ConnectionResult result)
        {
            Android.Util.Log.Error(TAG, "GMSonnection failed " + result.ErrorCode);
        }

        protected override void OnStop()
        {
            base.OnStop();
            _googleApiClient.Disconnect();
        }

        public void OnDataChanged(DataEventBuffer dataEvents)
        {
            var dataEvent = Enumerable.Range(0, dataEvents.Count)
                                      .Select(i => JavaObjectExtensions.JavaCast<IDataEvent>(dataEvents.Get(i)))
                                      .FirstOrDefault(x => x.Type == DataEvent.TypeChanged && x.DataItem.Uri.Path.Equals(MessagePath));

            if (dataEvent == null)
                return;

            //do stuffs here
        }
    }
}


