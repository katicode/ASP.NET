using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // vie käyttäjän virhesivulle kun jokin toiminto tuottaa poikkeuksen
            filters.Add(new HandleErrorAttribute());

            // koko ohjelman kattava filtteri, mihinkään ei pääse jos ei ole kirjautunut sisään
            filters.Add(new AuthorizeAttribute());
        }
    }
}
