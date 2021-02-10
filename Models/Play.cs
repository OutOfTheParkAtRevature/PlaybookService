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
    public class Play
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Play ID")]
        public Guid PlayID { get; set; }
        [DisplayName("Playbook ID")]
        [ForeignKey("PlaybookID")]
        public Guid PlaybookId { get; set; }
        [DisplayName("Play Name")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        public byte[] DrawnPlay { get; set; } //might change, goal is to have coaches able to draw a play and save it to the playbook
    }
}
