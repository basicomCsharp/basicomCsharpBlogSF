using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSF
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = String.Empty;
        public DateTime CreatedData { get; set; } = DateTime.Now;
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
