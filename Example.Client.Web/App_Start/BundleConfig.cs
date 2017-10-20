using System.Web;
using System.Web.Optimization;

namespace Example
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/react").Include(
                        "~/Content/Javascript/Utils/Array.js",
                        "~/Content/Javascript/Utils/Ajax.js",
                        "~/Content/Javascript/Controls/Forms/Textbox.jsx",
                        "~/Content/Javascript/Controls/ErrorMessage.jsx",
                        "~/Content/Javascript/Controls/AddStuff.jsx",
                        "~/Content/Javascript/Controls/SearchBar.jsx",
                        "~/Content/Javascript/Controls/Stuff.jsx",
                        "~/Content/Javascript/Controls/StuffList.jsx",
                        "~/Content/Javascript/Pages/ReactPage.jsx"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Css/bootstrap.css",
                      "~/Content/Less/site.css"));
        }
    }
}
