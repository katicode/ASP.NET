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
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        // pikanäppäin=ctor, constructor = muodostin
        // oletusmuodostin
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
        }

        // GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            // haetaan yhden elokuvan tiedot
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            // jos elokuvaa ei löydy niin not found
            if (movie == null)
                return NotFound();

            // muutoin palautetaan movie as an argument to this method
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies
        [HttpPost] // toiminto suoritetaan vain jos tulee post request
        public IHttpActionResult CreateMovie(MovieDto movie)
        {
            // jos kaikki ei ole ok niin badrequest
            if (!ModelState.IsValid)
                return BadRequest();

            // dto pitää palauttaa domain objektiksi
            var newMovie = Mapper.Map<MovieDto, Movie>(movie);

            // jos ok niin lisätään elokuva
            _context.Movies.Add(newMovie);
            // ja tallennetaan muutokset. huom. ilman saveChanges elokuva ei tallennu tietokantaan!
            _context.SaveChanges();

            // elokuvalla on nyt tietokannan luoma id
            // lisätään id dto:hon ja palautetaan to the client
            movie.Id = newMovie.Id;

            // lopuksi palautetaan movie-objekti
            // Request.RequestUri -> saadaan tarvittava uri = unified resource identifier, uri = using system (namespace);
            // esim. api/movies/10
            return Created(new Uri(Request.RequestUri + "/" + newMovie.Id), movie);
        }

        // tietojen päivitys, PUT
        // PUT /api/movies/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movie) // tämä ei palauta mitään
        {
            // jos kaikki ei ole ok niin badrequest
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // haetaan kyseisen movieid:n tiedot tietokannasta
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            // jos id:tä ei löydy tietokannasta niin not found
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // jos objekti on jo olemassa (movieInDb) niin se voidaan antaa toisena parametrina
            // jos ei niin voitaisiin käyttää var x = Mapper.Map.....(movie)
            // movieInDb on ladattu tietokannasta, mapper.map vertaa tietoja from source(movie) to target (movieInDb)
            Mapper.Map(movie, movieInDb);

            // ja vielä tallennus
            _context.SaveChanges();
        }

        // DELETE /api/movies/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            // haetaan kyseisen movieid:n tiedot tietokannasta
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            // jos id:tä ei löydy tietokannasta niin not found
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // poistetaan objekti muistista, tämä ei poista sitä vielä tietokannasta
            _context.Movies.Remove(movieInDb);

            // poiston tallennus tietokantaan 
            _context.SaveChanges();
        }
    }
}
