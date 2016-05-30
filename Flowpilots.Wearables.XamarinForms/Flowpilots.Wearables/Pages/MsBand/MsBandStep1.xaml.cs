using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band;
using Microsoft.Band.Portable;
using Xamarin.Forms;

namespace Flowpilots.Wearables.Pages.MsBand
{
    public partial class MsBandStep1 : ContentPage, INotifyPropertyChanged
    {
        #region Properties

        private ObservableCollection<BandDeviceInfo> _bandDeviceInfos;
        public ObservableCollection<BandDeviceInfo> BandDeviceInfos
        {
            get { return _bandDeviceInfos; }
            set { if (_bandDeviceInfos == value) return; _bandDeviceInfos = value; OnPropertyChanged(); }
        }

        private BandDeviceInfo _selectedBandDeviceInfo;
        public BandDeviceInfo SelectedBandDeviceInfo
        {
            get { return _selectedBandDeviceInfo; }
            set { if (_selectedBandDeviceInfo == value) return; _selectedBandDeviceInfo = value; OnPropertyChanged(); }
        }

        private BandClient _connectedBandClient;
        public BandClient ConnectedBandClient
        {
            get { return _connectedBandClient; }
            set { if (_connectedBandClient == value) return; _connectedBandClient = value; OnPropertyChanged(); }
        }

        private string _isConnected;
        public string IsConnected
        {
            get { return _isConnected; }
            set { if (_isConnected == value) return; _isConnected = value; OnPropertyChanged(); }
        }

        private string _firmwareVersion;
        public string FirmwareVersion
        {
            get { return _firmwareVersion; }
            set { if (_firmwareVersion == value) return; _firmwareVersion = value; OnPropertyChanged(); }
        }

        private string _hardwareVersion;
        public string HardwareVersion
        {
            get { return _hardwareVersion; }
            set { if (_hardwareVersion == value) return; _hardwareVersion = value; OnPropertyChanged(); }
        }

        #endregion

        public MsBandStep1()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async void GetListOfPairedBandButton_Click(object sender, EventArgs e)
        {
            var clients = await BandHelper.Instance.GetPairedBandsAsync();
            BandDeviceInfos = new ObservableCollection<BandDeviceInfo>(clients);
        }

        public async void ConnectToSelectedBandButton_Click(object sender, EventArgs e)
        {
            await BandHelper.Instance.Connect(SelectedBandDeviceInfo);
            ConnectedBandClient = BandHelper.Instance.BandClient;

            IsConnected = $"IsConnected: {ConnectedBandClient.IsConnected}";
            FirmwareVersion = $"FirmwareVersion: {await ConnectedBandClient.GetFirmwareVersionAsync()}";
            HardwareVersion = $"HardwareVersion: {await ConnectedBandClient.GetHardwareVersionAsync()}";
        }

        public async void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (ConnectedBandClient != null)
            {
                await ConnectedBandClient.DisconnectAsync();
                IsConnected = "IsConnected:";
                FirmwareVersion = "FirmwareVersion:";
                HardwareVersion = "HardwareVersion:";
            }
        }
    }
}
