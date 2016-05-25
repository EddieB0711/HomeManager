using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeManager.Books.Business
{
    public class BookRecord
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public int PublicationDate { get; set; }

        public IEnumerable<int> Authors { get; set; }

        public IEnumerable<int> Genres { get; set; }
    }
}