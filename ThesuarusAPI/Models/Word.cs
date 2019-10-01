using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThesuarusAPI.Models
{
  /// <summary>
  /// Represents a word with a synonym group
  /// </summary>
  public class Word
  {
    public Word()
    {
    }

    /// <summary>
    /// Primary key
    /// </summary>
    public int WordID { get; set; }

    /// <summary>
    /// Foreign key to the words synonym group.
    /// </summary>
    public int SynonymGroupID { get; set; }

    /// <summary>
    /// The synonym group the word belongs to.
    /// </summary>
    public SynonymGroup SynonymGroup { get; set; }

    /// <summary>
    /// The actual word.
    /// </summary>
    public string Characters { get; set; }
  }
}
