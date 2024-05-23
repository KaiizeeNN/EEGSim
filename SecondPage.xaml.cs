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

        private void OnSendClicked(object sender, EventArgs e)
        {
            // Start button click handling
            DisplayAlert("Send", "Send button clicked", "OK");
        }

    }
}
