﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UsertId { get; set; }

        public string FirstName {  get; set; }

        public string LastName { get; set; }

        
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
