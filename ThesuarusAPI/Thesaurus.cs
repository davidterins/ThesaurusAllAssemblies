using System;
using System.Collections.Generic;
using Thesaurus;
using System.Linq;
using ThesuarusAPI.Models;
using System.Diagnostics;

namespace ThesuarusAPI
{
  public class Thesaurus : IThesaurus
  {
    private static readonly Dictionary<int, SynonymGroup> synonymGroups = new Dictionary<int, SynonymGroup>();

    private static readonly Dictionary<string, WordModel> wordToSynonymGroupDictionary = new Dictionary<string, WordModel>();

    public void AddSynonyms(IEnumerable<string> synonyms)
    {
      HashSet<int> foundSynonymGroupIDs = new HashSet<int>();
      List<string> unrecognizedWords = new List<string>();

      foreach (string word in synonyms)
      {
        if (wordToSynonymGroupDictionary.ContainsKey(word))
        {
          foundSynonymGroupIDs.Add(wordToSynonymGroupDictionary[word].SynonymGroupID);
          continue;
        }
        else
        {
          unrecognizedWords.Add(word);
        }
      }

      SynonymGroup synonymGroup;

      if (foundSynonymGroupIDs.Count == 0)
      {
        // No synonyms was found for the words entered, create a new synonym group
        synonymGroup = new SynonymGroup(synonymGroups.Count);
        synonymGroups.Add(synonymGroup.ID, synonymGroup);
      }
      else if (foundSynonymGroupIDs.Count == 1)
      {
        // One synonym group was found.
        synonymGroup = synonymGroups[foundSynonymGroupIDs.First()];
      }
      else
      {
        synonymGroup = GetMergedSynonymGroup(foundSynonymGroupIDs);
      }

      foreach (string word in unrecognizedWords)
      {
        WordModel wordModel = new WordModel(wordToSynonymGroupDictionary.Count, synonymGroup.ID, word);
        synonymGroup.Members.Add(wordModel);
        wordToSynonymGroupDictionary.Add(word, wordModel);
      }
    }

    public IEnumerable<string> GetSynonyms(string word)
    {
      if (wordToSynonymGroupDictionary.ContainsKey(word))
      {
        int synonymGroupID = wordToSynonymGroupDictionary[word].SynonymGroupID;
        return synonymGroups[synonymGroupID].Members.Select(word => word.Characters);
      }

      return null;
    }

    public IEnumerable<string> GetWords()
    {
      return wordToSynonymGroupDictionary.Keys;
    }


    /// <summary>
    /// Gets a merged synonym group.
    /// </summary>
    /// <param name="foundSynonymGroupIDs">The synonym group id's to merge</param>
    /// <returns>A merged synonym group</returns>
    private static SynonymGroup GetMergedSynonymGroup(HashSet<int> foundSynonymGroupIDs)
    {
      int targetID = foundSynonymGroupIDs.Min();
      SynonymGroup targetSynonymGroup = synonymGroups[targetID];

      foundSynonymGroupIDs.Remove(targetID);

      foreach (var id in foundSynonymGroupIDs)
      {
        synonymGroups[id].MergeInto(targetSynonymGroup);
        synonymGroups.Remove(id);
      }

      return targetSynonymGroup;
    }
  }
}
