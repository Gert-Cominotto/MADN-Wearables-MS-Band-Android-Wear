using Android.App;
using Android.OS;

namespace Flowpilots.Wearables.Droid
{
    [Activity(Label = "NotificationWithExtraActivity", 
        Exported = true, 
        AllowEmbedded = true, 
        TaskAffinity = "",
        Theme = "@android:style/Theme.DeviceDefault.Light" )]
    public class NotificationWithExtraActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NotificationWithExtraActivity);
        }
    }
}