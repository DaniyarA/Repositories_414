using BookStore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore2.Controllers
{
    public class BookController : Controller
    {
        BookContext db = new BookContext();
        public ActionResult Index()
        {
            return View(db.Books);
        }

        //
        // GET: /Book/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Book/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Book/Create

        [HttpPost]
        public ActionResult Create(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // db.Books.Add(book); //этот код и ниже одинаковы
                    db.Entry(book).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges(); //Выполняет SQL выражения INSERT
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Book/Edit/5

        public ActionResult Edit(int? id)
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

        //
        // POST: /Book/Edit/5

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            try
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();  //Выполняет SQL выражения UPDATE
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
