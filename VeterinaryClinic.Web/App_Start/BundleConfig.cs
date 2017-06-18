using System.Web;
using System.Web.Optimization;

namespace VeterinaryClinic.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            Bundle knockoutBundle = new ScriptBundle("~/bundles/knockout")
                .Include("~/Scripts/knockout/knockout-{version}.js")
                .Include("~/Scripts/knockout/knockout.mapping-latest.js")
                .Include("~/Scripts/knockout/knockout.validation.js");
                // .Include("~/Scripts/knockout/knockout-server-side-validation.js")
                // .Include("~/Scripts/knockout/knockout.bindings.js");
            bundles.Add(knockoutBundle);

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
