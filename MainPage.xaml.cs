using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;

namespace EEGSimulator
{
    public partial class MainPage : ContentPage
    {
        IAdapter adapter = null;
        IService service = null;
        ICharacteristic characteristicUpdate = null;
        ICharacteristic characteristicWrite = null;

        string deviceId = "00000000-0000-0000-0000-546c0e594305";
        string serviceId = "0000fff0-0000-1000-8000-00805f9b34fb";
        string characteristicUpdateId = "0000fff4-0000-1000-8000-00805f9b34fb";
        string characteristicWriteId = "0000fff1-0000-1000-8000-00805f9b34fb";
        public MainPage()
        {
            InitializeComponent();

            RequestPermissions();

            adapter = CrossBluetoothLE.Current.Adapter;

            adapter.DeviceConnected += async (s, e) =>
            {
                MainThread.BeginInvokeOnMainThread(() => SetStatusText("Connected."));

                MainThread.BeginInvokeOnMainThread(() => SetButtonText(btnConnectDisconnect, "Disconnect"));
            };

            adapter.DeviceDisconnected += async (s, e) =>
            {
                MainThread.BeginInvokeOnMainThread(() => SetStatusText("Disconnected."));

                MainThread.BeginInvokeOnMainThread(() => SetButtonText(btnConnectDisconnect, "Connect"));
            };

            var ble = CrossBluetoothLE.Current;

            var state = ble.State;

            ble.StateChanged += (s, e) =>
            {
                if(ble.State == BluetoothState.Off)
                {
                    lblError.Text = "Bluetooth off.";
                    DisplayAlert("Alert!", "Bluetooth is off. Turn it on.", "OK");
                }
                
                if(ble.State == BluetoothState.On)
                {
                    lblError.Text = "Bluetooth on.";
                }
            };
        }

        private void SetStatusText(string status)
        {
            lblError.Text = status;
        }
        private async void RequestPermissions()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
        }
        
        private void SetButtonText(Button btn, string text)
        {
            btn.Text = text;
        }
        private async void ConnectDevice()
        {
            try
            {
                var ble = CrossBluetoothLE.Current;

                if (ble.State == BluetoothState.On)
                {
                    var adapter = CrossBluetoothLE.Current.Adapter;
                    adapter.ScanTimeout = 5000;

                    var permissionStatus = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();

                    if (permissionStatus == PermissionStatus.Granted)
                    {
                        lblError.Text = "Searching for device...";

                        CancellationToken cancel = new CancellationToken();

                        adapter.ConnectToKnownDeviceAsync(Guid.Parse(deviceId), new ConnectParameters(true), cancel);
                    }
                    else
                    {
                        var st = await Permissions.RequestAsync<Permissions.Bluetooth>();
                    }
                } 
                else
                {
                    lblError.Text = "Bluetooth is not on";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private async Task<bool> FindService()
        {
            bool ok = false;

            try
            {
                MainThread.BeginInvokeOnMainThread(() => SetStatusText("Finding correct service..."));

                CancellationToken cancel = new CancellationToken();

                service = await adapter.ConnectedDevices[0].GetServiceAsync(Guid.Parse(deviceId),cancel);

                if(service == null)
                {
                    MainThread.BeginInvokeOnMainThread(() => SetStatusText("Service not found..."));
                    ok = false;
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => SetStatusText("Service found..."));
                    ok = true;
                }
            }
            catch (Exception e)
            {
                MainThread.BeginInvokeOnMainThread(() => SetStatusText(e.Message));

            }

            return ok;
        }
        private void DisconnectDevice()
        {
            try
            {
                //if(btnStartStop.Text.Equals("Stop"))
                //{
                //    StopUpdates();
                //}

                lblError.Text = "Attempting to disconnect...";

                adapter.DisconnectDeviceAsync(adapter.ConnectedDevices[0]);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private async void OnConnectClicked(object sender, EventArgs e)
        {
            // Navigate to SecondPage
            ConnectDevice();
        }
        private async void OnGoToSecondPageClicked(object sender, EventArgs e)
        {
            // Navigate to SecondPage
            await Navigation.PushAsync(new SecondPage());
        }

        private async void OnGoToThirdPageClicked(object sender, EventArgs e)
        {
            // Navigate to ThirdPage
            await Navigation.PushAsync(new ThirdPage());
        }
    }
}
