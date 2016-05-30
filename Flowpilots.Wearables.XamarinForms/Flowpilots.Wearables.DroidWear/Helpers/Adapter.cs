using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Support.Wearable.Views;
using Android.Views;
using Android.Widget;

namespace Flowpilots.Wearables.Droid.Helpers
{
    public class Adapter : RecyclerView.Adapter
    {
        private string[] mDataset;
        private Context mContext;
        private LayoutInflater mInflater;

        // Provide a suitable constructor (depends on the kind of dataset)
        public Adapter(Context context, String[] dataset)
        {
            mContext = context;
            mInflater = LayoutInflater.From(context);
            mDataset = dataset;
        }

        // Provide a reference to the type of views you're using
        public class ItemViewHolder : WearableListView.ViewHolder
        {
            private TextView textView;
            public ItemViewHolder(View itemView) : base(itemView)
            {
                // find the text view within the custom item's layout
                textView = (TextView)itemView.FindViewById(Resource.Id.name);
            }
        }

        // Replace the contents of a list item
        // Instead of creating new views, the list tries to recycle existing ones
        // (invoked by the WearableListView's layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            // retrieve the text view
            var itemHolder = (ItemViewHolder)holder;
            var view = (TextView)itemHolder.ItemView;
            // replace text contents
            var text = mDataset[position];
            view.Text = text;
            // replace list item's metadata
            holder.ItemView.Tag = position;
        }

        // Create new views for list items
        // (invoked by the WearableListView's layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate our custom layout for list items
            return new ItemViewHolder(mInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null));
        }

        // Return the size of your dataset
        // (invoked by the WearableListView's layout manager)
        public override int ItemCount
        {
            get
            {
                return mDataset.Length;
            }
        }
    }
}