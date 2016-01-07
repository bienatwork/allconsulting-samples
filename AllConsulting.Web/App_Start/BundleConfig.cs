using System.Web.Optimization;

namespace AllConsulting.Web
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

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/Vendors/modernizr-2.8.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/Vendors/jquery-1.9.1.js",
                "~/Scripts/Vendors/bootstrap.js",
                "~/Scripts/Vendors/angular.js",
                "~/Scripts/Vendors/angular-route.js",
                "~/Scripts/Vendors/angular-cookies.js",
                "~/Scripts/Vendors/angular-ui/ui-bootstrap.js",
                "~/Scripts/Vendors/angular-ui/ui-bootstrap-tpls.js",
                "~/Scripts/Vendors/toastr.js",
                "~/Scripts/Vendors/angular-ui/ui-bootstrap.js",
                "~/Scripts/Vendors/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                "~/Scripts/spa/app.js",
                "~/Scripts/spa/services/apiService.js",
                "~/Scripts/spa/orders/ordersController.js",
                "~/Scripts/spa/orders/addOrderController.js",
                "~/Scripts/spa/directives/modal.directive.js"));
        }
    }
}