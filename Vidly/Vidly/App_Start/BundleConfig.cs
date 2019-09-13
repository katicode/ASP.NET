using System.Web;
using System.Web.Optimization;

namespace Vidly
{
    /* Määritetään which bundles are client side assets
     * Voidaan esimerkiksi liittää useita javascript tai css -tiedostoja paketiksi/nipuksi (into a bundle)
     * Sitten ei tarvitse tehdä niin montaa http-requestia -> nopeampi sivun latautuminen
     */
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // yhdistetään jquery ja bootstrap niput/bundles -> scriptbundle nimeltä lib
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        // Lisätty bootbox scriptit
                        // Ensin asennettu komennolla "install-package bootbox -version:4.3.0"
                        "~/Scripts/bootbox.js",
                        // lisätään datatables mitä käytetään
                        // ensin asennettu komennolla "install-package jquery.datatables -version:1.10.11"
                        "~/scripts/datatables/jquery.datatables.js",
                        "~/scripts/datatables/datatables.bootstrap.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/content/datatables/css/datatables.bootstrap.css")); // datatables css

        }
    }
}
