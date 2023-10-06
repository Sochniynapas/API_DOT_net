using API_dot.net.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API_dot.net.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {


        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(

                new Villa()
                {
                    Id = 1,
                    Name = "Royall Villa",
                    Squares = 100,
                    Сapacity = 40,
                    ImageUrl = "https://unsplash.com/photos/2pPw5Glro5I",
                    CreatedDate = DateTime.Now,
                    
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Medium Villa",
                    Squares = 70,
                    Сapacity = 25,
                    ImageUrl = "https://unsplash.com/photos/2pPw5Glro5I",
                    CreatedDate = DateTime.Now,
                },
                new Villa()
                {
                    Id = 3,
                    Name = "Low Villa",
                    Squares = 40,
                    Сapacity = 10,
                    ImageUrl = "https://unsplash.com/photos/2pPw5Glro5I",
                    CreatedDate = DateTime.Now,
                }) ;
        }

    }
}
