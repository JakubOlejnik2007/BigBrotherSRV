using Microsoft.Maui.Controls;

namespace BigBrotherSRV
{
    public partial class App : Application
    {
        public App()
        {
            Application.Current.UserAppTheme = AppTheme.Dark;

            InitializeComponent();

            MainPage = new MainPage();

            StartServer();
            StartUDP();
        }
        private async void StartUDP()
        {

            UdpServer server = new UdpServer();
            await Task.Run(() => server.StartAsync());
        }
        private async void StartServer()
        {
            TcpServer server = new TcpServer();
            await Task.Run(() => server.StartAsync());
        }
    }
}