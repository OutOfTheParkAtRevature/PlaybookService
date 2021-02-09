using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class PlaybookContext : DbContext
    {
        public DbSet<Play> plays { get; set; }
        public DbSet<Playbook> playbooks { get; set; }
        public PlaybookContext() { }
        public PlaybookContext(DbContextOptions<PlaybookContext> options) : base(options) { }
    }
}
