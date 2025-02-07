﻿using Microsoft.EntityFrameworkCore;
using PhotoProcessingApp.Model;


namespace PhotoProcessingApp.Infrastructure
{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }

}
