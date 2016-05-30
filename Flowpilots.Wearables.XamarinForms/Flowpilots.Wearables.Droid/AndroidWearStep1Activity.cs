using System;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Views;
using Flowpilots.Wearables.Droid;
using Flowpilots.Wearables.Pages.AndroidWear;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AndroidWearStep1), typeof(AndroidWearStep1Activity))]

namespace Flowpilots.Wearables.Droid
{
    public class AndroidWearStep1Activity : PageRenderer
    {
        private static string TAG = "AndroidWearStep1Activity";

        Android.Views.View view;
        Activity activity;

        public const string BothPath = "/both";
        public const string WatchOnlyPath = "/watch-only";
        public const string KeyNotificationId = "notification-id";
        public const string KeyTitle = "title";
        public const string KeyContent = "content";

        public const string ActionDismiss = "com.example.android.wearable.synchronizednotifications.DISMISS";

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var page = e.NewElement as AndroidWearStep1;

            // this is a ViewGroup - so should be able to load an AXML file and FindView<>

            activity = this.Context as Activity;

            var o = activity.LayoutInflater.Inflate(Resource.Layout.androidWearStep1Activity, this, false);
            view = o;

            var btnSendLocalNotification = view.FindViewById<Android.Widget.Button>(Resource.Id.btnSendLocalNotification);
            btnSendLocalNotification.Click += btnSendLocalNotification_OnClick;

            var btnSendNotificationToBoth = view.FindViewById<Android.Widget.Button>(Resource.Id.btnSendNotificationToBoth);
            btnSendNotificationToBoth.Click += btnSendNotificationToBoth_OnClick;

            var btnSendNotificationWithExtraIntent = view.FindViewById<Android.Widget.Button>(Resource.Id.btnSendNotificationWithExtraIntent);
            btnSendNotificationWithExtraIntent.Click += btnSendNotificationWithExtraIntent_OnClick;

            var btnSendNotificationWithExtraAction = view.FindViewById<Android.Widget.Button>(Resource.Id.btnSendNotificationWithExtraAction);
            btnSendNotificationWithExtraAction.Click += btnSendNotificationWithExtraAction_OnClick;

            var btnSendNotificationWithWearableOnlyaAction = view.FindViewById<Android.Widget.Button>(Resource.Id.btnSendNotificationWithWearableOnlyaAction);
            btnSendNotificationWithWearableOnlyaAction.Click += btnSendNotificationWithWearableOnlyaAction_OnClick;

            var btnSendNotificationWithBigText = view.FindViewById<Android.Widget.Button>(Resource.Id.btnSendNotificationWithBigText);
            btnSendNotificationWithBigText.Click += btnSendNotificationWithBigText_OnClick;

            var btnSendNotificationWithInboxStyle = view.FindViewById<Android.Widget.Button>(Resource.Id.btnSendNotificationWithInboxStyle);
            btnSendNotificationWithInboxStyle.Click += btnSendNotificationWithInboxStyle_OnClick;

            var btnSendNotificationWithVoiceInput = view.FindViewById<Android.Widget.Button>(Resource.Id.btnSendNotificationWithVoiceInput);
            btnSendNotificationWithVoiceInput.Click += btnSendNotificationWithVoiceInput_OnClick;

            var btnSendNotificationWithMultiplePages = view.FindViewById<Android.Widget.Button>(Resource.Id.btnSendNotificationWithMultiplePages);
            btnSendNotificationWithMultiplePages.Click += btnSendNotificationWithMultiplePages_OnClick;

            AddView(view);
        }

        private void btnSendLocalNotification_OnClick(object sender, EventArgs eventArgs)
        {
            var builder = new NotificationCompat.Builder(Context);
            builder.SetContentTitle("Local")
                .SetContentText("Notification")
                .SetLocalOnly(true)
                .SetSmallIcon(Resource.Drawable.icon2);

            NotificationManagerCompat.From(Context).Notify(111, builder.Build());
        }

        private void btnSendNotificationToBoth_OnClick(object sender, EventArgs eventArgs)
        {
            var builder = new NotificationCompat.Builder(Context);
            builder.SetContentTitle("Local and Wear")
                .SetContentText("Notification")
                .SetLocalOnly(false)
                .SetSmallIcon(Resource.Drawable.icon3);

            NotificationManagerCompat.From(Context).Notify(112, builder.Build());
        }

