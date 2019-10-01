using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prism.Services.Dialogs;
using WPFClient.Views;

namespace WPFClient.ViewModels
{
  public class MainWindowViewModel : BindableBase
  {
    private readonly IDialogService dialogService;
    private string inputText;
    private string selectedSynonym;

    public MainWindowViewModel(IDialogService dialogService)
    {
      this.dialogService = dialogService;

      AddSynonymsCommand = new DelegateCommand(AddSynonyms);
      OpenHelloWorldDialogCommand = new DelegateCommand(DispalyMessageBox);
      AllWords = new ObservableCollection<string>();
      Synonyms = new ObservableCollection<string>();
    }

    public DelegateCommand AddSynonymsCommand { get; private set; }

    public DelegateCommand OpenHelloWorldDialogCommand { get; private set; }

    public string InputText
    {
      get { return inputText; }
      set { SetProperty(ref inputText, value); }
    }

    /// <summary>
    /// Represent the selected word in the listview containing all words.
    /// </summary>
    public string SelectedSynonym
    {
      get { return selectedSynonym; }
      set
      {
        SetProperty(ref selectedSynonym, value);
        RefreshSynonyms(value);
      }
    }

    public ObservableCollection<string> AllWords { get; }

    public ObservableCollection<string> Synonyms { get; }

    /// <summary>
    /// Refresh the synonym collection with values 
    /// </summary>
    /// <param name="word"></param>
    private async void RefreshSynonyms(string word)
    {
      IEnumerable<string> synonyms = await Task.Run(async () =>
     {
       return await ThesaurusAPIHelper.GetSynonymsAsync(word);
     });

      Synonyms.Clear();
      Synonyms.AddRange(synonyms);
    }

    /// <summary>
    /// Refresh all the words
    /// </summary>
    private async void RefreshWords()
    {
      IEnumerable<string> words = await Task.Run(async () =>
      {
        return await ThesaurusAPIHelper.GetWordsAsync();
      });

      AllWords.Clear();
      AllWords.AddRange(words);
    }

    /// <summary>
    /// Parse the input and adds the words to the Thesaurus APi
    /// </summary>
    private async void AddSynonyms()
    {
      if (string.IsNullOrWhiteSpace(inputText))
      {
        return;
      }

      var parsedInput = inputText.Split(",").Select(s => s.Trim());

      await Task.Run(() => ThesaurusAPIHelper.AddSynonymsAsync(parsedInput));

      RefreshWords();
      Synonyms.Clear();
      InputText = string.Empty;
    }

    /// <summary>
    /// Displays a message box
    /// </summary>
    private void DispalyMessageBox()
    {
      dialogService.ShowDialog(typeof(HelloWorldDialogView).Name, null, null);
    }

  }
}
