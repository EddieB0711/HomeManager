using System.ComponentModel.DataAnnotations;

namespace HomeManager.Books.Business
{
    public class GenreRecord
    {
        [Key]
        public int Id { get; set; }

        public string Genre { get; set; }
    }
}