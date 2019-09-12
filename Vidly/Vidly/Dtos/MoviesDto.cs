using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        /* Data Transfer Object is a plain datastructure and it's used to transform data from client to server or vice versa
         * API ei saisi koskaan vastaanottaa (receive) tai palauttaa (return) domain objects (kuten juuri nyt CustomersControllerissa tehdään)
         * jos APIssa käytetään domain objects niin hakkeri pääsee helposti käsiksi ja voi lähettää esim. jsonilla sinne tietoja
         * DTOlla voidaan määritellä ne objektit, joita saa päivittää (ja joita ei)
         * Aina kun APIssa palautetaan (return) Customer-luokan objekti niin se pitää map to dto first
         * Ja APIn metodit, joissa modify eli esim. luodaan (CreateCustomer) tai päivitetään (UpdateCustomer) 
         * we need to map this customer dto properties back to our customer object -> esim. Auto mapper
         */

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public byte GenreId { get; set; }

        // viittaus Genre-luokkaan
        public GenreDto Genre { get; set; }


        public DateTime DateAdded { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)] //1-20kpl voi olla varastossa
        public byte NumberInStock { get; set; }


    }
}