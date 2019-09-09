/*Controllers: Classes that handle browser requests. 
They retrieve model data and call view templates that return a response. 
In an MVC app, the view only displays information; the controller handles and responds to user input and interaction. 
For example, the controller handles route data and query-string values, and passes these values to the model. 
The model might use these values to query the database. 
For example, https://localhost:1234/Home/About has route data of Home (the controller) and About (the action method to call on the home controller). 
https://localhost:1234/Movies/Edit/5 is a request to edit the movie with ID=5 using the movie controller. Route data is explained later in the tutorial.
 */

using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/

        /*alkuperäinen index
        public string Index()
        {
            return "This is my default action...";
        }
        */

        public IActionResult Index()
        {
            return View();
        }
        // The preceding code calls the controller's View method. It uses a view template to generate an HTML response. 
        //Controller methods (also known as action methods), such as the Index method above, generally return an IActionResult (or a class derived from ActionResult), not a type like string.


        /*
        **alkuperäinen Welcome, alempana olevaan action methodiin lisätty kaksi parametria
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }

        // GET: /HelloWorld/Welcome/ 
        // Requires using System.Text.Encodings.Web;

        **toinen Welcome, alempana vielä kolmas
        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
            // Uses HtmlEncoder.Default.Encode to protect the app from malicious input 
        }
        

        // Every public method in a controller is callable as an HTTP endpoint (targetable URL in the web application). 
        //In the sample above, both methods return a string. Note the comments preceding each method.

        public string Welcome(string name, int ID = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        }
        */

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}