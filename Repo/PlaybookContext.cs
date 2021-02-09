using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

