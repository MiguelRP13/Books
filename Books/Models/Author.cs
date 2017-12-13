using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models {
    public class Author {
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
