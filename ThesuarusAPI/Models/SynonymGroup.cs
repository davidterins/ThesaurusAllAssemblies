using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesuarusAPI.Models
{
  public class SynonymGroup
  {
    public SynonymGroup(int id)
    {
      ID = id;
      Members = new List<WordModel>();
    }

    public int ID { get; private set; }

    public List<WordModel> Members { get; }

    public void MergeInto(SynonymGroup target)
    {
      foreach (var wordModel in Members)
      {
        wordModel.SynonymGroupID = target.ID;
      }

      target.Members.AddRange(Members);
    }
  }
}
