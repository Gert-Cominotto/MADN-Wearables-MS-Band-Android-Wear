using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Flowpilots.Wearables.Droid;
using Flowpilots.Wearables.Pages.AndroidWear;
using Java.Lang;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Exception = System.Exception;

[assembly: ExportRenderer(typeof(AndroidWearStep3), typeof(AndroidWearStep3Activity))]

namespace Flowpilots.Wearables.Droid
{
    public class AndroidWearStep3Activity : PageRenderer,
        GoogleApiClient.IOnConnectionFailedListener,
        IResultCallback
    {
        Android.Views.View _view;
        Activity _activity;

        string path = "/start-activity";
        string pathMainActivity = "/start-activity-in-main-activity";
        private static string TAG = "AndroidWearStep3Activity";

        private GoogleApiClient _mGoogleApiClient;
        private IList<INode> _nodes;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var page = e.NewElement as AndroidWearStep3;

            // this is a ViewGroup - so should be able to load an AXML file and FindView<>

            _activity = this.Context as Activity;

            var o = _activity.LayoutInflater.Inflate(Resource.Layout.androidWearStep3Activity, this, false);
            _view = o;

            _mGoogleApiClient = new GoogleApiClient.Builder(Context)
                .AddApi(WearableClass.API)
                .AddOnConnectionFailedListener(this)
                .Build();

            WearableClass.NodeApi.GetConnectedNodes(_mGoogleApiClient)
                .SetResultCallback(this);

            var btnLaunch = _view.FindViewById<Android.Widget.Button>(Resource.Id.btnLaunch);
            btnLaunch.Click += BtnLaunchOnClick;

            var btnLaunchWithData = _view.FindViewById<Android.Widget.Button>(Resource.Id.btnLaunchWithData);
            btnLaunchWithData.Click += BtnLaunchWithDataOnClick;

            var btnLaunchWithDataInMainActivity = _view.FindViewById<Android.Widget.Button>(Resource.Id.btnLaunchWithDataInMainActivity);
            btnLaunchWithDataInMainActivity.Click += BtnLaunchWithDataInMainActivityOnClick;

            AddView(_view);
        }

        private void BtnLaunchWithDataInMainActivityOnClick(object sender, EventArgs e)
        {
            if (!_mGoogleApiClient.IsConnected)
            {
                _mGoogleApiClient.Connect();
            }
            else
            {
                foreach (var node in _nodes)
                    WearableClass.MessageApi.SendMessage(_mGoogleApiClient, node.Id, pathMainActivity, "<DATA HERE MAIN ACTIVITY>".GetBytes())
                        .SetResultCallback(this); 
            }
        }

        private void BtnLaunchWithDataOnClick(object sender, EventArgs e)
        {
            if (!_mGoogleApiClient.IsConnected)
            {
                _mGoogleApiClient.Connect();
            }
            else
            {
                foreach (var node in _nodes)
                    WearableClass.MessageApi.SendMessage(_mGoogleApiClient, node.Id, path, "<DATA HERE>".GetBytes())
                        .SetResultCallback(this); 
            }
        }

        private void BtnLaunchOnClick(object sender, EventArgs eventArgs)
        {
            if (!_mGoogleApiClient.IsConnected)
            {
                _mGoogleApiClient.Connect();
            }
            else
            {
                foreach (var node in _nodes)
                    WearableClass.MessageApi.SendMessage(_mGoogleApiClient, node.Id, path, new byte[0])
                        .SetResultCallback(this); 
            }
        }


        public void OnConnectionFailed(ConnectionResult connectionResult)
        {
            Log.Error(TAG, "Failed to connect to Google Api Client");
        }
        
        //Since Java bytecode does not support generics, casting between types can be messy. Handle everything here.
        public void OnResult(Java.Lang.Object raw)
        {
            Exception nodeException, messageException;
            try
            {
                // Get the message that was send
                var nodeResult = raw.JavaCast<INodeApiGetConnectedNodesResult>();

                _nodes = nodeResult.Nodes;
                foreach (var node in _nodes)
                    WearableClass.MessageApi.SendMessage(_mGoogleApiClient, node.Id, path, new byte[0])
                        .SetResultCallback(this); //will go to second try/catch block
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