using Android.App;
using Android.Content;
using Android.OS;

namespace Flowpilots.Wearables.Droid
{
    [Activity(Label = "AndroidWearStep1ActivityExtraIntent")]
    public class AndroidWearStep1ActivityExtraIntent : Activity
    {
        private const string EXTRA_VOICE_REPLY = "extra_voice_reply";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.androidWearStep1ActivityExtraIntent);

            //var response = GetMessageText(Intent);
        }

        private string GetMessageText(Intent intent)
        {
            Bundle remoteInput = RemoteInput.GetResultsFromIntent(intent);
            if (remoteInput != null)
            {
                return remoteInput.GetCharSequence(EXTRA_VOICE_REPLY);
            }
            return null;
        }
    }
}