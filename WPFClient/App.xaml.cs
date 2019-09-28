using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;

namespace WPFClient
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public static HttpClient Client { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      InitializeClient();

    }

    private static void InitializeClient()
    {
      Client = new HttpClient();
      Client.DefaultRequestHeaders.Accept.Clear();
      Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
  }
}
