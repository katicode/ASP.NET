using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // ValidationAttribute vaatii
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        // alla oleva pätkä löytyy helposti kun kirjoittaa override IsValid ja valkkaa siitä tokan (sarkaimella "enter")
        // validation context = membership type
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // objectinstance = gives us access to containing class, joka tässä tapauksessa customer
            // objekti pitää sitoa customeriin eli var customer = (Customer)
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.Unknown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo) // 0 = unknown, 1 = pay as you go
            {
                return ValidationResult.Success;
            }

            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birthdate is required.");
            }

            // lasketaan käyttäjän ikä
            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            // jos ikä on 18 tai enemmän niin success, muutoin teksti käyttäjälle
            // alla olevat 3 riviä voisivat olla yhtä pötköä, mutta parempi lukea näin
            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be at least 18 year old to go on a membership.");
        }
    }
}