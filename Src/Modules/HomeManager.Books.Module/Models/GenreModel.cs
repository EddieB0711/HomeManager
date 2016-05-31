using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using HomeManager.Infrastructure.Extensions;

namespace HomeManager.Books.Module.Models
{
    [MetadataType(typeof(BookModelMetadata))]
    public class GenreModel
    {
        public GenreModel(int id, string genre)
        {
            genre.NullGuard();

            Id = id;
            Genre = genre;
        }

        public int Id { get; private set; }

        public string Genre { get; private set; }
    }

    public class GenreModelMetadata : IMetadataProvider<GenreModel>
    {
        public void BuildMetadata(MetadataBuilder<GenreModel> builder)
        {
            builder.Property(p => p.Genre);
            builder.Property(p => p.Id).Hidden();

            builder.TableLayout()
                .Group("Genres")
                    .ContainsProperty(p => p.Genre)
                .EndGroup();
        }
    }
}