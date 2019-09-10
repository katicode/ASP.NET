using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)] //1-20kpl voi olla varastossa
        public byte NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }

        // viittaus Genre-luokkaan
        [Required]
        public Genre Genre { get; set; }
    }
}