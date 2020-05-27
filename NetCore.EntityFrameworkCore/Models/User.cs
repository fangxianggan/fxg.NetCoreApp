using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCore.EntityFrameworkCore.Models
{
    [Table("User")]
   public  class User
    {
        [Key]
        public int id { set; get; }

        public string name { set; get; }
    }
}
