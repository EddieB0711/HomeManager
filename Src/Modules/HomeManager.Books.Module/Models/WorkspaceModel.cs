using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using HomeManager.Books.Business;
using HomeManager.Infrastructure.Extensions;

namespace HomeManager.Books.Module.Models
{
    [MetadataType(typeof(BookModelMetadata))]
    public class WorkspaceModel
    {
        private WorkspaceModel(IEnumerable<BookModel> books, IEnumerable<AuthorModel> authors, IEnumerable<GenreModel> genres)
        {
            Books = books;
            Authors = authors;
            Genres = genres;
        }

        public IEnumerable<BookModel> Books { get; private set; }

        public IEnumerable<AuthorModel> Authors { get; private set; }

        public IEnumerable<GenreModel> Genres { get; private set; }

        public static WorkspaceModel Create(IEnumerable<BookRecord> books, IEnumerable<AuthorRecord> authors, IEnumerable<GenreRecord> genres)
        {
            var bookModels = books.Cast(b => ConverToBookModel(b, authors, genres));
            var authorModels = authors.Cast(a => new AuthorModel(a.Id, a.FirstName, a.LastName));
            var genreModels = genres.Cast(g => new GenreModel(g.Id, g.Genre));

            return new WorkspaceModel(bookModels, authorModels, genreModels);
        }

        private static BookModel ConverToBookModel(BookRecord record, IEnumerable<AuthorRecord> authors, IEnumerable<GenreRecord> genres)
        {
            var bookAuthors = authors.Where(record.Authors, a => i => a.Id == i)
                    .Cast(a => new AuthorModel(a.Id, a.FirstName, a.LastName));
            var bookGenres = genres.Where(record.Genres, g => i => g.Id == i)
                .Cast(g => new GenreModel(g.Id, g.Genre));

            return new BookModel(record.Id, record.Title, record.PublicationDate, bookAuthors, bookGenres);
        }
    }

    internal class WorkspaceModelMetadata : IMetadataProvider<WorkspaceModel>
    {
        public void BuildMetadata(MetadataBuilder<WorkspaceModel> builder)
        {
            builder.Property(p => p.Books);
            builder.Property(p => p.Authors);
            builder.Property(p => p.Genres);

            builder.TableLayout()
                .Group("Book Info")
                    .ContainsProperty(p => p.Books)
                    .ContainsProperty(p => p.Authors)
                    .ContainsProperty(p => p.Genres)
                .EndGroup();
        }
    }
}