using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlogSF
{
    [Table("Books")]
    public class Book
    {        
        public Guid Id { get; set; } = Guid.NewGuid();        
        public string Name { get; set; } = String.Empty;        
        public string Author { get; set; } = String.Empty;
        public DateTime CreatedData { get; set; } = DateTime.Now;                 
        public string Content { get; set; } = String.Empty;       
    }
}
