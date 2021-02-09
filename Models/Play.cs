using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Play
    {
        [Key]
        [DisplayName("Play ID")]
        public Guid PlayID { get; set; }
        [DisplayName("Playbook ID")]
        public Guid PlaybookID { get; set; }
        public string name { get; set; }
        public string Desription { get; set; }
        [DisplayName("Drawn Play")]
        public byte[] DrawnPlay { get; set; }
        public Guid SubmittedBy { get; set; }
    }
}
