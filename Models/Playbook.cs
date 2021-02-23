using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Playbook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Playbook ID")]
        public Guid Playbookid { get; set; }
        [DisplayName("Team ID")]
        public Guid TeamID { get; set; }
        [DisplayName("Playbook Name")]
        public string Name { get; set; }
        [DisplayName("In Development")]
        public bool InDev { get; set; }
    }
}
