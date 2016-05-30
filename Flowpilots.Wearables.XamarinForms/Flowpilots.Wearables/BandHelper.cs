using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Band.Portable;
using Microsoft.Band.Portable.Tiles;

namespace Flowpilots.Wearables
{
    public class BandHelper
    {
        public static BandHelper Instance { get; private set; }
        static BandHelper() { Instance = new BandHelper(); }

        public BandClient BandClient { get; set; }
        public string BandClientName { get; set; }

        public async Task<List<BandDeviceInfo>> GetPairedBandsAsync()
        {
            var bands = await BandClientManager.Instance.GetPairedBandsAsync();
            return bands.ToList();
        }

        public async Task Connect(BandDeviceInfo selectedBandToConnectWith = null)
        {
            var pairedBands = await GetPairedBandsAsync();
            try
            {
                if (selectedBandToConnectWith != null)
                {
                    BandClient = await BandClientManager.Instance.ConnectAsync(selectedBandToConnectWith);
                    BandClientName = selectedBandToConnectWith.Name;
                }
                else if (pairedBands != null && pairedBands.Any())
                {
                    BandClient = await BandClientManager.Instance.ConnectAsync(pairedBands.First());
                    BandClientName = pairedBands.First().Name;
                }

                // do work after successful connect

            }
            catch (Exception ex)
            {
                // handle a Band connection exception
                throw new CouldntConnectToBandException();
            }
        }


        public static async Task<BandTile> CreateTile(string tileName)
        {
            var assembly = typeof(Flowpilots.Wearables.App).GetTypeInfo().Assembly;

            BandImage smallIcon;
            using (var stream = assembly.GetManifestResourceStream("Flowpilots.Wearables.Assets.BandLogo-24x24.png"))
            {
                smallIcon = await BandImage.FromStreamAsync(stream);
            }

            BandImage tileIcon;
            using (var stream = assembly.GetManifestResourceStream("Flowpilots.Wearables.Assets.BandLogo-46x46.png"))
            {
                tileIcon = await BandImage.FromStreamAsync(stream);
            }

            // create a new Guid for the tile
            var tileGuid = Guid.NewGuid();

            // create a new tile with a new Guid
            var tile = new BandTile(tileGuid)
            {
                // enable badging (the count of unread messages)
                //IsBadgingEnabled = true,
                // set the name
                Name = tileName,
                // set the icons
                SmallIcon = smallIcon,
                Icon = tileIcon
            };
            return tile;
        }
    }


    public class CouldntConnectToBandException : Exception
    {
    }
}