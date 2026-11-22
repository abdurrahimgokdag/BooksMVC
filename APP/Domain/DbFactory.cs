using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace APP.Domain;

public class DbFactory : IDesignTimeDbContextFactory<Db>
{
    const string CONNECTIONSTRING = "data source=DB.db";
    
    public Db CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Db>();
        optionsBuilder.UseSqlite(CONNECTIONSTRING);
        return new Db(optionsBuilder.Options);
    }
    
        // Seeds the database tables with initial data (Optional).
    public void SeedDb()
    {
        // Create a new instance of the Db type:
        var db = CreateDbContext(null);

        // Delete existing data:
        // ("çok" taraftaki tablolar önce silinir, yoksa foreign key hatası olur)
        var bookGenres = db.BookGenres.ToList();
        db.BookGenres.RemoveRange(bookGenres);
        var books = db.Books.ToList();
        db.Books.RemoveRange(books);
        var authors = db.Authors.ToList();
        db.Authors.RemoveRange(authors);
        var genres = db.Genres.ToList();
        db.Genres.RemoveRange(genres);

        // Reset identity columns (for SQLite):
        // IDs will start from 1
        db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE " +
            "SET SEQ=0 WHERE NAME='BookGenres'");
        db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE " +
            "SET SEQ=0 WHERE NAME='Books'");
        db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE " +
            "SET SEQ=0 WHERE NAME='Authors'");
        db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE " +
            "SET SEQ=0 WHERE NAME='Genres'");

        // Insert new data:
        // Genres (hocadaki Roles'un karşılığı):
        var genre = new Genre() { Name = "Bilim Kurgu" };
        db.Genres.Add(genre);
        genre = new Genre { Name = "Fantastik" };
        db.Genres.Add(genre);
        genre = new Genre { Name = "Macera" };
        db.Genres.Add(genre);
        genre = new Genre { Name = "Polisiye" };
        db.Genres.Add(genre);
        db.SaveChanges(); // commit changes to the database

        // Authors with Books (hocadaki Statuses with Users'ın karşılığı):
        db.Authors.Add(new Author
        {
            FirstName = "Frank",
            LastName = "Herbert",
            Books = new List<Book>
            {
                new Book
                {
                    Name = "Dune",
                    NumberOfPages = 712,
                    PublishDate = new DateTime(1965, 8, 1),
                    Price = 349.90m,
                    IsTopSeller = true,
                    BookGenres = new List<BookGenre>
                    {
                        new BookGenre { GenreId = db.Genres.Single(
                            g => g.Name == "Bilim Kurgu").Id },
                        new BookGenre { GenreId = db.Genres.Single(
                            g => g.Name == "Macera").Id }
                    }
                }
            }
        });
        db.Authors.Add(new Author
        {
            FirstName = "J.R.R.",
            LastName = "Tolkien",
            Books = new List<Book>
            {
                new Book
                {
                    Name = "Hobbit",
                    NumberOfPages = 368,
                    PublishDate = DateTime.Parse(
                        "09/21/1937", new CultureInfo("en-US")),
                    Price = 189.50m,
                    IsTopSeller = false,
                    BookGenres = new List<BookGenre>
                    {
                        new BookGenre { GenreId = db.Genres.Single(
                            g => g.Name == "Fantastik").Id }
                    }
                }
            }
        });
        db.Authors.Add(new Author { FirstName = "Agatha", LastName = "Christie" });
        db.SaveChanges();
    }
}