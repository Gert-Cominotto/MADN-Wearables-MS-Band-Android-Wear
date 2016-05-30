using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;

namespace Flowpilots.Wearables.Droid
{
    [Activity(
        Label = "Step1Activity",
        Icon = "@drawable/icon",
        NoHistory = false,
        StateNotNeeded = true)]
    public class Step1Activity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Step1);


            // Get our button from the layout resource,
            // and attach an event to it
            var button = FindViewById<Button>(Resource.Id.myButton);

            int count = 0;
            button.Click += delegate
            {
                var notification = new NotificationCompat.Builder(this)
                    .SetContentTitle("Button tapped")
                    .SetContentText("Button tapped " + count++ + " times!")
                    .SetSmallIcon(Android.Resource.Drawable.StatNotifyVoicemail)
                    .SetGroup("group_key_demo").Build();

                var manager = NotificationManagerCompat.From(this);
                manager.Notify(1, notification);
                button.Text = "Check Notification!";
            };
        }
    }
}


