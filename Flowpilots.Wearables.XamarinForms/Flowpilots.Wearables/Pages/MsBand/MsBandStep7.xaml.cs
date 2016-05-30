using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band.Portable;
using Microsoft.Band.Portable.Sensors;
using Microsoft.Band.Portable.Tiles;
using Microsoft.Band.Portable.Tiles.Pages;
using Microsoft.Band.Portable.Tiles.Pages.Data;
using Xamarin.Forms;

namespace Flowpilots.Wearables.Pages.MsBand
{
    public partial class MsBandStep7 : ContentPage
    {
        public MsBandStep7()
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

        // define the element Ids for our tile’s page 
        enum TilePageElementId : short
        {
            Button_PushMe = 1,
        }

        private async void CreateTileWithButton_Click(object sender, EventArgs e)
        {
            // add the layout to the tile     
            if (BandHelper.Instance.BandClient == null)
                await BandHelper.Instance.Connect();

            var tile = await BandHelper.CreateTile("Step 7 Tile");

            // create a filled rectangle to provide the background for a button 
            var panel = new FilledPanel
            {
                Rect = new PageRect(0, 0, 245, 102),
                BackgroundColorSource = ElementColorSource.BandBase
            };

            // add a button to our layout 
            panel.Elements.Add(
                new TextButton
                {
                    ElementId = (short)TilePageElementId.Button_PushMe,
                    Rect = new PageRect(60, 25, 100, 50),
                    PressedColor = new Microsoft.Band.Portable.BandColor(0xFF, 0x00, 0x00),
                });

            // create the page layout 
            var layout = new PageLayout(panel);

            // add the layout to the tile, and add the tile to the Band 
            try
            {
                tile.PageLayouts.Add(layout);
                if (await BandHelper.Instance.BandClient.TileManager.AddTileAsync(tile))
                {
                    // layout and tile added successfully    
                }
                else
                {
                    // tile failed to be added, handle error    
                }
            }
            catch (Exception ex)
            {
                // handle an error adding the layout 
            }

            var pageGuid = Guid.NewGuid();

            // create the content to assign to the page 
            var textButtonData = new TextButtonData
            {
                ElementId = (int)TilePageElementId.Button_PushMe,
                Text = "Push Me!"
            };

            var pageContent = new PageData
            {
                PageId = pageGuid,
                PageLayoutIndex = 0,
                Data = {
                    textButtonData
                }
            };


            // set the page content to the Band 
            try
            {
                await BandHelper.Instance.BandClient.TileManager
                    .SetTilePageDataAsync(tile.Id, pageContent);
            }
            catch (Exception ex)
            {
                // handle a Band connection exception 
            }
        }

        private async void StartListeningToEventsButton_Click(object sender, EventArgs e)
        {
            // Subscribe to events 
            BandHelper.Instance.BandClient.TileManager.TileOpened += TileManagerOnTileOpened;
            BandHelper.Instance.BandClient.TileManager.TileClosed += TileManagerOnTileClosed;
            BandHelper.Instance.BandClient.TileManager.TileButtonPressed += TileManagerOnTileButtonPressed;

            // Start listening for events 
            await BandHelper.Instance.BandClient.TileManager.StartEventListenersAsync();
        }

        void TileManagerOnTileButtonPressed(object sender, BandTileButtonPressedEventArgs bandTileButtonPressedEventArgs)
        {
            // This method is called when the user presses the button in our tile’s layout.    
            //   
            // e.TileEvent.TileId is the tile’s Guid.   
            // e.TileEvent.Timestamp is the DateTimeOffset of the event.   
            // e.TileEvent.PageId is the Guid of our page with the button.    
            // e.TileEvent.ElementId is the value assigned to the button in our layout (i.e.,  TilePageElementId.Button_PushMe). 
            //   
            // handle the event 
        }

        void TileManagerOnTileClosed(object sender, BandTileClosedEventArgs bandTileClosedEventArgs)
        {
            // This method is called when the user exits our Band tile.   
            //   
            // e.TileEvent.TileId is the tile’s Guid.   
            // e.TileEvent.Timestamp is the DateTimeOffset of the event.   
            //   
            // handle the event 
        }

        void TileManagerOnTileOpened(object sender, BandTileOpenedEventArgs bandTileOpenedEventArgs)
        {
            // This method is called when the user taps our Band tile.    
            //   
            // e.TileEvent.TileId is the tile’s Guid.    
            // e.TileEvent.Timestamp is the DateTimeOffset of the event. 
            //
            // handle the event 
        }
    }
}
