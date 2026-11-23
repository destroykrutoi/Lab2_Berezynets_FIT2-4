namespace StudentCabinet;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        UserAppTheme = AppTheme.Light;
        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);
        window.Title = "Особистий кабінет студента";
        window.Width = 520;
        window.Height = 860;
        return window;
    }
}
