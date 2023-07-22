using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSF
{
    [Table("Users")]
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();       
        public string FirstName { get; set; } = String.Empty;        
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string login { get; set; } = String.Empty;
        public string password { get; set; } = String.Empty;
        
        public List<Book> Books { get; set; } = new List<Book>();        
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
