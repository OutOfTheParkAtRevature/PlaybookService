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
        public DbSet<Play> Plays;
        public DbSet<Playbook> Playbooks;
        public Repo(PlaybookContext progContext, ILogger<Repo> logger)
        {
            _playbookContext = progContext;
            _logger = logger;
        }

        public async Task<Playbook> GetPlaybookById(int id)
        {
            return await Playbooks.FindAsync(id);
        }
        public async Task<IEnumerable<Playbook>> GetPlaybooks()
        {
            return await Playbooks.ToListAsync();
        }
        public async Task<Play> GetPlayById(int id)
        {
            return await Plays.FindAsync(id);
        }
        public async Task<IEnumerable<Play>> GetPlays()
        {
            return await Plays.ToListAsync();
        }
    }
}
