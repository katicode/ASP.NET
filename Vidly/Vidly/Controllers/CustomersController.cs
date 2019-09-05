using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models; // jotta Customers luokka löydetään

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ViewResult Index()
        {
            // luodaan objekti customers ja asetetaan sen sisällöksi GetCustomers
            var customers = GetCustomers();

            return View(customers);
        }

        // GET: Customers/Details
        public ActionResult Details(int id)
        {
            // c = Customer instanssi (voisi käyttää myös esim. foreach)
            // SingleOrDefault palauttaa nollan jos asiakkaita ei löydy
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            // jos id:tä ei ole olemassa eli mennään osoitteeseen details/100 niin palautetaan HttpNotFound
            if (customer == null)
                return HttpNotFound();

            //palautetaan Customer
            return View(customer);
        }

        // luodaan lista, joka viedään Index-metodille ja palautetaan siellä View:lle
        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "John Smith" },
                new Customer { Id = 2, Name = "Mary Williams" }
            };
        }
    }
}