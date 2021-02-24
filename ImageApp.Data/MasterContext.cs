using ImageApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Data
{
    public class MasterContext : DbContext
    {
        public MasterContext()
        {
        }

        public MasterContext(DbContextOptions<MasterContext> options)
        {

        }

        DbSet<UserModel> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasKey(x => x.Id);
            modelBuilder.Entity<CategoryModel>().HasKey(x => x.Id);

            modelBuilder.Entity<UserModel>().HasMany(h => h.Categories).WithOne(w => w.UserModel).HasForeignKey(f => f.CreateUser);
            modelBuilder.Entity<UserModel>().HasMany(h => h.Categories).WithOne(w => w.UserModel).HasForeignKey(f => f.UpdateUser);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(Environment.GetEnvironmentVariable("MYSQL_URI"));
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
