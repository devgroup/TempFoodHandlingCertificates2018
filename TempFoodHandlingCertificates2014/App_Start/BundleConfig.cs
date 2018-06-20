using System.Web;
using System.Web.Optimization;

namespace TempFoodHandlingCertificates2014
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            //Start of modification to RegisterBundles in order to add JQueryUI to use datetime picker
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-{version}.js"));
            //End of custom modification to RegisterBundles

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"                      
                        ));

            bundles.Add(new ScriptBundle("~/bundles/analytics").Include(
                        "~/Scripts/GAnalyticsTracker.js" 
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/overrideBootstrap.css",
                //Additional styles added for datepicker.css
                    //  "~/Content/themes/base/datepicker.css",
                     //  "~/Content/themes/base/core.css",
                      // "~/Content/themes/base/theme.css",
                       "~/Content/jquery-ui.css"
                       //"~/Content/jquery-ui.theme.css",
                      //  "~/Content/jquery-ui.structure.css"
                      //Our ugly style - thanks Seamless
                      // "~/Content/HCCWeb.css"

                      ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
