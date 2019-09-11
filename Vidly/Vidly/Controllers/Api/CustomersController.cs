using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models; // IEnumerable lista tarvitsee

namespace Vidly.Controllers.Api
    /* Kun Api on luotu niin muista konfigurointi
     * tiedosto Global.asax.cs (kohta 3)
     */

    /*Visual Studio has added the full set of dependencies for ASP.NET Web API 2 to project 'Vidly'. 

    The Global.asax.cs file in the project may require additional changes to enable ASP.NET Web API.

    1. Add the following namespace references:

        using System.Web.Http; -> nyt vain tämä piti lisätä
        using System.Web.Routing; -> tämä oli jo valmiina

    2. If the code does not already define an Application_Start method, add the following method: -> oli jo valmiina

        protected void Application_Start()
        {
        }

    3. Add the following lines to the beginning of the Application_Start method:

        GlobalConfiguration.Configure(WebApiConfig.Register);
    */
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        // pikanäppäin=ctor, constructor = muodostin
        // oletusmuodostin
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        // GET /api/customers/1
        public Customer GetCustomer(int id)
        {
            // haetaan yhden asiakkaan tiedot
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // jos asiakasta ei löydy niin not found
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // muutoin palautetaan customer
            return customer;
        }

        // POST /api/customers
        [HttpPost] // toiminto suoritetaan vain jos tulee post request
        public Customer CreateCustomer(Customer customer)
        {
            // jos kaikki ei ole ok niin badrequest
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // jos ok niin lisätään asiakas
            _context.Customers.Add(customer);
            // ja tallennetaan muutokset. huom. ilman saveChanges asiakas ei tallennu tietokantaan!
            _context.SaveChanges();

            // lopuksi palautetaan customer-objekti
            return customer;
        }

        // tietojen päivitys, PUT
        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer) // tämä ei palauta mitään
        {
            // jos kaikki ei ole ok niin badrequest
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // haetaan kyseisen asiakasid:n tiedot tietokannasta
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // jos id:tä ei löydy tietokannasta niin not found
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // jos ok niin päivitetään tiedot
            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

            // ja vielä tallennus
            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            // haetaan kyseisen asiakasid:n tiedot tietokannasta
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // jos id:tä ei löydy tietokannasta niin not found
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // poistetaan objekti muistista, tämä ei poista sitä vielä tietokannasta
            _context.Customers.Remove(customerInDb);

            // poiston tallennus tietokantaan 
            _context.SaveChanges();
        }
    }
}
