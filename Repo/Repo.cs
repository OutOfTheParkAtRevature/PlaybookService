using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataTransfer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class Repo
    {

        private readonly PlaybookContext _playbookContext;
        private readonly ILogger _logger;
        public DbSet<Playbook> playbooks;
        public DbSet<Play> plays;

        public Repo(PlaybookContext playbookContext, ILogger<Repo> logger)
        {
            _playbookContext = playbookContext;
            _logger = logger;
            this.playbooks = _playbookContext.Playbooks;
            this.plays = _playbookContext.Plays;
        }

        public async Task CommitSave()
        {
            await _playbookContext.SaveChangesAsync();
        }
        public async Task<Playbook> GetPlaybookById(int id)
        {
            return await playbooks.FindAsync(id);
        }
        public async Task<IEnumerable<Playbook>> GetPlaybooks()
        {
            return await playbooks.ToListAsync();
        }
        public async Task<Play> GetPlayById(int id)
        {
            return await plays.FindAsync(id);
        }
        public async Task<IEnumerable<Play>> GetPlays()
        {
            return await plays.ToListAsync();
        }
    }
}
