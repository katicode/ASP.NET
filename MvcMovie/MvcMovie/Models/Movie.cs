using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class Movie
    {
        /*Määrittelyt tässä = luokan sisällä!
         * A significant benefit is that you didn't need to change a single line of code in the MoviesController class or 
         * in the Create.cshtml view in order to enable this validation UI. 
         * The controller and views you created earlier in this tutorial automatically picked up the validation rules 
         * that you specified by using validation attributes on the properties of the Movie model class.
         */

        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(5)]
        [Required]
        public string Rating { get; set; }

        /* Contains:
         * The Id field which is required by the database for the primary key.
         * [DataType(DataType.Date)]: The DataType attribute specifies the type of the data (Date). With this attribute:
            - The user is not required to enter time information in the date field.
            - Only the date is displayed, not time information.
         */
    }

}
