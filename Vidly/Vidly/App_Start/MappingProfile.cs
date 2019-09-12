using AutoMapper; // Profile rivillä 13 vaatii tämän
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Dtos; // tarvitaa, jotta CustomerDto löytyy
using Vidly.Models; // tarvitaan, jotta Customer löytyy

namespace Vidly.App_Start

    /* Ennen tämän luokan tekoa asennettu automapper "package manager console":ssa
     * komennolla "install-package automapper -version:4.1"
     */
{
    public class MappingProfile : Profile
    {
        // constructor, muodostin -> pikaluonti ctor + 2x sarkain
        public MappingProfile()
        {
            /* CreateMap-metodi: Automapper käyttää reflection to scan these types, find their properties 
             * and maps them based on their name. Automapper = convension-based mapping tool
             */
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>();

            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>();
        }
    }
}