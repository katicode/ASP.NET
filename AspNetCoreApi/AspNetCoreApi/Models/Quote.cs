using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApi.Models
{
    public class Quote

        // Models -luomisen jälkeen kannattaa aina "buildata" sovellus ennen kuin jatkaa. Että kaikki meni ok.
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}
