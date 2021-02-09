using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Playbook
    {
        [Key]
        [DisplayName("Playbook ID")]
        public Guid Playbookid { get; set; }
        [DisplayName("Team ID")]
        public int TeamID { get; set; }
    }
}
