using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class Repo
    {

        private readonly PlaybookContext _playbookContext;
        private readonly ILogger _logger;

        public DbSet<Playbook> Playbooks;
        public DbSet<Play> Plays;

        public Repo(PlaybookContext playbookContext, ILogger<Repo> logger)
        {
            _playbookContext = playbookContext;
            _logger = logger;
            this.Playbooks = _playbookContext.Playbooks;
            this.Plays = _playbookContext.Plays;

        }
        /// <summary>
        ///  saves changes to the database
        /// </summary>
        /// <returns></returns>
        public async Task CommitSave()
        {
            await _playbookContext.SaveChangesAsync();
        }
        /// <summary>
        /// returns a Playbood by the playbookID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Playbook> GetPlaybookById(Guid id)
        {

            return await Playbooks.FindAsync(id);
        }
        /// <summary>
        /// returns all playBooks
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Playbook>> GetPlaybooks()
        {
            return await Playbooks.ToListAsync();
        }
        /// <summary>
        ///  returns a play by the playID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Play> GetPlayById(Guid id)
        {
            return await Plays.FindAsync(id);
        }
        /// <summary>
        /// returns all plays
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Play>> GetPlays()
        {
            return await Plays.ToListAsync();
        }
        /// <summary>
        /// returns all plays with the playbookID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Play>> GetPlaysByPlaybookId(Guid id)
        {
            return await Plays.Where(x => x.PlaybookId == id).ToListAsync();
        }
        /// <summary>
        /// returns all of the playbooks where TeamID == id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Playbook>> GetPlaybooksByTeamId(Guid id)
        {
            return await Playbooks.Where(x => x.TeamID == id).ToListAsync();
        }
    }
}
