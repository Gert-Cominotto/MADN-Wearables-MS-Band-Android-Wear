using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band.Portable;
using Microsoft.Band.Portable.Sensors;
using Microsoft.Band.Portable.Tiles;
using Xamarin.Forms;

namespace Flowpilots.Wearables.Pages.MsBand
{
    public partial class MsBandStep4 : ContentPage
    {
        public MsBandStep4()
        {
            InitializeComponent();
            BindingContext = this;
        }
        
        #region Properties

        private ObservableCollection<BandTile> _bandTiles;
        public ObservableCollection<BandTile> BandTiles
        {
            get { return _bandTiles; }
            set { if (_bandTiles == value) return; _bandTiles = value; OnPropertyChanged(); }
        }
        
        #endregion


        private async void GetListOfApplicationTilesButton_Click(object sender, EventArgs e)
        {
            if (BandHelper.Instance.BandClient == null)
                await BandHelper.Instance.Connect();

            // get the current set of tiles 
            var tiles = await BandHelper.Instance.BandClient.TileManager.GetTilesAsync();
            BandTiles = new ObservableCollection<BandTile>(tiles);
        }


        private async void CheckIfThereIsSpaceForMoreTilesOnTheBandButton_Click(object sender, EventArgs e)
        {
            if (BandHelper.Instance.BandClient == null)
                await BandHelper.Instance.Connect();

            // determine the number of available tile slots on the Band
            var remainingTileCap = await BandHelper.Instance.BandClient.TileManager.GetRemainingTileCapacityAsync();

            await DisplayAlert("Remaining Tile Capacity", remainingTileCap.ToString(), "OK");
        }

        private async void CreateTileButton_Click(object sender, EventArgs e)
        {
            if (BandHelper.Instance.BandClient == null)
                await BandHelper.Instance.Connect();

            var tile = await BandHelper.CreateTile("Step4 Tile");

            // add the tile to the Band
            if (await BandHelper.Instance.BandClient.TileManager.AddTileAsync(tile))
            {
                // Reset
                _tiles = null;

                // do work if the tile was successfully created
                await DisplayAlert("Tile was added on the band!", string.Empty, "OK");
            }
        }

        private async void RemoveAllTilesButton_Click(object sender, EventArgs e)
        {
            if (BandHelper.Instance.BandClient == null)
                await BandHelper.Instance.Connect();

            // get the current set of tiles 
            foreach (var t in await GetAllTiles())
            {
                // remove the tile from the Band
                try
                {
                    await BandHelper.Instance.BandClient.TileManager.RemoveTileAsync(t.Id);

                    // do work if the tile was successfully removed
                    await DisplayAlert("All my tiles were removed on the band!", string.Empty, "OK");
                }
                catch (Exception)
                {
                    await DisplayAlert("Fail!", string.Empty, "OK");
                }
            }

            // Reset
            _tiles = null;
        }

        private async void SendNotificationButton_Click(object sender, EventArgs e)
        {
            if (BandHelper.Instance.BandClient == null)
                await BandHelper.Instance.Connect();

            // get the current set of tiles 
            foreach (var t in await GetAllTiles())
            {
                await BandHelper.Instance.BandClient.NotificationManager.ShowDialogAsync(t.Id, "Dialog title", $"Dialog body {t.Id}");
            }
            // send a dialog to the Band for one of our tiles
        }

        private async void SendMessageButton_Click(object sender, EventArgs e)
        {
            if (BandHelper.Instance.BandClient == null)
                await BandHelper.Instance.Connect();

            // get the current set of tiles 
            foreach (var t in await GetAllTiles())
            {
                // Send a message to the Band for one of our tiles, and show it as a dialog.
                await BandHelper.Instance.BandClient.NotificationManager.SendMessageAsync(t.Id,
                 $"Message title {Guid.NewGuid()}", $"Message body {Guid.NewGuid()}", DateTime.Now, true);
            }
        }

        private async void SendMessageWithoutDialogButton_Click(object sender, EventArgs e)
        {
            if (BandHelper.Instance.BandClient == null)
                await BandHelper.Instance.Connect();

            // get the current set of tiles 
            foreach (var t in await GetAllTiles())
            {
                // Send a message to the Band for one of our tiles, and show it as a dialog.
                await BandHelper.Instance.BandClient.NotificationManager.SendMessageAsync(t.Id,
                 $"Message title {Guid.NewGuid()}", $"Message body {Guid.NewGuid()}", DateTime.Now, false);
            }
        }

        private List<BandTile> _tiles;
        private async Task<IEnumerable<BandTile>> GetAllTiles()
        {
            if (_tiles == null)
            {
                var tiles = await BandHelper.Instance.BandClient.TileManager.GetTilesAsync();
                _tiles = tiles.ToList();
            }

            return _tiles;
        }
    }
}
