using Microsoft.EntityFrameworkCore;
using PhotoProcessingService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoProcessingService.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)

        {
        }
    }
}
