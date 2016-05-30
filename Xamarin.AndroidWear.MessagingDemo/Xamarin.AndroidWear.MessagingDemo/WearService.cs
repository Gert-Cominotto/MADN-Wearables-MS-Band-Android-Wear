using System.Linq;
using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using Android.Support.V4.Content;
using JavaObjectExtensions = Java.Interop.JavaObjectExtensions;

namespace Xamarin.AndroidWear.MessagingDemo
{
    [Service]
    [IntentFilter(new[] { "com.google.android.gms.wearable.BIND_LISTENER" })]
    public class WearService : WearableListenerService
    {
        const string MessagePath = "/MadnDemo/Data";
        GoogleApiClient _googleApiClient;

        public override void OnCreate()
        {
            base.OnCreate();
            _googleApiClient = new GoogleApiClient.Builder(this.ApplicationContext)
                    .AddApi(WearableClass.API)
                    .Build();

            _googleApiClient.Connect();
        }

        public override void OnDataChanged(DataEventBuffer dataEvents)
        {
            var dataEvent = Enumerable.Range(0, dataEvents.Count)
                                      .Select(i => JavaObjectExtensions.JavaCast<IDataEvent>(dataEvents.Get(i)))
                                      .FirstOrDefault(x => x.Type == DataEvent.TypeChanged && x.DataItem.Uri.Path.Equals(MessagePath));
            if (dataEvent == null)
                return;

            //get data from wearable
            var dataMapItem = DataMapItem.FromDataItem(dataEvent.DataItem);
            var message = dataMapItem.DataMap.GetString("Message");

            var intent = new Intent();
            intent.SetAction(Intent.ActionSend);
            intent.PutExtra("WearMessage", message);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
        }
    }
}