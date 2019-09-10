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
        // tietokantayhteys
        private ApplicationDbContext _context;

        // dbcontext = disposable object, kertakäyttöinen objekti
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        // GET: Movies/Details
        public ActionResult Details(int id)
        {
            // c = Customer instanssi (voisi käyttää myösesim. foreach)
            // SingleOrDefault palauttaa nollan jos elokuvia ei löydy
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            // jos id:tä ei ole olemassa eli mennään osoitteeseen details/100 niin palautetaan HttpNotFound
            if (movie == null)
                return HttpNotFound();

            //palautetaan movie
            return View(movie);
        }

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
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            // jos elokuvaa ei löydy tietokannasta niin palautetaan 404
            if (movie == null)
                return HttpNotFound();

            var viewModel = new NewMovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("New", viewModel);
        }

        // Tätä actionia kutsutaan kun navigoidaan /movies -osoitteeseen
        // int? -> tekee kyseisestä parametrista valinnasen (optional, tehdään siitä nullable)
        // string sortBy = on C# reference type = nullable jo valmiiksi
        
/* Tämä index kommentteihin -> teen uuden
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
*/

        // regex = regular expression
        // jos tarvitsee määritellä lisää rajauksia niin googlaa: ASP.NET MVC Attribute Route Constraints
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ViewResult New()
        {
            // haetaan genret tietokannasta
            var genres = _context.Genres.ToList();

            var viewModel = new NewMovieFormViewModel
            {
                Genres = genres
            };
            
            return View("New", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.Genre = movie.Genre;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }

    
}