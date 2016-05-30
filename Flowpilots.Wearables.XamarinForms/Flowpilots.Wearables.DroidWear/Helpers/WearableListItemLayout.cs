using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.Wearable.Views;
using Android.Util;
using Android.Widget;

namespace Flowpilots.Wearables.Droid.Helpers
{
    public class WearableListItemLayout : LinearLayout, WearableListView.IOnCenterProximityListener
    {

        private ImageView mCircle;
        private TextView mName;

        private readonly float mFadedTextAlpha;
        private readonly int mFadedCircleColor;
        private readonly int mChosenCircleColor;


        public WearableListItemLayout(Context context) : base(context, null)
        {
        }

        public WearableListItemLayout(Context context, IAttributeSet attrs) : base(context, attrs, 0)
        {
        }

        public WearableListItemLayout(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            mFadedTextAlpha = 100f;
            mFadedCircleColor = Resource.Color.grey;
            mChosenCircleColor = Resource.Color.blue;
        }

        protected override void OnFinishInflate()
        {
            base.OnFinishInflate();
            // These are defined in the layout file for list items
            // (see next section)
            mCircle = (ImageView)FindViewById(Resource.Id.circle);
            mName = (TextView)FindViewById(Resource.Id.name);
        }

        public void OnCenterPosition(bool p0)
        {
            mName.Alpha = 1f;
            ((GradientDrawable)mCircle.Drawable).SetColor(mChosenCircleColor);

        }

        public void OnNonCenterPosition(bool p0)
        {
            ((GradientDrawable)mCircle.Drawable).SetColor(mFadedCircleColor);
            mName.Alpha = mFadedTextAlpha;
        }
    }
}