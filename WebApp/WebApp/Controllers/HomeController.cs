using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}