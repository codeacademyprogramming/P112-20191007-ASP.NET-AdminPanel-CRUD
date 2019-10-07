using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminPanel.Models;

namespace AdminPanel.Controllers
{
    public class CustomerController : Controller
    {
        P112AdminDemoEntities db = new P112AdminDemoEntities();

        [HttpGet]
        // GET: Customer
        public ActionResult Index()
        {
            List<Customer> customers = db.Customers.ToList();
            return View(customers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer, HttpPostedFileBase Photo)
        {
            if (Photo != null)
            {
                string photoName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Photo.FileName;
                string filePath = Path.Combine(Server.MapPath("~/Uploads"), photoName);
                Photo.SaveAs(filePath);
                customer.Photo = photoName;
            }


            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            Customer customer = db.Customers.FirstOrDefault(c => c.Id == Id);
            db.Customers.Remove(customer);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int Id)
        {
            Customer customer = db.Customers.Find(Id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Update(Customer customer, HttpPostedFileBase Photo)
        {
            db.Entry(customer).State = EntityState.Modified;

            if (Photo != null)
            {
                string photoName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Photo.FileName;
                string filePath = Path.Combine(Server.MapPath("~/Uploads"), photoName);
                Photo.SaveAs(filePath);
                customer.Photo = photoName;
            }
            else
            {
                db.Entry(customer).Property(c => c.Photo).IsModified = false;

            }



            db.SaveChanges();

            return RedirectToAction("Index");

        }


    }
}