using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.Wearable.Views;
using Android.Util;
using Android.Views;
using Android.Widget;
using Flowpilots.Wearables.Droid;
using Flowpilots.Wearables.Pages.AndroidWear;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AndroidWearStepX), typeof(AndroidWearStepXActivity))]

namespace Flowpilots.Wearables.Droid
{
    public class AndroidWearStepXActivity : PageRenderer,
        DelayedConfirmationView.IDelayedConfirmationListener,
        GoogleApiClient.IOnConnectionFailedListener,
        IResultCallback
    {
        Android.Views.View _view;
        Activity _activity;
        
        private GoogleApiClient _mGoogleApiClient;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var page = e.NewElement as AndroidWearStep1;

            // this is a ViewGroup - so should be able to load an AXML file and FindView<>

            _activity = this.Context as Activity;

            var o = _activity.LayoutInflater.Inflate(Resource.Layout.androidWearStep1Activity, this, false);
            _view = o;

            _mGoogleApiClient = new GoogleApiClient.Builder(this.Context)
                .AddApi(WearableClass.API)
                .AddOnConnectionFailedListener(this)
                .Build();

            var button = _view.FindViewById<Android.Widget.Button>(Resource.Id.btnConnect);
            button.Click += (sender, args) =>
            {
                if (!_mGoogleApiClient.IsConnected)
                {
                    _mGoogleApiClient.Connect();

                    WearableClass.NodeApi.GetConnectedNodes(_mGoogleApiClient).SetResultCallback(this);
                }
            };

            AddView(_view);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);
            _view.Measure(msw, msh);
            _view.Layout(0, 0, r - l, b - t);
        }

        
        /**
	     * Starts the DelayedConfirmationView when user presses "Start Timer" button.
	     */
        public void OnStartTimer(Android.Views.View view)
        {
            //delayedConfirmationView.Start();
            //delayedConfirmationView.SetListener(this);
        }

        public void OnTimerSelected(Android.Views.View v)
        {
            //v.Pressed = true;
            //Notification notification = new NotificationCompat.Builder(this)
            //    .SetSmallIcon(Resource.Drawable.ic_launcher)
            //    .SetContentTitle(GetString(Resource.String.notification_title))
            //    .SetContentText(GetString(Resource.String.notification_timer_selected))
            //    .Build();
            //NotificationManagerCompat.From(this).Notify(0, notification);
            //SendMessageToCompanion(TIMER_SELECTED_PATH);
            //// Prevent onTimerFinished from being heard.
            //((DelayedConfirmationView)v).SetListener(null);
            //Finish();
        }

        public void OnTimerFinished(Android.Views.View v)
        {
            //Notification notification = new NotificationCompat.Builder(this)
            //    .SetSmallIcon(Resource.Drawable.ic_launcher)
            //    .SetContentTitle(GetString(Resource.String.notification_title))
            //    .SetContentText(GetString(Resource.String.notification_timer_finished))
            //    .Build();
            //NotificationManagerCompat.From(this).Notify(0, notification);
            //SendMessageToCompanion(TIMER_FINISHED_PATH);
            //Finish();
        }

        public void OnConnectionFailed(ConnectionResult connectionResult)
        {
            //Log.Error(TAG, "Failed to connect to Google Api Client");
        }

        string path;
        private static string TAG = "DelayedConfirmation";
        private void SendMessageToCompanion(string path)
        {
            this.path = path;
            //WearableClass.NodeApi.GetConnectedNodes(_mGoogleApiClient).SetResultCallback(this);
        }

        //Since Java bytecode does not support generics, casting between types can be messy. Handle everything here.
        public void OnResult(Java.Lang.Object raw)
        {
            Exception nodeException, messageException;
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

                foreach (var node in nodeResult.Nodes)
                    WearableClass.MessageApi.SendMessage(_mGoogleApiClient, node.Id, path, new byte[0]).SetResultCallback(this); //will go to second try/catch block
                return;
            }
            catch (Exception e)
            {
                nodeException = e;
            }
            try
            {
                //check that it worked correctly
                var messageResult = raw.JavaCast<IMessageApiSendMessageResult>();
                if (!messageResult.Status.IsSuccess)
                    Log.Error(TAG, "Failed to connect to Google Api Client with status "
                    + messageResult.Status);
                return;
            }
            catch (Exception e)
            {
                messageException = e;
            }
            //Will never get here
            Log.Warn(TAG, "Unexpected type for OnResult");
            Log.Error(TAG, "Node Exception", nodeException);
            Log.Error(TAG, "Message Exception", messageException);
        }
    }
}