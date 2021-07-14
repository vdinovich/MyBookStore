using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookStore.Models;

namespace MyBookStore.Controllers
{
    public class MyBookStoreController : Controller
    {
        MyBookContext db = new MyBookContext();
        // GET: MyBookStore
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Book> books = db.Books;
            ViewBag.Books = books;
            return View();
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buy(Purchase purchase, int id)
        {
            purchase.Date = DateTime.Now;
            purchase.BookId = id;
            ViewBag.BookId = id;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            //return "Спасибо за покупку, " + purchase.Person + "!";
            return RedirectToAction("PurchaseList");
            //return View();
        }
        [HttpGet]
        public ActionResult PurchaseList()
        {
            IEnumerable<Purchase> purchases = db.Purchases;
            ViewBag.Purchases = purchases;
            return View();
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            // CREATE
            db.Books.Add(book);
            // сохраняем в бд все изменения
            db.SaveChanges();
            //return "Спасибо за покупку, " + purchase.Person + "!";
            return RedirectToAction("Index");
            //return View();
        }

        //[HttpGet]
        //public ActionResult BookList()
        //{
        //    IEnumerable<Book> books = db.Books;
        //    ViewBag.Books = books;
        //    return View();
        //}
        [HttpGet]
        public ActionResult BookList()
        {
            using (MyBookContext db = new MyBookContext())
            {
                return View(db.Books.ToList());
            }
        }

        //[HttpGet]
        //public ActionResult EditBook(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult EditBook(Book book, int id)
        //{

        //    db.SaveChanges();
        //    return View();
        //}

        public ActionResult EditBook(int id)
        {
            using (MyBookContext db = new MyBookContext())
            {
                return View(db.Books.Where(x => x.Id == id).FirstOrDefault());
            }
            //return View();
        }

        // POST: Customer/Edit
        [HttpPost]
        public ActionResult EditBook(int id, Book book)
        {
            try
            {
                using (MyBookContext db = new MyBookContext())
                {
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("BookList");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DetailsBook(int id)
        {
            using (MyBookContext db = new MyBookContext())
            {
                return View(db.Books.Where(x => x.Id == id).FirstOrDefault());
            }
            //return View();
        }
        //#region BookDelete:
        // GET: Customer/Delete
        [HttpGet]
        public ActionResult DeleteBook(int id)
        {
            using (MyBookContext db = new MyBookContext())
            {
                return View(db.Books.Where(x => x.Id == id).FirstOrDefault());
            }
        }
        
        // POST: Customer/Delete
        [HttpPost]
        public ActionResult DeleteBook(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (MyBookContext db = new MyBookContext())
                {
                    Book book = db.Books.Where(x => x.Id == id).FirstOrDefault();
                    db.Books.Remove(book);
                    db.SaveChanges();
                }
                return RedirectToAction("BookList");
            }
            catch
            {
                return View();
            }
        }
      //        SignIn logic
           [HttpGet]
           public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(User user)
        {
            var obj = db.Users.Where(x => x.Login.Equals(user.Login) && x.Password.Equals(user.Password)).FirstOrDefault();
            if (user.Login == "admin" && user.Password == "admin")
            {
                return RedirectToAction("BookList");
            }
            else if (obj != null)
            {
                return RedirectToAction("Index");
            }    
            else
            {
                ViewBag.LoginIncorrect = "Ваш логин/пароль были введены неправильно. Попытайтесь еще раз.";
            }
            
            return View(); 
        }      
            
                
    }
}


