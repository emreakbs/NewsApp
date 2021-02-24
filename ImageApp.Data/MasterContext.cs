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
        DbSet<ImageModel> Images { get; set; }
        DbSet<CategoryModel> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ID
            modelBuilder.Entity<UserModel>().HasKey(x => x.Id);
            modelBuilder.Entity<CategoryModel>().HasKey(x => x.Id);
            modelBuilder.Entity<ImageModel>().HasKey(x => x.Id);
            //Foreign Key
            modelBuilder.Entity<UserModel>().HasMany(h => h.Categories).WithOne(w => w.UserModel).HasForeignKey(f => f.CreateUser);
            modelBuilder.Entity<UserModel>().HasMany(h => h.Categories).WithOne(w => w.UserModel).HasForeignKey(f => f.UpdateUser);
            modelBuilder.Entity<CategoryModel>().HasMany(h => h.Images).WithOne(w => w.CategoryModel).HasForeignKey(f => f.CategoryId);
            //Bool
            modelBuilder.Entity<ImageModel>().Property(x => x.Status).HasConversion<short>();
            modelBuilder.Entity<CategoryModel>().Property(x => x.Status).HasConversion<short>();
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
