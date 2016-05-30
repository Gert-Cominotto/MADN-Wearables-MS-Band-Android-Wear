using System;
using Flowpilots.Wearables.Pages.MsBand;
using Xamarin.Forms;

namespace Flowpilots.Wearables.Pages
{
	public partial class MsBandPage : ContentPage
	{
		public MsBandPage()
		{
			InitializeComponent ();
		}
        
	    private async void Step1_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MsBandStep1());
        }
        
	    private async void Step2_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MsBandStep2());
        }
        
	    private async void Step3_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MsBandStep3());
        }
        
	    private async void Step4_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MsBandStep4());
        }
        
	    private async void Step5_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MsBandStep5());
        }
        
	    private async void Step6_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MsBandStep6());
        }
        
	    private async void Step7_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MsBandStep7());
        }
	}
}

