//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.Gms.Common.Apis;
//using Android.Gms.Wearable;
//using Android.OS;
//using Android.Runtime;
//using Android.Util;
//using Android.Views;
//using Android.Widget;

//namespace Flowpilots.Wearables.Droid
//{
//    [Service]
//    [IntentFilter(new[] { "com.google.android.gms.wearable.BIND_LISTENER" })]
//    public class WearService : WearableListenerService
//    {
//        Handler handler;
//        public override void OnCreate()
//        {
//            base.OnCreate();
//            this.handler = new Handler();
//            Log.Error("WearIntegration", "Created");
//        }

//        public override void OnMessageReceived(IMessageEvent messageEvent)
//        {
//            base.OnMessageReceived(messageEvent);

//            HandleMessage(messageEvent);
//        }

//        async void HandleMessage(IMessageEvent message)
//        {
//            try
//            {
//                Log.Error("WearIntegration", "Received Message");
//                var client = new GoogleApiClient.Builder(this)
//                  .AddApi(WearableClass.API)
//                  .Build();

//                var result = client.BlockingConnect(30, Java.Util.Concurrent.TimeUnit.Seconds);
//                if (!result.IsSuccess)
//                    return;

//                var path = message.Path;

//                try
//                {

//                    //if (path.StartsWith(TweetsPath))
//                    //{

//                    //    var viewModel = new TwitterViewModel();

//                    //    await viewModel.ExecuteLoadTweetsCommand();

//                    //    var request = PutDataMapRequest.Create(TweetsPath + "/Answer");
//                    //    var map = request.DataMap;

//                    //    var tweetMap = new List<DataMap>();
//                    //    foreach (var tweet in viewModel.Tweets)
//                    //    {
//                    //        var itemMap = new DataMap();

//                    //        itemMap.PutLong("CreatedAt", tweet.CreatedAt.Ticks);
//                    //        itemMap.PutString("ScreenName", tweet.ScreenName);
//                    //        itemMap.PutString("Text", tweet.Text);

//                    //        tweetMap.Add(itemMap);
//                    //    }
//                    //    map.PutDataMapArrayList("Tweets", tweetMap);
//                    //    map.PutLong("UpdatedAt", DateTime.UtcNow.Ticks);

//                    //    await WearableClass.DataApi.PutDataItem(client, request.AsPutDataRequest());
//                    //}
//                }
//                finally
//                {
//                    client.Disconnect();
//                }
//            }
//            catch (Exception e)
//            {
//                Android.Util.Log.Error("WearIntegration", e.ToString());
//            }
//        }
//    }
//}