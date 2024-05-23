namespace EEGSimulator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
