using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Repository
    {
        BookContext bk = new BookContext();

        public IEnumerable<Book> GetBooks()
        {
            
            return bk.Books;
        }

        public Book GetBook(int id)
        {
            return bk.Books.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return bk.Users;
        }

        public void AddUser(User u)
        {
            bk.Users.Add(u);
            bk.SaveChanges();
        }

   

        public User GetUser(int id)
        {
            return bk.Users.FirstOrDefault(x => x.Id == id);
        }

     

        public Autor GetAutor(int id)
        {
            return bk.Autors.FirstOrDefault(x => x.Id == id);
        }


    }
}