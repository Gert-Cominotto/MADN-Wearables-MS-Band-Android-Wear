using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.OS;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Util;
using Java.Interop;
using Android.Views.Animations;
using Android.Widget;
using Adapter = Flowpilots.Wearables.Droid.Helpers.Adapter;

namespace Flowpilots.Wearables.Droid
{
    [Activity(
        Label = "Flowpilots.Wearables.Droid",
        MainLauncher = true,
        Icon = "@drawable/icon",
        NoHistory = true,
        StateNotNeeded = true)]
    public class MainActivity : Activity, WearableListView.IClickListener
    {
        // Sample dataset for the list
        string[] _elements = {
            "Step 1 - Simple Example",
            "Step 2 - Wear-specific Notification"
        };

        static string[] _elements2 = {
            "Step 1 - Simple Example",
            "Step 2 - Wear-specific Notification",
            "Step 3 - Extra data has been added!"
        };

        static WearableListView _listView;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Log.Error("Flowpilots", "Main Activity Started");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _listView = FindViewById<WearableListView>(Resource.Id.wearable_list);
            // Assign an adapter to the list
            var adapter = new Adapter(this, _elements);
            
            _listView.SetAdapter(adapter);

            // Set a click listener
            _listView.SetClickListener(this);
            
            #region Step 3
            // Register the local broadcast receiver, defined in step 3.
            var messageFilter = new IntentFilter(Intent.ActionSend);
            var messageReceiver = new MessageReceiver();
            LocalBroadcastManager.GetInstance(this).RegisterReceiver(messageReceiver, messageFilter);

            #endregion
        }

        public class MessageReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
                var message = intent.GetStringExtra("message");
                _elements2[2] = message;

                // Assign an adapter to the list
                var adapter = new Adapter(context, _elements2);
                _listView.SwapAdapter(adapter, true);
            }
        }

        public void OnClick(WearableListView.ViewHolder v)
        {
            var tag = (int)v.ItemView.Tag;
            Intent intent = null;

            switch (tag)
            {
                case 0:
                    intent = new Intent(this, typeof(Step1Activity));
                    break;
                case 1:
                    intent = new Intent(this, typeof(Step2Activity));
                    break;
            }

            StartActivity(intent);
        }

        public void OnTopEmptyRegionClick()
        {
            throw new NotImplementedException();
        }
    }
}


