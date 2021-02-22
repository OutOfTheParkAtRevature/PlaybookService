using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DataTransfer;

namespace Model.DataTransfer
{

    public class PlayBookWithPlaysDto
    {
        
        [DisplayName("Playbook ID")]
        public Guid Playbookid { get; set; }
        [DisplayName("Team ID")]
        public Guid TeamID { get; set; }
        [DisplayName("Playbook Name")]
        public string Name { get; set; }
        public List<PlayDto> playDtos { get; set; } = new List<PlayDto>();
    }
}
