using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Util;
namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        BookContext db = new BookContext();
       /* public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все полученный объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View();
        }
        */
         //Второй метод получения данных из DB
        /*public ActionResult Index()
        {
            ViewBag.Message = "Это частичное представление от Index.cshtml";
            return View(db.Books);
        }
        */
        public ActionResult Index()
        {
            IEnumerable<Book> books = db.Books;
            //SelectList books = new SelectList(db.Books, "Author", "Name");
            ViewBag.Books = books;
            return View();
        }
        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = getToday();
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
        }

        private DateTime getToday()
        {
            return DateTime.Now;
        }

        [HttpPost]
        public string Square(int a, int h)
        {
            double s = a * h / 2;
            return "<h2>Площадь треугольника с основанием " + a + " и высотой " + h + " равна " + s + "</h2>";
        }
        /*public string Square()
        {
            int a = Int32.Parse(Request.Params["a"]);
            int h = Int32.Parse(Request.Params["h"]);
            double s = a * h / 2;
            return "<h2>Площадь треугольника с основанием " + a + " и высотой " + h + " равна " + s + "</h2>";
        }*/

        
        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2>Привет мир!</h2>");
        }

        public ActionResult GetImage()
        {
            string path = "../Images/vs.png";
            return new ImageResult(path);
        }

        public ActionResult Partial()
        {
            return PartialView();
        }

        [HttpPost]
        public string Index(string[] countr)
        {
            string result = "";
            foreach (string c in countr)
            {
                result += c;
                result += ";";
            }
            return "Вы выбрали: " + result;
        }
    }
}
