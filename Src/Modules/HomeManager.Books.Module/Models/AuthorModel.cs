using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using HomeManager.Infrastructure.Extensions;

namespace HomeManager.Books.Module.Models
{
    [MetadataType(typeof(AuthorModelMetadata))]
    public class AuthorModel
    {
        public AuthorModel(int id, string firstName, string lastName)
        {
            firstName.NullGuard();
            lastName.NullGuard();

            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }
    }

    internal class AuthorModelMetadata : IMetadataProvider<AuthorModel>
    {
        public void BuildMetadata(MetadataBuilder<AuthorModel> builder)
        {
            builder.Property(p => p.FirstName);
            builder.Property(p => p.LastName);
            builder.Property(p => p.Id).Hidden();

            builder.TableLayout()
                .Group("Authors")
                    .ContainsProperty(p => p.FirstName)
                    .ContainsProperty(p => p.LastName)
                .EndGroup();
        }
    }
}