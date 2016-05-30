using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace Flowpilots.Wearables.Droid
{
    [Activity(Label = "LaunchedFromHandheldWithDataActivity",
        Exported = true,
        AllowEmbedded = true,
        TaskAffinity = "",
        Theme = "@android:style/Theme.DeviceDefault.Light")]
    public class LaunchedFromHandheldWithDataActivity : Activity
    {
        static TextView _textView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LaunchedFromHandheldWithExtraData);

            _textView = FindViewById<TextView>(Resource.Id.textViewExtraData);
            if (_textView == null)
            {
                Log.Error("FlowPilots", "_textView is null");
            }
            else
            {
                Log.Error("FlowPilots", "_textView is NOT null");

                var message = Intent.GetStringExtra("WearMessage");
                if (string.IsNullOrEmpty(message))
                {
                    Log.Error("FlowPilots", "message is null");
                    }
                else
                {
                    Log.Error("FlowPilots", "message is NOT null");
                    _textView.Text = message;
                }
            }

        }
    }
}