using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class NewMovieFormViewModel
    {
        // voi tehdä myös List, mutta IEnumerable tarjoaa enemmän ominaisuuksia
        public IEnumerable<Genre> Genres { get; set; }

        public Movie Movie { get; set; }

        // haetaan title, jos elokuva löytyy ja id on jo olemassa niin otsikoksi -> edit
        // jos ei niin otsikoksi -> new
        public string Title
        {
            get
            {
                if (Movie != null && Movie.Id != 0)
                    return "Edit Movie";

                return "New Movie";
            }
        }
    }
}