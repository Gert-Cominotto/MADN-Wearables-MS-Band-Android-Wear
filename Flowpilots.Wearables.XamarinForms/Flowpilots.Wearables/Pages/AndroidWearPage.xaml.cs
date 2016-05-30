using System;
using Flowpilots.Wearables.Pages.AndroidWear;
using Flowpilots.Wearables.Pages.MsBand;
using Xamarin.Forms;

namespace Flowpilots.Wearables.Pages
{
	public partial class AndroidWearPage : ContentPage
	{
		public AndroidWearPage()
		{
			InitializeComponent ();
		}

        private async void Step1_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AndroidWearStep1());
        }

        private async void Step2_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AndroidWearStep2());
        }

        private async void Step3_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AndroidWearStep3());
        }

        private async void StepX_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AndroidWearStepX());
        }
    }
}

