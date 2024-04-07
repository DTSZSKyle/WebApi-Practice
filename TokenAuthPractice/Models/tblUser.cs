using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TokenAuthPractice.Models
{
    [Table("tblUser", Schema = "dbo")]
    public class tblUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string Username { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string Password { get; set; }
    }
}