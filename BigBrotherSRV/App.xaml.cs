namespace BigBrotherSRV
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            StartServer();
        }

        private async void StartServer()
        {
            TcpServer server = new TcpServer();
            await Task.Run(() => server.StartAsync());
        }
    }
}