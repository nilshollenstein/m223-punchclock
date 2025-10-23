using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace M223PunchclockDotnet.Model {

    public class DatabaseContext : DbContext 
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options){

        }

        
        public DbSet<Entry> Entries {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Entry>().ToTable("entry");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;Ssl Mode=Disable;");
    }
}