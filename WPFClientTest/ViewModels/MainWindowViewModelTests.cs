using Moq;
using NUnit.Framework;
using Prism.Services.Dialogs;
using WPFClient.ViewModels;
using WPFClient.Views;

namespace WPFClientTest.ViewModels
{
  public class MainWindowViewModelTests
  {
    private Mock<IDialogService> mockDialogService;

    [SetUp]
    public void Setup()
    {
      mockDialogService = new Mock<IDialogService>();
    }

    [Test]
    public void OpenMessageBoxTest()
    {
      var viewModel = new MainWindowViewModel(mockDialogService.Object);
      mockDialogService.Setup(s => s.ShowDialog(typeof(HelloWorldDialogView).Name, null, null));

      viewModel.OpenHelloWorldDialogCommand.Execute();

      mockDialogService.VerifyAll();
    }
  }
}