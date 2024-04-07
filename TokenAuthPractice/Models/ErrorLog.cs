using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TokenAuthPractice.Models
{
    [Table("ErrorLog", Schema = "dbo")]
    public class ErrorLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Message { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string RequestMethod { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string RequestUri { get; set; }

        public DateTime? TimeUtc { get; set; }
    }
}