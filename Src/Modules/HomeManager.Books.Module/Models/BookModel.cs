using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using HomeManager.Infrastructure.Extensions;

namespace HomeManager.Books.Module.Models
{
    [MetadataType(typeof(BookModelMetadata))]
    public class BookModel
    {
        public BookModel(int id, string title, int publicationDate, IEnumerable<AuthorModel> authors, IEnumerable<GenreModel> genres)
        {
            title.NullGuard();

            Id = id;
            Title = title;
            PublicationDate = publicationDate;
            Authors = authors;
            Genres = genres;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public int PublicationDate { get; private set; }

        public IEnumerable<AuthorModel> Authors { get; private set; }

        public IEnumerable<GenreModel> Genres { get; private set; }
    }

    public class BookModelMetadata : IMetadataProvider<BookModel>
    {
        public void BuildMetadata(MetadataBuilder<BookModel> builder)
        {
            builder.Property(p => p.Title);
            builder.Property(p => p.PublicationDate);
            builder.Property(p => p.Id).Hidden();

            builder.TableLayout()
                .Group("Books")
                    .ContainsProperty(p => p.Title)
                    .ContainsProperty(p => p.PublicationDate)
                .EndGroup();
        }
    }
}