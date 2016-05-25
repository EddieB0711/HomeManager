using System.ComponentModel.DataAnnotations;

namespace HomeManager.Books.Business
{
    public class AuthorRecord
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}