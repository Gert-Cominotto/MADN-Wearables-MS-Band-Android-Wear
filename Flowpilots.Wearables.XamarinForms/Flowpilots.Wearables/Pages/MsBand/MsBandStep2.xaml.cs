using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band.Portable;
using Microsoft.Band.Portable.Sensors;
using Xamarin.Forms;

namespace Flowpilots.Wearables.Pages.MsBand
{
    public partial class MsBandStep2 : ContentPage, INotifyPropertyChanged
    {
        #region Properties

        private UserConsent _userConcent;
        public UserConsent UserConsent
        {
            get { return _userConcent; }
            set { if (_userConcent == value) return; _userConcent = value; OnPropertyChanged(); }
        }

        private BandHeartRateReading _bandHeartRateReading;
        public BandHeartRateReading BandHeartRateReading
        {
            get { return _bandHeartRateReading; }
            set { if (_bandHeartRateReading == value) return; _bandHeartRateReading = value; OnPropertyChanged(); }
        }

        #endregion

        public MsBandStep2()
        {
            InitializeComponent();
            BindingContext = this;
        }


        private async void GetConsentButton_Click(object sender, EventArgs e)
        {
            if (BandHelper.Instance.BandClient == null)
            {
                await BandHelper.Instance.Connect();

                UserConsent = BandHelper.Instance.BandClient.SensorManager.HeartRate.UserConsented;
            }

            UserConsent = BandHelper.Instance.BandClient.SensorManager.HeartRate.UserConsented;
            // check current user heart rate consent 
            if (UserConsent != UserConsent.Granted)
            {
                // user hasn’t consented, request consent  
                await BandHelper.Instance.BandClient.SensorManager.HeartRate.RequestUserConsent();
            }

            // hook up to the Heartrate sensor ReadingChanged event 
            BandHelper.Instance.BandClient.SensorManager.HeartRate.ReadingChanged += (s, a) =>
            {
                BandHeartRateReading = a.SensorReading;
            };
        }

        private async void StartHeartRateSensor_Click(object sender, EventArgs e)
        {
            // start the Heartrate sensor 
            try
            {
                await BandHelper.Instance.BandClient.SensorManager.HeartRate.StartReadingsAsync();
            }
            catch (Exception ex)
            {
                // handle a Band connection exception     
                throw ex;
            }
        }
    }
}
