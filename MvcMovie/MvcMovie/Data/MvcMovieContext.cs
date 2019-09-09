using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Movie> Movie { get; set; }
    }

    /*
 * The preceding code creates a DbSet<Movie> property for the entity set. In Entity Framework terminology, an entity set typically corresponds to a database table. 
 * An entity corresponds to a row in the table.
 * 
 * The name of the connection string is passed in to the context by calling a method on a DbContextOptions object. 
 * For local development, the ASP.NET Core configuration system reads the connection string from the appsettings.json file.
 */
}
