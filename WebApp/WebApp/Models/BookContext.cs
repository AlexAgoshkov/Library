using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Autor> Autors { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>().HasMany(b => b.Books)
                .WithMany(a => a.Autors)
                .Map(t => t.MapLeftKey("autorID")
                .MapRightKey("bookID")
                .ToTable("AutorBook"));

            modelBuilder.Entity<User>().HasMany(b => b.Books)
                .WithMany(u => u.Users)
                .Map(t => t.MapLeftKey("userID")
                .MapRightKey("bookID")
                .ToTable("UserBook"));
        }
    }

    public class Autor : Person
    {
        public Autor(string name, string lastName)
        {
            this.Name = name;
            this.LastName = lastName;
            Books = new List<Book>();
        }

        public virtual ICollection<Book> Books { get; set; }

        public Autor()
        {
            Books = new List<Book>();
        }
    }

    public class Person
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }

    public class Book
    {
        public Book(string title)
        {
            this.Name = title;
            Users = new List<User>();
            Autors = new List<Autor>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }

        public virtual ICollection<Autor> Autors { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Book()
        {
            Users = new List<User>();
            Autors = new List<Autor>();
        }
    }

    public class User : Person
    {
        public virtual ICollection<Book> Books { get; set; }
        public User()
        {
            Books = new List<Book>();
        }


    }
}