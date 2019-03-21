using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDataTableApplication.Models;

namespace MVCDataTableApplication.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopulateData()
        {
            using (DataTableAppEntities db = new DataTableAppEntities())
            {
                var custList = db.Customers.ToList<Customer>();
                return Json(new { data = custList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddCustomer(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    return View(new Customer());
                }
                else
                {
                    using (DataTableAppEntities db = new DataTableAppEntities())
                    {
                        return View(db.Customers.Where(x => x.CusomerID == id).FirstOrDefault());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult AddCustomer(Customer cust)
        {
            try
            {
                using (DataTableAppEntities db = new DataTableAppEntities())
                {
                    if (cust.CusomerID == 0)
                    {
                        db.Customers.Add(cust);
                        db.SaveChanges();
                        return Json(new { success = true, message = "New Customer successfully Addedd" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Customer myCust = new Customer();
                        myCust = db.Customers.Where(m => m.CusomerID == cust.CusomerID).FirstOrDefault();
                        myCust.ContactName = cust.ContactName;
                        myCust.CompanyName = cust.CompanyName;
                        myCust.Address = cust.Address;
                        myCust.City = cust.City;
                        myCust.Country = cust.Country;
                        myCust.Phone = cust.Phone;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Customer info updated successfully" }, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
           
            
        }

        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                using (DataTableAppEntities db = new DataTableAppEntities())
                {
                    Customer cust = db.Customers.Where(x => x.CusomerID == id).FirstOrDefault();
                    db.Customers.Remove(cust);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Customer deleted successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
             
        }
    }
}