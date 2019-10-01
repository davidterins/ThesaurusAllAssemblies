using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThesuarusAPI.Models
{
  /// <summary>
  /// Represents a group of synonyms
  /// </summary>
  public class SynonymGroup
  {
    public SynonymGroup()
    {
      Words = new List<Word>();
    }

    /// <summary>
    /// Primary key
    /// </summary>
    public int SynonymGroupID { get; set; }

    /// <summary>
    /// A list of words included in the synonym group.
    /// </summary>
    public virtual ICollection<Word> Words { get; set; }
  }
}