        private void btnSendNotificationWithExtraIntent_OnClick(object sender, EventArgs eventArgs)
        {
            // Build intent for notification content
            var pendingIntent = new Intent(Context, typeof(AndroidWearStep1ActivityExtraIntent));
            pendingIntent.PutExtra("EXTRA_EVENT_ID", 2);
            var viewPendingIntent = PendingIntent.GetActivity(Context, 0, pendingIntent, 0);

            var builder = new NotificationCompat.Builder(Context);
            builder.SetContentTitle("Notification")
                .SetContentText("With Extra Intent")
                .SetLocalOnly(false)
                .SetSmallIcon(Resource.Drawable.icon4)
                .SetContentIntent(viewPendingIntent);

            NotificationManagerCompat.From(Context).Notify(113, builder.Build());
        }

        private void btnSendNotificationWithExtraAction_OnClick(object sender, EventArgs eventArgs)
        {
            // Build intent for notification content
            var pendingIntent = new Intent(Context, typeof(AndroidWearStep1ActivityExtraIntent));
            pendingIntent.PutExtra("EXTRA_EVENT_ID", 2);
            var viewPendingIntent = PendingIntent.GetActivity(Context, 0, pendingIntent, 0);

            // Build action intent
            var mapIntent = new Intent(Intent.ActionView);
            mapIntent.SetData(Android.Net.Uri.Parse("geo:51.2195163,4.4156713,17"));
            var actionPendingIntent = PendingIntent.GetActivity(Context, 0, mapIntent, 0);


            var builder = new NotificationCompat.Builder(Context);
            builder.SetContentTitle("Notification")
                .SetContentText("With Extra Action")
                .SetLocalOnly(false)
                .SetSmallIcon(Resource.Drawable.icon5)
                .SetContentIntent(viewPendingIntent)
                .AddAction(Resource.Drawable.icon5action, "Etra Action Title", actionPendingIntent);

            NotificationManagerCompat.From(Context).Notify(114, builder.Build());
        }

        private void btnSendNotificationWithWearableOnlyaAction_OnClick(object sender, EventArgs eventArgs)
        {
            // Build intent for notification content
            var pendingIntent = new Intent(Context, typeof(AndroidWearStep1ActivityExtraIntent));
            pendingIntent.PutExtra("EXTRA_EVENT_ID", 2);
            var viewPendingIntent = PendingIntent.GetActivity(Context, 0, pendingIntent, 0);

            // Build action intent
            var mapIntent = new Intent(Intent.ActionView);
            mapIntent.SetData(Android.Net.Uri.Parse("geo:51.2195163,4.4156713,17"));
            var actionPendingIntent = PendingIntent.GetActivity(Context, 0, mapIntent, 0);

            // Build wearable only action intent
            var wearableMapIntent = new Intent(Intent.ActionView);
            wearableMapIntent.SetData(Android.Net.Uri.Parse("geo:22.280260, 114.173885"));
            var wearableActionPendingIntent = PendingIntent.GetActivity(Context, 0, wearableMapIntent, 0);


            var builder = new NotificationCompat.Builder(Context);
            builder.SetContentTitle("Notification")
                .SetContentText("With Extra Action")
                .SetLocalOnly(false)
                .SetSmallIcon(Resource.Drawable.icon6)
                .SetContentIntent(viewPendingIntent)
                .AddAction(Resource.Drawable.icon6action, "Etra Action Title", actionPendingIntent)
                .Extend(
                    new NotificationCompat.WearableExtender()
                        .AddAction(new NotificationCompat.Action(Resource.Drawable.icon6action, "Wearable Only", wearableActionPendingIntent)));

            NotificationManagerCompat.From(Context).Notify(115, builder.Build());
        }



