using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        BookContext bookContext = new BookContext();
        Repository eF = new Repository();

        public ActionResult Index()
        {
            return View();
        }
 

        public ActionResult ShowBooks()
        {
            return View(eF.GetBooks());
        }

        public ActionResult ShowUsers()
        {
            return View(eF.GetUsers());
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public RedirectResult AddUser(User user)
        {
            eF.AddUser(user);
            return Redirect("/Home/ShowUsers");
        }

        public async Task<RedirectResult> BookHasTaken(int id, int id2)
        {
            eF.AddUserBook(id, id2);
            await Task.Run(() => eF.SendMail(id, id2));
            return Redirect("/Home/FindUser/" + id);
        }

        [HttpGet]
        public ActionResult TakeBook(int id)
        {
            ViewBag.id = id;
        
            return View(eF.GetBooks());
        }

        public ActionResult ShowAutors()
        {
            return View(eF.GetAutors());
        }

        public ActionResult ShowReaders(int id)
        {
            return View(eF.GetBook(id));
        }


        public ActionResult FindUser(int id)
        {
            return View(eF.GetUser(id));
        }

        public ActionResult FindAutor(int id)
        {
            return View(eF.GetAutor(id));
        }

        [HttpGet]
        public RedirectResult RemoveBookFromUser(int id, int id2)
        {
            eF.RemoveBookFromUser(id, id2);
            string path = "/Home/FindUser/" + id;
            return Redirect(path);
        }

        [HttpGet]
        public ActionResult AddAuthor()
        {
            return View();
        }
        [HttpPost]
        public RedirectResult AddAuthor(Autor book)
        {
            eF.AddAuthor(book);
            return Redirect("/Home/ShowAutors");
        }

        [HttpGet]
        public ActionResult AddBookByAuthor()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult AddBookByAuthor(string authorName, string authorSecondName, string bookTitle)
        {
            if (authorName != "" && authorSecondName != "" && bookTitle != "")
            {
                eF.AddBookByAuthor(authorName, authorSecondName, bookTitle);
                return Redirect("/Home/ShowBooks/");

            }
            return Redirect("/Home/AddBookByAuthor/");
        }

    }
}