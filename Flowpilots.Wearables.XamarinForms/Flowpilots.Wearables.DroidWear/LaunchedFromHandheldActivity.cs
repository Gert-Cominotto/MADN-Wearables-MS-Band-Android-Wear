using Android.App;
using Android.OS;

namespace Flowpilots.Wearables.Droid
{
    [Activity(Label = "LaunchedFromHandheldActivity", 
        Exported = true, 
        AllowEmbedded = true, 
        TaskAffinity = "",
        Theme = "@android:style/Theme.DeviceDefault.Light" )]
    public class LaunchedFromHandheldActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LaunchedFromHandheld);
        }
    }
}