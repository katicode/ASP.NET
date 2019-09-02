using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrekki!" };

            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            
            // passing data (movie) to the view
            // ViewBag & ViewData hankalampia käyttää (nimiä pitää muokata joka paikkaan)
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }

        // Tätä actionia kutsutaan kun navigoidaan /movies -osoitteeseen
        // int? -> tekee kyseisestä parametrista valinnasen (optional, tehdään siitä nullable)
        // string sortBy = on C# reference type = nullable jo valmiiksi
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            // Jos pageIndex ei ole määritelty niin annetaan sen arvoksi 1
            if (!pageIndex.HasValue)
                pageIndex = 1;

            // Jos sortBy:tä ei ole määritelty niin sortataan nimen mukaan
            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            // Palautetaan sisältö, 0 = ensimmäinen annettu parametri eli pageIndex, 1 = toinen annettu parametri eli sortBy
            // String (isolla) vaati lisäämään ylös using.System
            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }

        // regex = regular expression
        // jos tarvitsee määritellä lisää rajauksia niin googlaa: ASP.NET MVC Attribute Route Constraints
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

    }
}