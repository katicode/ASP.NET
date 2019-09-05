using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // required vaatii tämän
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        //asetetaan vaatimuksia, ylikirjoittaa defaultit, DataAnnotations
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        // Navigation property
        public MembershipType MembershipType { get; set; }

        public byte MembershipTypeId { get; set; }
    }
}