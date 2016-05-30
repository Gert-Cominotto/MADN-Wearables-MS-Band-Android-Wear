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
    public partial class MsBandStep5 : ContentPage
    {
        public MsBandStep5()
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

        private async void CreateLayout1Button_Click(object sender, EventArgs e)
        {
            // Create a scrollable vertical panel that will hold 2 text messages. 
            var panel = new ScrollFlowPanel
            {
                Rect = new PageRect(0, 0, 245, 102),
                Orientation = FlowPanelOrientation.Vertical,
                ScrollBarColorSource = ElementColorSource.BandBase
            };

            // add the text block to contain the first message 
            panel.Elements.Add(new WrappedTextBlock
            {
                ElementId = (short)TileMessagesLayoutElementId.Message1,
                Rect = new PageRect(0, 0, 245, 102),
                // left, top, right, bottom margins         
                Margins = new Margins(15, 0, 15, 0),
                Color = new Microsoft.Band.Portable.BandColor(0xFF, 0xFF, 0xFF),
                Font = WrappedTextBlockFont.Small
            });

            // add the text block to contain the second message 
            panel.Elements.Add(new WrappedTextBlock
            {
                ElementId = (short)TileMessagesLayoutElementId.Message2,
                Rect = new PageRect(0, 0, 245, 102),
                // left, top, right, bottom margins         
                Margins = new Margins(15, 0, 15, 0),
                Color = new Microsoft.Band.Portable.BandColor(0xFF, 0xFF, 0xFF),
                Font = WrappedTextBlockFont.Small
            }
                );

            // create the page layout 
            PageLayout layout = new PageLayout(panel);

            try
            {
                // add the layout to the tile     
                if (BandHelper.Instance.BandClient == null)
                    await BandHelper.Instance.Connect();

                var tile = await BandHelper.CreateTile("Step5 Tile - Layout 1");

                // get the current set of tiles 
                tile.PageLayouts.Add(layout);

                try
                {     // add the tile to the Band     
                    if (await BandHelper.Instance.BandClient.TileManager.AddTileAsync(tile))
                    {
                        // tile was successfully added     
                        // can proceed to set tile content with SetPagesAsync    
                    }
                    else {
                        // tile failed to be added, handle error    
                    }
                }
                catch (Exception ex)
                {
                    // handle a Band connection exception 
                }

                // specify which layout to use for this page  
                var tbd1 = new WrappedTextBlockData
                {
                    ElementId = (Int16)TileMessagesLayoutElementId.Message1,
                    Text = "This is the text of the first message"
                };

                var tbd2 = new WrappedTextBlockData
                {
                    ElementId = (Int16)TileMessagesLayoutElementId.Message2,
                    Text = "This is the text of the second message"
                };

                // create a new Guid for the messages page 
                var messagesPageGuid = Guid.NewGuid();
                // create the object that contains the page content to be set 
                var pageContent = new PageData
                {
                    PageId = messagesPageGuid,
                    PageLayoutIndex = (int)TileLayoutIndex.MessagesLayout,
                    Data = { tbd1, tbd2 }
                };

                try
                {     // set the page content on the Band     
                    await BandHelper.Instance.BandClient.TileManager
                        .SetTilePageDataAsync(tile.Id, pageContent);

                }
                catch (Exception ex)
                {
                    // handle a Band connection exception 
                }

            }
            catch (Exception ex)
            {
                // handle an error adding the layout }
            }
        }


        private async void CreateLayout2Button_Click(object sender, EventArgs e)
        {
            if (BandHelper.Instance.BandClient == null)
                await BandHelper.Instance.Connect();

            // We'll create a Tile that looks like this:
            // +--------------------+
            // | MY CARD            | 
            // | |||||||||||||||||  | 
            // | 123456789          |
            // +--------------------+

            // First, we'll prepare the layout for the Tile page described above.
            TextBlock myCardTextBlock = new TextBlock()
            {
                Color = new Microsoft.Band.Portable.BandColor(0xFF, 0xFF, 0xFF),
                ElementId = 1, // the Id of the TextBlock element; we'll use it later to set its text to "MY CARD"
                Rect = new PageRect(0, 0, 200, 25)
            };
            Barcode barcode = new Barcode(BarcodeType.Code39)
            {
                ElementId = 2, // the Id of the Barcode element; we'll use it later to set its barcode value to be rendered
                Rect = new PageRect(0, 0, 250, 50)
            };
            TextBlock digitsTextBlock = new TextBlock()
            {
                ElementId = 3, // the Id of the TextBlock element; we'll use it later to set its text to "123456789"
                Rect = new PageRect(0, 0, 200, 25)
            };
            FlowPanel panel = new FlowPanel
            {
                Orientation = FlowPanelOrientation.Vertical,
                Rect = new PageRect(0, 0, 250, 100),
                Elements = { myCardTextBlock, barcode, digitsTextBlock }
            };


            var myTile = await BandHelper.CreateTile("Step5 Tile - Layout 2");
            myTile.PageLayouts.Add(new PageLayout(panel));

            // Remove the Tile from the Band, if present. An application won't need to do this everytime it runs. 
            // But in case you modify this sample code and run it again, let's make sure to start fresh.

            try
            {
                await BandHelper.Instance.BandClient.TileManager.RemoveTileAsync(myTile.Id);
            }
            catch (Exception exception)
            {

            }
            // Create the Tile on the Band.
            await BandHelper.Instance.BandClient.TileManager.AddTileAsync(myTile);

            // And create the page with the specified texts and values.

            var tbd1 = new TextBlockData
            {
                ElementId = myCardTextBlock.ElementId,
                Text = "MY CARD"
            };
            var bd = new BarcodeData
            {
                ElementId = barcode.ElementId,
                BarcodeValue = "123456789"
            };
            var tbd2 = new TextBlockData
            {
                ElementId = digitsTextBlock.ElementId,
                Text = "123456789"
            };

            PageData page = new PageData
            {
                PageId = Guid.NewGuid(),
                PageLayoutIndex = 0,
                Data = { tbd1, bd, tbd2 }
            };


            await BandHelper.Instance.BandClient.TileManager.SetTilePageDataAsync(myTile.Id, page);
        }


        // Define symbolic constants for indexes to each layout that 
        // the tile has. The index of the first layout is 0. Because only 
        // 5 layouts are allowed, the max index value is 4.
        internal enum TileLayoutIndex { MessagesLayout = 0, };

        // Define symbolic constants to uniquely (in MessagesLayout) 
        // identify each of the elements of our layout 
        // that contain content that the app will set 
        // (that is, these Ids will be used when calling APIs 
        // to set the page content). 
        internal enum TileMessagesLayoutElementId : short
        {
            Message1 = 1,
            // Id for the 1st message text block     
            Message2 = 2,
            // Id for the 2nd message text block 
        };
    }
}
