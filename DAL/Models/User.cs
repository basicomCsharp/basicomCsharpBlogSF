﻿using System;
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
        //internal string id;

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();       
        public string FirstName { get; set; } = String.Empty;        
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Login { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public Role Role { get; set; }//public string Role { get; set; } = String.Empty;
    }
}
