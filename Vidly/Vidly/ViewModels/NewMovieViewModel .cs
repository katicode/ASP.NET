using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // tämä piti lisätä kun tuotiin Movie-luokan muuttujat tänne
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class NewMovieFormViewModel
    {
        // voi tehdä myös List, mutta IEnumerable tarjoaa enemmän ominaisuuksia
        public IEnumerable<Genre> Genres { get; set; }

        // ? = nullable
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte? GenreId { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)] //1-20kpl voi olla varastossa
        public byte? NumberInStock { get; set; }


        // haetaan title, jos elokuva löytyy ja id on jo olemassa niin otsikoksi -> edit
        // jos ei niin otsikoksi -> new
        public string Title
        {
            get
            {
                // Movie property poistettu joten tähän riittää nyt pelkkä id
                // voisi olla myös muodossa: 
                // return Id != 0 ? "Edit Movie" : "New Movie";
                if (Id != 0)
                    return "Edit Movie";

                return "New Movie";
            }
        }

        // kirjoita ctor ja 2x sarkain -> automaattinen contructor
        // default constructor, käytetään uuden movien luomisessa (kun mitään parametria ei lähetetä)
        public NewMovieFormViewModel()
        {
            // id nollaksi jotta formin hidden field on täytetty
            Id = 0;
        }
        // tämä toinen ottaa movie objectin
        // eli jos lähetetään movie objects niin käytetään tätä konstruktoria
        public NewMovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}