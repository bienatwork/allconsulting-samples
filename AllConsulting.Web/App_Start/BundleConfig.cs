using System.Web.Optimization;

namespace ACAG.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //ui-bootstrap-csp.css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/toastr.css",
                "~/Content/Site.css",
                "~/Content/ui-bootstrap-csp.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/js/Vendors/modernizr-2.8.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/js/Vendors/jquery-1.9.1.js",
                "~/js/Vendors/bootstrap.js",
                "~/js/Vendors/angular.js",
                "~/js/Vendors/angular-route.js",
                "~/js/Vendors/angular-cookies.js",
                "~/js/Vendors/angular-ui/ui-bootstrap.js",
                "~/js/Vendors/angular-ui/ui-bootstrap-tpls.js",
                "~/js/Vendors/toastr.js",
                "~/js/Vendors/angular-ui/ui-bootstrap.js",
                "~/js/Vendors/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                "~/js/spa/app.js",
                "~/js/spa/services/apiService.js",
                "~/js/spa/orders/ordersController.js",
                "~/js/spa/orders/addOrderController.js",
                "~/js/spa/directives/modal.directive.js"));
        }
    }
}