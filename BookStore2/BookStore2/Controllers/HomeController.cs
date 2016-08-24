using BookStore2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore2.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();
        public ActionResult Index()
        {
            return View(db.Books);
        }

        public ActionResult BookView(int id)
        {
            var book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            return RedirectToAction("Index");
        }

        //Купить книгу
        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Buy(Purchase purchase)
        {
           /* if (ModelState.IsValid)
            {*/
                purchase.Date = DateTime.Now;
                // добавляем информацию о покупке в базу данных
                //db.Purchases.Add(purchase);
                db.Entry(purchase).State = System.Data.Entity.EntityState.Added;
                // сохраняем в бд все изменения
                db.SaveChanges();
                return View(purchase);
           /* }
            ViewBag.Error = "Ошибка добавления записи к базе данных покупки книгу " + purchase.BookId;
            return View("Error");*/
        }

        //Редактирование данных в БД
        [HttpGet] //выбираем данных из базы по определенным ID
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost] //Сохраним изменения в базу данных или UPDATE 
        public ActionResult EditBook(Book book)
        {
            db.Entry(book).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();  //Выполняет SQL выражения UPDATE
            return RedirectToAction("Index");
        }

        //Добавление модели в БД
        [HttpGet]
        public ActionResult Create() //возвращает пользователю представление с формой для добавления
        {
            return RedirectToAction("Create", "Book");
        }

        [HttpPost]
        public ActionResult Create(Book book) //принимает данные этой формы
        {
            if (ModelState.IsValid)
            {
                // db.Books.Add(book); //этот код и ниже одинаковы
                db.Entry(book).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges(); //Выполняет SQL выражения INSERT
                return RedirectToAction("Index");
            }
            return View(book);
        }

        //Удаление модели из БД
       /* 
        //с такими методами тоже можно удалять, но они уязвимы в плане безопасности
        public ActionResult Delete(int id)
        {
            Book b = db.Books.Find(id);
            if (b != null)
            {
                db.Books.Remove(b);
                db.SaveChanges(); //Выполняет SQL выражения DELETE
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int id)
        {
            Book b = new Book { Id = id };
            db.Entry(b).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges(); //Выполняет SQL выражения DELETE

            return RedirectToAction("Index");
        }
        */
        //метод передает удаляемую модель в представление
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Book b = db.Books.Find(id);
            if(b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }

        //Атрибут ActionName("Delete") указывает, что метод DeleteConfirmed будет восприниматься как действие Delete. 
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Books.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //скачать файл с сервера
        public FileResult GetFile()
        {
            // Путь к файлу
            string file_path = Server.MapPath("~/Files/changed.txt");
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно
            string file_name = "changed.txt";
            return File(file_path, file_type, file_name);
        }

        // Отправка массива байтов
        public FileResult GetBytes()
        {
            string path = Server.MapPath("~/Files/changed.txt");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string file_type = "application/txt";
            string file_name = "changed.txt";
            return File(mas, file_type, file_name);
        }

        // Отправка потока
        public FileResult GetStream()
        {
            string path = Server.MapPath("~/Files/changed.txt");
            // Объект Stream
            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "application/txt";
            string file_name = "changed.txt";
            return File(fs, file_type, file_name);
        }

        //Контекст запроса информация о пользователе
        public string Info()
        {
            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
