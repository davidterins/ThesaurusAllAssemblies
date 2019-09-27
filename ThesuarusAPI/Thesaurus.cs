using System;
using System.Collections.Generic;
using Thesaurus;

namespace ThesuarusAPI
{
  public class Thesaurus : IThesaurus
  {
    public void AddSynonyms(IEnumerable<string> synonyms)
    {
    }

    public IEnumerable<string> GetSynonyms(string word)
    {
      return new string[] { "SynonymOne", "SynonymTwo", "SynonymThree" };
    }

    public IEnumerable<string> GetWords()
    {
      return new string[] { "AllWordsOne", "AllWordsTwo", "AllWordsThree" };
    }
  }
}
