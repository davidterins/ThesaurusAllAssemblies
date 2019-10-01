using Prism.Ioc;
using Prism.Unity;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using WPFClient.ViewModels;
using WPFClient.Views;

namespace WPFClient
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication
  {
    public static HttpClient Client { get; private set; }

    public override void Initialize()
    {
      base.Initialize();
      InitializeClient();
    }

    /// <summary>
    /// Initializes the <see cref="HttpClient"/> for the application.
    /// </summary>
    private static void InitializeClient()
    {
      Client = new HttpClient();
      Client.DefaultRequestHeaders.Accept.Clear();
      Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    protected override Window CreateShell()
    {
      return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.RegisterDialog<HelloWorldDialogView, HelloWorldDialogViewModel>();
    }
  }
}
