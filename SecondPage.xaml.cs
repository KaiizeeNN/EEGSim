using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEGSimulator
{
    public partial class SecondPage : ContentPage
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            // Start button click handling
            DisplayAlert("Start", "Start button clicked", "OK");
        }

        private void OnStopClicked(object sender, EventArgs e)
        {
            // Stop button click handling
            DisplayAlert("Stop", "Stop button clicked", "OK");
        }
    }
}
