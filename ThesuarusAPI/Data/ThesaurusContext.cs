using Microsoft.EntityFrameworkCore;
using ThesuarusAPI.Models;

namespace ThesuarusAPI.Data
{
  /// <summary>
  /// The database context for a thesaurus
  /// </summary>
  public class ThesaurusContext : DbContext
  {
    public ThesaurusContext(DbContextOptions<ThesaurusContext> options)
      : base(options)
    {
    }

    public DbSet<Word> Words { get; set; }

    public DbSet<SynonymGroup> SynonymGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<SynonymGroup>()
        .HasMany(o => o.Words)
        .WithOne(word => word.SynonymGroup)
        .OnDelete(DeleteBehavior.NoAction);
    }
  }
}
