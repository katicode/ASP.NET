using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
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
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        // GET /api/customers/1
        public CustomerDto GetCustomer(int id)
        {
            // haetaan yhden asiakkaan tiedot
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // jos asiakasta ei löydy niin not found
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // muutoin palautetaan customer as an argument to this method
            return Mapper.Map<Customer, CustomerDto>(customer);
        }

        // POST /api/customers
        [HttpPost] // toiminto suoritetaan vain jos tulee post request
        public CustomerDto CreateCustomer(CustomerDto customer)
        {
            // jos kaikki ei ole ok niin badrequest
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // dto pitää palauttaa domain objektiksi
            var newCustomer = Mapper.Map<CustomerDto, Customer>(customer);

            // jos ok niin lisätään asiakas
            _context.Customers.Add(newCustomer);
            // ja tallennetaan muutokset. huom. ilman saveChanges asiakas ei tallennu tietokantaan!
            _context.SaveChanges();

            // asiakkaalla on nyt tietokannan luoma id
            // lisätään id dto:hon ja palautetaan to the client
            customer.Id = newCustomer.Id;

            // lopuksi palautetaan customer-objekti
            return customer;
        }

        // tietojen päivitys, PUT
        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customer) // tämä ei palauta mitään
        {
            // jos kaikki ei ole ok niin badrequest
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // haetaan kyseisen asiakasid:n tiedot tietokannasta
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // jos id:tä ei löydy tietokannasta niin not found
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // jos objekti on jo olemassa (customerInDb) niin se voidaan antaa toisena parametrina
            // jos ei niin voitaisiin käyttää var x = Mapper.Map.....(customer)
            // customerInDb on ladattu tietokannasta, mapper.map vertaa tietoja from source(customer) to target (customerInDb)
            Mapper.Map(customer, customerInDb);

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
