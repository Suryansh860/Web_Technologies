using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment_1.Controllers
{
    public class CodeController : Controller
    {
        private northwindEntities db = new northwindEntities();
        // GET: Code
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllCustomersInGermany()
        {
            var germany_customers = db.Customers.Where(c => c.Country == "Germany").ToList();
            return View(germany_customers);
        }

        public ActionResult Customer_Details()
        {
            var details = db.Orders.FirstOrDefault(o => o.OrderID == 10248)?.Customer;
            return View(details);
        }
    }
}