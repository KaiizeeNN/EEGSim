namespace EEGSimulator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SecondPage), typeof(SecondPage));
            Routing.RegisterRoute(nameof(ThirdPage), typeof(ThirdPage));
        }
    }
}
