using System;
using System.Data.Entity; // jotta include toimii, rivi 31
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models; // jotta Customers luokka löydetään
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // tietokantayhteys
        private ApplicationDbContext _context;

        // dbcontext = disposable object, kertakäyttöinen objekti
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ViewResult Index()
        {
            // luodaan objekti customers ja asetetaan sen sisällöksi tietokannan Customers ToList
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        // GET: Customers/Details
        public ActionResult Details(int id)
        {
            // c = Customer instanssi (voisi käyttää myös esim. foreach)
            // SingleOrDefault palauttaa nollan jos asiakkaita ei löydy
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            // jos id:tä ei ole olemassa eli mennään osoitteeseen details/100 niin palautetaan HttpNotFound
            if (customer == null)
                return HttpNotFound();

            //palautetaan Customer
            return View(customer);
        }
        public ActionResult New()
        {
            // haetaan membershiptypes tietokannasta
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                // customer pitää sisällyttää myös, koska Id viedään CustomerFormissa hidden-kenttänä Save Actionille
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm" ,viewModel);
        }

        // F9 pikanäppäin breakpointille

        // metodiin voidaan mennä ainoastaan postilla
        // model binding, mvc framework binds this model (customer) to request data
        // Save tallentaa tai päivittää asiakkaan tiedot, riippuen löytyykö id jo tietokannasta
        [HttpPost]
        [ValidateAntiForgeryToken]// MVC hoitaa kaiken validoinnin -> token'
        // eli tänne token (yllä) sekä helper metodi viewseihin
        public ActionResult Save(Customer customer)
        {
            // virheenkäsittelijä, jos jokin tallennuksessa menee vikaan, palautetaan käyttäjä sivulle
            // muista myös luokkiin validoinnit
            if (!ModelState.IsValid)
            {
                // asetetaan viewModel ja sisällöksi Save-Actionille lähetetty customer-objekti
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                // kun customer lisätty _context:iin niin tiedot ovat muistissa mutta eivät vielä tietokannassa
                _context.Customers.Add(customer);
            else
            {
                // single-metodi palauttaa exceptionin jos asiakasta ei löydy
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                // tietojen päivitys tietokantaan
                // TryUpdateModel päivittäisi kaikki tiedot, sitä ei yleensä haluta
                // AutoMapper voisi toimia myös
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }

            // tallennetaan kaikki, jos onnistuu. jos tulee virhe niin mitään ei tallenneta
            _context.SaveChanges();

            // toiminnan jälkeen palautetaan asiakas -> customers/index
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            // haetaan tietokannasta Customers taulusta id
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            //jos id:tä ei löydy niin palautetaan virhe 404
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };


            return View("CustomerForm", viewModel);
        }

    }
}