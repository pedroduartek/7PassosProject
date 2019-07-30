using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class ComerestusDbContext : DbContext
    {
        public DbSet<ModeloDeHiscores> ModeloDeHiscores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=ComerestusDB; Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
