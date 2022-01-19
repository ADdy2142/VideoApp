using Project.Infrastructure;
using Project.Models;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Context
{
    // بخش اصلی کار با پایگاه داده
    public class ApplicationDbContext : DbContext
    {
        // خصوصیات زیر جداول پایگاه داده و روابط بین آنها را ترسیم می کنند
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Project.Models.History> Histories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<UserVideo> UserVideos { get; set; }
        public DbSet<VideoGenre> VideoGenres { get; set; }
        public DbSet<VideoHistory> VideoHistories { get; set; }

        // سازنده
        public ApplicationDbContext() : base("ApplicationDbContext")
        {
        }

        // این متد هنگامی فراخوانی می شود که مدل ها در پایگاه داده شروع به ساختن شوند
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ApplicationDbContext>(modelBuilder);
            //var sqliteConnectionInitializer = new SqliteDropCreateDatabaseWhenModelChanges<ApplicationDbContext>(modelBuilder);
            //var sqliteConnectionInitializer = new SqliteDropCreateDatabaseAlways<ApplicationDbContext>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}