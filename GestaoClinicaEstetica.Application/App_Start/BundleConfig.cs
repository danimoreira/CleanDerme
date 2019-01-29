using System.Web;
using System.Web.Optimization;

namespace GestaoClinicaEstetica.Application
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobstrusive.js",
                        "~/Scripts/globalize/*.js",
                        "~/Scripts/jquery.validate.globalize.js"                        
                        ));
            
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/FullCalendar/lib/moment.min.js",
                      "~/Scripts/jquery.mask.js",
                      "~/Scripts/select2.full.min.js",
                      "~/Scripts/toastr.min.js",
                      "~/Scripts/Datatables/datatables.min.js",                      
                      "~/Scripts/FullCalendar/fullcalendar.min.js",
                      "~/Scripts/FullCalendar/locale/pt-br.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/select2.min.css",
                      "~/Content/toastr.min.css",
                      "~/Scripts/Datatables/datatables.min.css",
                      "~/Scripts/FullCalendar/fullcalendar.min.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/configuration").Include(                      
                      "~/Scripts/Configuration/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/controllers").Include(
                      "~/Scripts/Controllers/*.js"));
            
        }
    }
}
