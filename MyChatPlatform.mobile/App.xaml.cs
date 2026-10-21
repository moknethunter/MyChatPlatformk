public partial class App : Application
{
    private readonly MainPage _mainPage;
    public App(MainPage page)
    {
        InitializeComponent();
        _mainPage = page;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new NavigationPage(_mainPage));
    }
}
