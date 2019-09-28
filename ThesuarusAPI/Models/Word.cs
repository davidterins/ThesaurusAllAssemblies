using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesuarusAPI.Models
{
  public class WordModel
  {
    public WordModel(int id, int synonymGroupID, string characters)
    {
      ID = id;
      SynonymGroupID = synonymGroupID;
      Characters = characters;
    }

    public int ID { get; }

    public int SynonymGroupID { get; set; }

    public string Characters { get; }
  }
}
