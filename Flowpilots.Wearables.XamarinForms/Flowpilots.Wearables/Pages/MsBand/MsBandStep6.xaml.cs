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
    public partial class MsBandStep6 : ContentPage
    {
        public MsBandStep6()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
