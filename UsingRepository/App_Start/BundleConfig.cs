using System.Web.Optimization;

namespace UsingRepository
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-1.7.2.min.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootbox.js",
                "~/Scripts/superfish.js",
                "~/Scripts/jquery.scrolltotop.js",
                "~/scripts/typeahead.bundle.js",
                "~/scripts/datatables/jquery.dataTables.js",
                "~/scripts/datatables/dataTables.bootstrap.js",
                "~/scripts/typeahead.bundle.js",
                "~/scripts/jquery-ui.js",
                "~/Scripts/jquery.fancybox.js",
                "~/Scripts/common.js",
                "~/Scripts/jquery.flexslider-min.js",
                "~/Scripts/ckeditor/ckeditor.js",
                "~/Scripts/toastr.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/bootbox.min.js",
                "~/Scripts/owl.carousel.min.js",
                "~/Scripts/aos.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/app.js",
                "~/Scripts/app/Controllers/showImageController.js",
                "~/Scripts/app/Services/cartService.js",
                "~/Scripts/app/Controllers/cartAddController.js",
                "~/Scripts/app/Controllers/cartUpdateController.js",
                "~/Scripts/app/Services/userService.js",
                "~/Scripts/app/Controllers/UserController.js",
                "~/Scripts/app/Controllers/saveUserDetailsController.js",
                "~/Scripts/app/Services/DeleteService.js",
                "~/Scripts/app/Controllers/DeleteController.js",
                "~/Scripts/app/Services/checkoutService.js",
                "~/Scripts/app/Controllers/checkoutController.js",
                "~/Scripts/app/Services/cityCountryService.js",
                "~/Scripts/app/Controllers/cityCountryController.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"
                      , "~/Content/site.css"
                      , "~/Content/flexslider.css"
                      , "~/Content/jquery-ui.css"
                      , "~/Content/jquery.fancybox.css"
                      , "~/Content/datatables/css/datatables.bootstrap.css"
                      , "~/Content/toastr.min.css"
                      , "~/Content/typeahead.css"
                      , "~/Content/all.css"
                      , "~/Content/owl.carousel.min.css"
                      , "~/owl.theme.default.min.css",
                      "~/Content/aos.css"));
        }
    }
}
