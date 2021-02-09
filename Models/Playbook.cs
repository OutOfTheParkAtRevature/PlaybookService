using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class Playbook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Playbook ID")]
        public int PlaybookID { get; set; }
        [DisplayName("Team ID")]
        [ForeignKey("TeamID")]
        public int TeamID { get; set; }
    }
}
