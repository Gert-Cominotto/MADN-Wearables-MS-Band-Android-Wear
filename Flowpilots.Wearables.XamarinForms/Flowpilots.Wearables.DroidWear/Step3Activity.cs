using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.OS;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Java.Interop;
using Android.Views.Animations;
using Android.Widget;
using Flowpilots.Wearables.Droid.Helpers;

namespace Flowpilots.Wearables.Droid
{
    [Activity(
        Label = "Step3Activity",
        Icon = "@drawable/icon",
        NoHistory = true,
        StateNotNeeded = true)]
    public class Step3Activity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Step2);
            
            // Build intent for notification content
            var pendingIntent = new Intent(this, typeof(MainActivity));
            pendingIntent.PutExtra("EXTRA_EVENT_ID", 2);
            var viewPendingIntent = PendingIntent.GetActivity(this, 0, pendingIntent, 0);

            var notificationIntent = new Intent(this, typeof(NotificationWithExtraActivity));
            var notificationPendingIntent = PendingIntent.GetActivity(
                    this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);

            var builder = new NotificationCompat.Builder(this);
            builder.SetContentTitle("Notification")
                .SetContentText("With Custom Wearable Display Activity")
                .SetLocalOnly(false)
                .SetSmallIcon(Resource.Drawable.ic_full_sad)
                .SetContentIntent(viewPendingIntent)
                .Extend(
                    new NotificationCompat.WearableExtender()
                        .SetDisplayIntent(notificationPendingIntent));

            NotificationManagerCompat.From(this).Notify(201, builder.Build());
        }
    }
}


