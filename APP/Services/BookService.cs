using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class BookService : DbService<Book>, IService<BookRequest, BookResponse>
    {
        public BookService(DbContext db) : base(db)
        {
        }

        public CommandResponse Add(BookRequest request)
        {
            // Hocadaki "aynı kullanıcı adı var mı?" kontrolünün karşılığı:
            // aynı isimde kitap var mı?
            if (DbQuery().Any(b => b.Name == request.Name.Trim()))
                return Error("Book with the same name exists!");
            var entity = new Book
            {
                Name = request.Name.Trim(),
                NumberOfPages = request.NumberOfPages,
                // Request'te DateTime? + [Required] olduğu için buraya null gelmez.
                PublishDate = request.PublishDate.Value,
                Price = request.Price,
                IsTopSeller = request.IsTopSeller,
                AuthorId = request.AuthorId ?? 0,   // hocadaki StatusId ?? 0
                GenreIds = request.GenreIds,        // hocadaki RoleIds; BookGenres'ı oluşturur
            };
            DbAdd(entity);
            return Success(entity.Id, "Book created successfully.");
        }

        public BookRequest Edit(int id)
        {
            var entity = DbSingle(id);
            if (entity is null)
                return null;
            return new BookRequest
            {
                Id = entity.Id,
                Name = entity.Name,
                NumberOfPages = entity.NumberOfPages,
                PublishDate = entity.PublishDate,
                Price = entity.Price,
                IsTopSeller = entity.IsTopSeller,
                AuthorId = entity.AuthorId,
                GenreIds = entity.GenreIds
            };
        }

        protected override IQueryable<Book> DbQuery()
        {
            // Yazar ve türler de gelsin (AuthorNameR, GenreNamesR için),
            // önce çok satanlar, sonra isme göre sırala.
            // Not: SQLite decimal (Price) üzerinden sıralama yapamaz, o yüzden Price ile sıralamadık.
            return base.DbQuery().Include(b => b.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .OrderByDescending(b => b.IsTopSeller).ThenBy(b => b.Name);
        }

        public IQueryable<BookResponse> Query()
        {
            return DbQuery().Select(b => new BookResponse
            {
                Id = b.Id,
                Name = b.Name,
                NumberOfPages = b.NumberOfPages,
                PublishDate = b.PublishDate,
                Price = b.Price,
                IsTopSeller = b.IsTopSeller,
                AuthorId = b.AuthorId,
                GenreIds = b.GenreIds,

                NumberOfPagesR = b.NumberOfPages.HasValue ? b.NumberOfPages.Value.ToString()
                    : string.Empty,
                PublishDateR = b.PublishDate.ToString("MM/dd/yyyy"),
                PriceR = b.Price.ToString("N2"),
                IsTopSellerR = b.IsTopSeller ? "Yes" : "No",
                AuthorNameR = b.Author.FirstName + " " + b.Author.LastName,
                GenreNamesR = string.Join("<br>", b.BookGenres.Select(bg => bg.Genre.Name))
            });
        }

        public CommandResponse Remove(int id)
        {
            var entity = DbSingle(id);
            if (entity is null)
                return Error("Book not found!");

            // Delete the relational BookGenre entities first.
            DbRemove(entity.BookGenres);

            DbRemove(entity);

            return Success(entity.Id, "Book deleted successfully.");
        }

        public CommandResponse Update(BookRequest request)
        {
            if (DbQuery().Any(b => b.Id != request.Id && b.Name == request.Name.Trim()))
                return Error("Book with the same name exists!");

            var entity = DbSingle(request.Id);
            if (entity is null)
                return Error("Book not found!");

            // Delete the relational BookGenre entities first.
            if (request.GenreIds is not null)
                DbRemove(entity.BookGenres);

            entity.Name = request.Name.Trim();
            entity.NumberOfPages = request.NumberOfPages;
            entity.Price = request.Price;
            entity.IsTopSeller = request.IsTopSeller;
            if (request.PublishDate.HasValue)
                entity.PublishDate = request.PublishDate.Value;
            if (request.AuthorId.HasValue)
                entity.AuthorId = request.AuthorId.Value;
            if (request.GenreIds is not null)
                entity.GenreIds = request.GenreIds;
            DbUpdate(entity);

            return Success(entity.Id, "Book updated successfully.");
        }
    }
}