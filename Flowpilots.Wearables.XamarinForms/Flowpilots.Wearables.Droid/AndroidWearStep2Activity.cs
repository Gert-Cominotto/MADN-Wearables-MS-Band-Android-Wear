using System;
using System.Diagnostics;
using System.Linq;
using Android.App;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using Android.OS;
using Android.Runtime;
using Android.Support.Wearable.Views;
using Android.Util;
using Android.Views;
using Android.Widget;
using Flowpilots.Wearables.Droid;
using Flowpilots.Wearables.Pages.AndroidWear;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AndroidWearStep2), typeof(AndroidWearStep2Activity))]

namespace Flowpilots.Wearables.Droid
{
    public class AndroidWearStep2Activity : PageRenderer,
        GoogleApiClient.IOnConnectionFailedListener,
        GoogleApiClient.IConnectionCallbacks,
        IResultCallback
    {
        Android.Views.View _view;
        Activity _activity;

        string path;
        private static string TAG = "Step2";

        private GoogleApiClient _mGoogleApiClient;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var page = e.NewElement as AndroidWearStep2;

            // this is a ViewGroup - so should be able to load an AXML file and FindView<>

            _activity = this.Context as Activity;

            var o = _activity.LayoutInflater.Inflate(Resource.Layout.androidWearStep2Activity, this, false);
            _view = o;

            _mGoogleApiClient = new GoogleApiClient.Builder(this.Context)
                .AddApi(WearableClass.API)
                .AddOnConnectionFailedListener(this)
                .AddConnectionCallbacks(this)
                .Build();

            WearableClass.NodeApi.GetConnectedNodes(_mGoogleApiClient)
                .SetResultCallback(this);

            var button = _view.FindViewById<Android.Widget.Button>(Resource.Id.btnConnect);
            button.Click += (sender, args) =>
            {
                if (!_mGoogleApiClient.IsConnected)
                {
                    _mGoogleApiClient.Connect();
                }
            };

            AddView(_view);
        }

        public void OnConnectionFailed(ConnectionResult connectionResult)
        {
            Log.Error(TAG, "Failed to connect to Google Api Client");
        }
        
        //Since Java bytecode does not support generics, casting between types can be messy. Handle everything here.
        public void OnResult(Java.Lang.Object raw)
        {
            try
            {
                // Get the message that was send
                var nodeResult = raw.JavaCast<INodeApiGetConnectedNodesResult>();

                // Get all of the selected Nodes 
                var list = nodeResult.Nodes.Select(x => x.DisplayName).ToList();
                var listAdapter = new ArrayAdapter<string>(
                    Context, Android.Resource.Layout.SimpleListItem1,
                    list);

                // Show the list of Nodes in the UI
                var listview = _view.FindViewById<Android.Widget.ListView>(Resource.Id.listViewConnectedDevices);
                listview.Adapter = listAdapter;
            }
            catch (Exception e)
            {
                Debugger.Break();
            }
        }

        public void OnConnected(Bundle connectionHint)
        {
        }

        public void OnConnectionSuspended(int cause)
        {

        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);
            _view.Measure(msw, msh);
            _view.Layout(0, 0, r - l, b - t);
        }
    }
}