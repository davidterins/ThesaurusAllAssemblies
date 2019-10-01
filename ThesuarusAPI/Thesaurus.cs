using System;
using System.Collections.Generic;
using Thesaurus;
using System.Linq;
using ThesuarusAPI.Models;
using System.Diagnostics;
using ThesuarusAPI.Data;

namespace ThesuarusAPI
{
  /// <summary>
  /// Represents a thesaurus.
  /// </summary>
  public class Thesaurus : IThesaurus
  {
    public readonly ThesaurusContext dbContext;

    public Thesaurus(ThesaurusContext dbContext)
    {
      this.dbContext = dbContext;
    }

    /// <summary>
    /// Adds the given synonyms to the thesaurus.
    /// </summary>
    /// <param name="synonyms">The synonyms to add.</param>
    public void AddSynonyms(IEnumerable<string> synonyms)
    {
      HashSet<int> foundSynonymGroupIDs = new HashSet<int>();
      List<string> unrecognizedWords = new List<string>();

      foreach (string word in synonyms)
      {
        var wordsQuery = dbContext.Words.Where(w => w.Characters == word);
        if (wordsQuery.Any())
        {
          foundSynonymGroupIDs.Add(wordsQuery.Select(p => p.SynonymGroupID).First());
          continue;
        }
        else
        {
          unrecognizedWords.Add(word);
        }
      }

      SynonymGroup synonymGroup = GetSynonymGroup(foundSynonymGroupIDs);

      foreach (string word in unrecognizedWords)
      {
        var newWord = new Word
        {
          Characters = word,
          SynonymGroup = synonymGroup
        };

        dbContext.Words.Add(newWord);
      }

      dbContext.SaveChanges();
    }

    /// <summary>
    /// Gets the synonyms for a given word.
    /// </summary>
    /// <param name="word">The word to return the synonyms for.</param>
    /// <returns>
    /// A <see cref="IEnumerable{String}"/> of synonyms.
    /// </returns>
    public IEnumerable<string> GetSynonyms(string word)
    {
      var result = new List<string>();

      var synonymGroupId = dbContext.Words
        .Where(w => w.Characters == word)
        .Select(w => w.SynonymGroupID)
        .First();

      result = dbContext.Words
        .Where(g => g.SynonymGroupID == synonymGroupId)
        .Select(w => w.Characters)
        .ToList();

      return result;
    }

    /// <summary>
    /// Gets all words from the thesaurus.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{String}"/> containing all the words in
    /// the thesaurus.
    /// </returns>
    public IEnumerable<string> GetWords()
    {
      return dbContext.Words.Select(s => s.Characters).ToList();
    }

    /// <summary>
    /// Gets the <see cref="SynonymGroup"/> that the words should be added to.
    /// </summary>
    /// <param name="foundSynonymGroupIDs">the ids of possible groups</param>
    /// <returns>The <see cref="SynonymGroup"/> that the words should be added to</returns>
    private SynonymGroup GetSynonymGroup(HashSet<int> foundSynonymGroupIDs)
    {
      SynonymGroup synonymGroup;

      if (foundSynonymGroupIDs.Count == 0)
      {
        // No synonyms was found for the words entered, create a new synonym group
        synonymGroup = new SynonymGroup();
      }
      else if (foundSynonymGroupIDs.Count == 1)
      {
        // One synonym group was found.
        synonymGroup = dbContext.SynonymGroups.Find(foundSynonymGroupIDs.First());
      }
      else
      {
        // The words was members of multiple synonymgroups. 
        synonymGroup = GetMergedSynonymGroup(foundSynonymGroupIDs);
      }

      return synonymGroup;
    }


    /// <summary>
    /// Change <see cref="Word.SynonymGroupID"/>'s in <see cref="ThesaurusContext.Words"/> to point to the same
    /// <see cref="SynonymGroup"/> in <see cref="ThesaurusContext.SynonymGroups"/> and deletes the unused
    /// <see cref="SynonymGroup"/>'s from <see cref="ThesaurusContext.SynonymGroups"/>.
    /// </summary>
    /// <param name="synonymGroupIDsToMerge">The synonym group id's to merge</param>
    /// <returns>A merged synonym group</returns>
    private SynonymGroup GetMergedSynonymGroup(HashSet<int> synonymGroupIDsToMerge)
    {
      int targetID = synonymGroupIDsToMerge.Min();
      SynonymGroup targetSynonymGroup = dbContext.SynonymGroups.Find(targetID);

      synonymGroupIDsToMerge.Remove(targetID);

      foreach (int groupId in synonymGroupIDsToMerge)
      {
        var words = dbContext.Words.Where(g => g.SynonymGroupID == groupId).ToList();

        SynonymGroup mergingSynonymGroup = dbContext.SynonymGroups.Find(groupId);

        foreach (Word word in words)
        {
          word.SynonymGroup = targetSynonymGroup;
          dbContext.SaveChanges();
        }

        dbContext.SynonymGroups.Remove(mergingSynonymGroup);
        dbContext.SaveChanges();
      }

      return targetSynonymGroup;
    }
  }
}
