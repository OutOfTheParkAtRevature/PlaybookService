using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataTransfer;
using Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaybookService
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaybookController : ControllerBase
    {
        private readonly Logic _logic;
        private readonly ILogger<PlaybookController> _logger;
        public PlaybookController(Logic logic, ILogger<PlaybookController> logger)
        {
            _logic = logic;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<Playbook>> GetPlaybooks()
        {
            return await _logic.GetPlaybooks();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Playbook>> GetPlaybook(string teamId,string name)
        {
            return await _logic.GetPlaybookById(Guid.Parse(teamId));
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Playbook>> GetPlaybookWithPlays(string teamId, string name)
        //{
        //    return await _logic.GetPlaybookById(Guid.Parse(teamId));
        //}
        [HttpGet("plays")]
        public async Task<IEnumerable<PlayDto>> GetPlays()
        {
            return await _logic.GetPlays();
        }
        [HttpGet("plays/{id}")]
        public async Task<ActionResult<PlayDto>> GetPlayDto(string teamId)
        {
            return await _logic.GetPlayDto(Guid.Parse(teamId));
        }
        [HttpPost]
        public async Task<ActionResult<Playbook>> CreatePlaybook(string teamId, string name)
        {
            return await _logic.CreatePlaybook(Guid.Parse(teamId),name);
        }
        [HttpPost("plays")]
        public async Task<ActionResult<Play>> CreatePlay(PlayDto createPlay)
        {
            return await _logic.CreatePlay(createPlay);
        }
        [HttpPut("plays/edit/{id}")]
        public async Task<ActionResult<Play>> EditPlay(string PlayID, PlayDto createPlay)
        {
            return await _logic.EditPlay(Guid.Parse(PlayID), createPlay);
        }
        [HttpDelete("/delete/{id}")]
        public async Task<ActionResult<Playbook>> DeletePlaybook(string id )
        {
            return await _logic.DeletePlaybook(Guid.Parse(id));
        }
        [HttpDelete("plays/delete/{id}")]
        public async Task<ActionResult<Play>> DeletePlay(string id)
        {
            return await _logic.DeletePlay(Guid.Parse(id));
        }
    }
}
