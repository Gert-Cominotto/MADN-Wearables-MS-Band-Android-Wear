using System;
using Microsoft.Band.Portable;
using Microsoft.Band.Portable.Sensors;
using Xamarin.Forms;

namespace Flowpilots.Wearables.Pages.MsBand
{
    public partial class MsBandStep3 : ContentPage
    {
        public MsBandStep3()
        {
            InitializeComponent();
            BindingContext = this;
        }

        #region Properties

        private UserConsent _userConcent;
        public UserConsent UserConsent
        {
            get { return _userConcent; }
            set { if (_userConcent == value) return; _userConcent = value; OnPropertyChanged(); }
        }

        private BandAccelerometerReading _bandAccelerometerReading;
        public BandAccelerometerReading BandAccelerometerReading
        {
            get { return _bandAccelerometerReading; }
            set { if (_bandAccelerometerReading == value) return; _bandAccelerometerReading = value; OnPropertyChanged(); }
        }

        #endregion


        private async void StartAccelerometerSensor_Click(object sender, EventArgs e)
        {
            if (BandHelper.Instance.BandClient == null)
            {
                await BandHelper.Instance.Connect();
            }

            // hook up to the Accelerometer sensor ReadingChanged event 
            BandHelper.Instance.BandClient.SensorManager.Accelerometer.ReadingChanged += (s, a) =>
            {
                // do work when the reading changes (i.e., update a UI element) 
                BandAccelerometerReading = a.SensorReading;
            };


            // start the Heartrate sensor 
            try
            {
                await BandHelper.Instance.BandClient.SensorManager.Accelerometer.StartReadingsAsync();
            }
            catch (Exception ex)
            {
                // handle a Band connection exception     
                throw ex;
            }
        }
    }
}
