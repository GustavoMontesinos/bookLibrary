using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books);
        builder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books);
        builder.Entity<Book>()
            .HasMany(b => b.Sections)
            .WithOne(s => s.Book);
        builder.Entity<Book>()
            .HasMany(b => b.Tags)
            .WithMany(t => t.Books);
        builder.Entity<Book>()
            .HasOne(b => b.Language)
            .WithMany(t => t.Books);
        builder.Entity<Publisher>()
            .HasIndex(p => p.Name)
            .IsUnique();
        builder.Entity<Tag>()
            .HasIndex(p => p.Name)
            .IsUnique();

        builder.Entity<Language>().HasData(
            new Language{ Id = Guid.NewGuid(), Name = "Afrikaans"},
            new Language{ Id = Guid.NewGuid(), Name = "Arabic"},
            new Language{ Id = Guid.NewGuid(), Name = "Bengali"},
            new Language{ Id = Guid.NewGuid(), Name = "Bulgarian"},
            new Language{ Id = Guid.NewGuid(), Name = "Catalan"},
            new Language{ Id = Guid.NewGuid(), Name = "Cantonese"},
            new Language{ Id = Guid.NewGuid(), Name = "Croatian"},
            new Language{ Id = Guid.NewGuid(), Name = "Czech"},
            new Language{ Id = Guid.NewGuid(), Name = "Danish"},
            new Language{ Id = Guid.NewGuid(), Name = "Dutch"},
            new Language{ Id = Guid.NewGuid(), Name = "Lithuanian"},
            new Language{ Id = Guid.NewGuid(), Name = "Malay"},
            new Language{ Id = Guid.NewGuid(), Name = "Malayalam"},
            new Language{ Id = Guid.NewGuid(), Name = "Panjabi"},
            new Language{ Id = Guid.NewGuid(), Name = "Tamil"},
            new Language{ Id = Guid.NewGuid(), Name = "English"},
            new Language{ Id = Guid.NewGuid(), Name = "Finnish"},
            new Language{ Id = Guid.NewGuid(), Name = "French"},
            new Language{ Id = Guid.NewGuid(), Name = "German"},
            new Language{ Id = Guid.NewGuid(), Name = "Greek"},
            new Language{ Id = Guid.NewGuid(), Name = "Hebrew"},
            new Language{ Id = Guid.NewGuid(), Name = "Hindi"},
            new Language{ Id = Guid.NewGuid(), Name = "Hungarian"},
            new Language{ Id = Guid.NewGuid(), Name = "Indonesian"},
            new Language{ Id = Guid.NewGuid(), Name = "Italian"},
            new Language{ Id = Guid.NewGuid(), Name = "Japanese"},
            new Language{ Id = Guid.NewGuid(), Name = "Javanese"},
            new Language{ Id = Guid.NewGuid(), Name = "Korean"},
            new Language{ Id = Guid.NewGuid(), Name = "Norwegian"},
            new Language{ Id = Guid.NewGuid(), Name = "Polish"},
            new Language{ Id = Guid.NewGuid(), Name = "Portuguese"},
            new Language{ Id = Guid.NewGuid(), Name = "Romanian"},
            new Language{ Id = Guid.NewGuid(), Name = "Russian"},
            new Language{ Id = Guid.NewGuid(), Name = "Serbian"},
            new Language{ Id = Guid.NewGuid(), Name = "Slovak"},
            new Language{ Id = Guid.NewGuid(), Name = "Slovene"},
            new Language{ Id = Guid.NewGuid(), Name = "Spanish"},
            new Language{ Id = Guid.NewGuid(), Name = "Swedish"},
            new Language{ Id = Guid.NewGuid(), Name = "Telugu"},
            new Language{ Id = Guid.NewGuid(), Name = "Thai"},
            new Language{ Id = Guid.NewGuid(), Name = "Turkish"},
            new Language{ Id = Guid.NewGuid(), Name = "Ukrainian"},
            new Language{ Id = Guid.NewGuid(), Name = "Vietnamese"},
            new Language{ Id = Guid.NewGuid(), Name = "Welsh"},
            new Language{ Id = Guid.NewGuid(), Name = "Algerian"},
            new Language{ Id = Guid.NewGuid(), Name = "Aramaic"},
            new Language{ Id = Guid.NewGuid(), Name = "Armenian"},
            new Language{ Id = Guid.NewGuid(), Name = "Berber"},
            new Language{ Id = Guid.NewGuid(), Name = "Burmese"},
            new Language{ Id = Guid.NewGuid(), Name = "Bosnian"},
            new Language{ Id = Guid.NewGuid(), Name = "Brazilian"},
            new Language{ Id = Guid.NewGuid(), Name = "Cypriot"},
            new Language{ Id = Guid.NewGuid(), Name = "Corsica"},
            new Language{ Id = Guid.NewGuid(), Name = "Creole"},
            new Language{ Id = Guid.NewGuid(), Name = "Scottish"},
            new Language{ Id = Guid.NewGuid(), Name = "Egyptian"},
            new Language{ Id = Guid.NewGuid(), Name = "Esperanto"},
            new Language{ Id = Guid.NewGuid(), Name = "Estonian"},
            new Language{ Id = Guid.NewGuid(), Name = "Finn"},
            new Language{ Id = Guid.NewGuid(), Name = "Flemish"},
            new Language{ Id = Guid.NewGuid(), Name = "Georgian"},
            new Language{ Id = Guid.NewGuid(), Name = "Hawaiian"},
            new Language{ Id = Guid.NewGuid(), Name = "Inuit"},
            new Language{ Id = Guid.NewGuid(), Name = "Irish"},
            new Language{ Id = Guid.NewGuid(), Name = "Icelandic"},
            new Language{ Id = Guid.NewGuid(), Name = "Latin"},
            new Language{ Id = Guid.NewGuid(), Name = "Mandarin"},
            new Language{ Id = Guid.NewGuid(), Name = "Nepalese"},
            new Language{ Id = Guid.NewGuid(), Name = "Sanskrit"},
            new Language{ Id = Guid.NewGuid(), Name = "Tagalog"},
            new Language{ Id = Guid.NewGuid(), Name = "Tahitian"},
            new Language{ Id = Guid.NewGuid(), Name = "Tibetan"},
            new Language{ Id = Guid.NewGuid(), Name = "Gypsy"},
            new Language{ Id = Guid.NewGuid(), Name = "Wu"} );
    }
}