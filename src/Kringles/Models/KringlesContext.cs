using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Kringles.Models
{
    public class KringlesContext : IdentityDbContext<KringlesUser>
    {
        private IConfigurationRoot _config;

        public KringlesContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        public DbSet<Kringle> Kringles { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["Data:KringlesContextConnection"]);
        }
    }
}
