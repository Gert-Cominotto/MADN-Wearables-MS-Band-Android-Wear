using System;
using Xamarin.Forms;

namespace Flowpilots.Wearables.Pages
{
	public class MsBandPageCS : ContentPage
	{
		public MsBandPageCS()
        {
   //         Title = "Microsoft Band";

   //         var button = new Button {
			//	Text = "Upcoming Appointments",
			//	VerticalOptions = LayoutOptions.CenterAndExpand
			//};
			//button.Clicked += OnUpcomingAppointmentsButtonClicked;
            
			//Content = new StackLayout {
			//	Children = {
			//		new Label {
			//			Text = "This week's appointments go here",
			//			HorizontalOptions = LayoutOptions.Center,
			//			VerticalOptions = LayoutOptions.CenterAndExpand
			//		},
			//		button
			//	}	
			//};
		}

		async void OnUpcomingAppointmentsButtonClicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new UpcomingAppointmentsPage ());
		}
	}
}
