using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Collections.Generic;

namespace WPFClient
{
  public class MainWindowViewModel : BindableBase
  {
    private string _inputText;
    private string _selectedSynonym;

    public MainWindowViewModel()
    {
      AddSynonymsCommand = new DelegateCommand(AddSynonyms);
      AllWords = new ObservableCollection<string>();
      Synonyms = new ObservableCollection<string>();
    }

    public DelegateCommand AddSynonymsCommand { get; private set; }

    public string InputText
    {
      get { return _inputText; }
      set { SetProperty(ref _inputText, value); }
    }

    public string SelectedSynonym
    {
      get { return _selectedSynonym; }
      set
      {
        SetProperty(ref _selectedSynonym, value);
        RefreshSynonyms(value);
      }
    }

    public ObservableCollection<string> AllWords { get; }

    public ObservableCollection<string> Synonyms { get; }

    private async void RefreshSynonyms(string word)
    {
      IEnumerable<string> synonyms = await Task.Run(async () =>
     {
       return await ThesaurusAPIHelper.GetSynonymsAsync(word);
     });

      Synonyms.Clear();
      Synonyms.AddRange(synonyms);
    }

    private async void RefreshWords()
    {
      IEnumerable<string> words = await Task.Run(async () =>
      {
        return await ThesaurusAPIHelper.GetWordsAsync();
      });

      AllWords.Clear();
      AllWords.AddRange(words);
    }

    private async void AddSynonyms()
    {
      if (string.IsNullOrWhiteSpace(_inputText))
      {
        return;
      }

      var parsedInput = _inputText.Split(",").Select(s => s.Trim());

      await Task.Run(() => ThesaurusAPIHelper.AddSynonymsAsync(parsedInput));

      RefreshWords();
    }
  }
}
