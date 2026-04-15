namespace BigBrotherSRV
{
    public partial class App : Application
    {
        public App()
        {
            Application.Current.UserAppTheme = AppTheme.Dark;

            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage());
        }
    }
}