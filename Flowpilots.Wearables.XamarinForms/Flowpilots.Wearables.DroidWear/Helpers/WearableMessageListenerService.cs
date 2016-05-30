using Android.App;
using Android.Content;
using Android.Gms.Wearable;
using Android.Support.V4.Content;
using Android.Util;

namespace Flowpilots.Wearables.Droid.Helpers
{
    [Service]
    [IntentFilter(new string[] { "com.google.android.gms.wearable.BIND_LISTENER" })]
    public class WearableMessageListenerService : WearableListenerService
    {
        private static readonly string START_ACTIVITY_PATH = "/start-activity";
        private static readonly string START_ACTIVITY_PATH_IN_MAIN_ACTIVITY = "/start-activity-in-main-activity";

        public override void OnMessageReceived(IMessageEvent messageEvent)
        {
            if (messageEvent.Path.Equals(START_ACTIVITY_PATH))
            {
                string message = messageEvent.GetData().GetString();

                if (string.IsNullOrWhiteSpace(message))
                {
                    var launchPage = new Intent(this, typeof(LaunchedFromHandheldActivity));
                    launchPage.AddFlags(ActivityFlags.NewTask);
                    StartActivity(launchPage);
                }
                else
                {
                    // Start a new activity
                    var launchPage = new Intent(this, typeof(LaunchedFromHandheldWithDataActivity));
                    launchPage.PutExtra("WearMessage", message);
                    launchPage.AddFlags(ActivityFlags.NewTask);
                    StartActivity(launchPage);
                }
            }
            else if (messageEvent.Path.Equals(START_ACTIVITY_PATH_IN_MAIN_ACTIVITY))
            {
                string message = messageEvent.GetData().GetString();

                // Broadcast message to wearable activity for display
                var messageIntent = new Intent();
                messageIntent.SetAction(Intent.ActionSend);
                messageIntent.PutExtra("message", message);
                LocalBroadcastManager.GetInstance(this).SendBroadcast(messageIntent);
            }
            else {
                base.OnMessageReceived(messageEvent);
            }
        }
    }
}