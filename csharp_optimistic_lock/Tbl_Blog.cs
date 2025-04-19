using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_optimistic_lock
{
    [Table("Tbl_Blog")]
    public class Tbl_Blog
    {
        [Key]
        public string BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