        private void btnSendNotificationWithBigText_OnClick(object sender, EventArgs eventArgs)
        {
            // Specify the 'big view' content to display the long
            var bigStyle = new NotificationCompat.BigTextStyle();
            bigStyle.BigText("MUCH MUCH MUCH MUCH MUCH MORE MORE MORE MORE MORE MORE MORE MORE MORE MORE MORE MORE MORE TEXT!!");

            var builder = new NotificationCompat.Builder(Context);
            builder.SetContentTitle("Notification")
                .SetContentText("With Big Text")
                .SetLocalOnly(false)
                .SetSmallIcon(Resource.Drawable.icon7)
                .SetStyle(bigStyle);

            NotificationManagerCompat.From(Context).Notify(116, builder.Build());
        }


        private void btnSendNotificationWithInboxStyle_OnClick(object sender, EventArgs eventArgs)
        {
            // Specify the 'big view' content to display the long
            var inboxStyle = new NotificationCompat.InboxStyle();
            inboxStyle
                .AddLine("Line1")
                .AddLine("Line2")
                .AddLine("Line3")
                .AddLine("Line4")
                .SetBigContentTitle("Big Content Tilte")
                .SetSummaryText("+3 more");


            var builder = new NotificationCompat.Builder(Context);
            builder.SetContentTitle("Notification")
                .SetContentText("With Inbox Style")
                .SetLocalOnly(false)
                .SetSmallIcon(Resource.Drawable.icon8)
                .SetStyle(inboxStyle);

            NotificationManagerCompat.From(Context).Notify(117, builder.Build());
        }

        private const string EXTRA_VOICE_REPLY = "extra_voice_reply";

        private void btnSendNotificationWithVoiceInput_OnClick(object sender, EventArgs eventArgs)
        {
            // Key for the string that's delivered in the action's intent
            var replyChoices = new string[] { "Choice 1", "Choice 2" };
            var remoteInput = new Android.Support.V4.App.RemoteInput.Builder(EXTRA_VOICE_REPLY)
                    .SetLabel("Reply")
                    .SetChoices(replyChoices)
                    .Build();


            // Build intent for notification content
            var pendingIntent = new Intent(Context, typeof(AndroidWearStep1ActivityExtraIntent));
            pendingIntent.PutExtra("EXTRA_EVENT_ID", 2);
            var viewPendingIntent = PendingIntent.GetActivity(Context, 0, pendingIntent, PendingIntentFlags.UpdateCurrent);

            // Create the reply action and add the remote input
            var action = new NotificationCompat.Action.Builder(Resource.Drawable.ic_media_play, "Reply!", viewPendingIntent)
                            .AddRemoteInput(remoteInput)
                            .Build();

            // Build the notification and add the action via WearableExtender
            var notification = new NotificationCompat.Builder(Context)
                            .SetSmallIcon(Resource.Drawable.icon9)
                            .SetContentTitle("Notification")
                            .SetContentText("With Voice Input Action")
                            .Extend(new NotificationCompat.WearableExtender().AddAction(action))
                            .Build();


            NotificationManagerCompat.From(Context).Notify(118, notification);
        }



        private void btnSendNotificationWithMultiplePages_OnClick(object sender, EventArgs eventArgs)
        {
            // Specify the 'big view' content to display the long
            var bigStyle = new NotificationCompat.BigTextStyle();
            bigStyle.BigText("MUCH MUCH MUCH MUCH MUCH MORE MORE MORE MORE MORE MORE MORE MORE MORE MORE MORE MORE MORE TEXT!!");

            var builder = new NotificationCompat.Builder(Context);
            builder.SetContentTitle("Notification")
                .SetContentText("With Big Text")
                .SetLocalOnly(false)
                .SetSmallIcon(Resource.Drawable.icon10)
                .SetStyle(bigStyle);

            // Create a big text style for the second page
            var secondPageStyle = new NotificationCompat.BigTextStyle();
            secondPageStyle.SetBigContentTitle("Page 2")
                           .BigText("A lot of text...");


            // Create second page notification
            Notification secondPageNotification = new NotificationCompat.Builder(Context)
                    .SetStyle(secondPageStyle)
                    .Build();

            // Extend the notification builder with the second page
            var notification = builder
                    .Extend(new NotificationCompat.WearableExtender()
                            .AddPage(secondPageNotification))
                    .Build();


            NotificationManagerCompat.From(Context).Notify(119, notification);
        }


        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);
            view.Measure(msw, msh);
            view.Layout(0, 0, r - l, b - t);
        }
    }
}