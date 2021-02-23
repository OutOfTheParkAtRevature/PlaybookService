using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class PlaybookContext : DbContext
    {

        public DbSet<Playbook> Playbooks { get; set; }
        public DbSet<Play> Plays { get; set; }

        public PlaybookContext() { }
        public PlaybookContext(DbContextOptions<PlaybookContext> options) : base(options) { }

    }
}

