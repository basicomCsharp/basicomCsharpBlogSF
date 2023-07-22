using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSF
{
    [Table("Comments")]
    public class Comment
    {      
        public Guid Id { get; set; } = Guid.NewGuid();        
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;

        //public Guid UserId { get; set; }
        //[ForeignKey("UserId")]
        //public User User { get; set; }

        //[ForeignKey("BookId")]
        //public Guid BookId { get; set; }
        //public Book Book { get; set; }
    }
}
