using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankingApp.Models;

namespace BankingApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Customers()
        {
            BankingDBEntities entity = new BankingDBEntities();
            var customers = entity.tblCustomers.ToList();

            return View(customers);
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

        public ActionResult Customer(int id)
        {
            BankingDBEntities entity = new BankingDBEntities();
            var customer = entity.tblCustomers.Where(p=>p.Id == id).FirstOrDefault();

            return View(customer);
        }

        public ActionResult Transfer(int id)
        {
            BankingDBEntities entity = new BankingDBEntities();
            var customer = entity.tblCustomers.Where(p => p.Id == id).FirstOrDefault();

            ViewBag.CustomersList = entity.tblCustomers.ToList();

            return View(customer);
        }

        [HttpPost]
        public ActionResult Transfer2(int? fromTransfer,int? toTransfer, decimal? amount)
        {

             
            BankingDBEntities entity = new BankingDBEntities();
            var fromCustomer = entity.tblCustomers.Where(p => p.Id == fromTransfer).FirstOrDefault();
            fromCustomer.Balance = fromCustomer.Balance - amount;


            var toCustomer = entity.tblCustomers.Where(p => p.Id == toTransfer).FirstOrDefault();
            toCustomer.Balance = toCustomer.Balance + amount;
            
            entity.SaveChanges();
           
            return RedirectToAction("Index");
        }
    }
}