using System.Web;
using System.Web.Optimization;

namespace CMS_Project
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                      "~/Scripts/ckeditor/ckeditor.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Bundle/Website/GlobalStyles").Include(
                "~/Content/WebSite/assets/corporate/css/fonts.googleapis.css",
                "~/Content/WebSite/assets/plugins/font-awesome/css/font-awesome.css",
                "~/Content/WebSite/assets/plugins/bootstrap/css/bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/Bundle/Website/PageLevelPluginStyles").Include(
                "~/Content/WebSite/assets/pages/css/animate.css",
                "~/Content/WebSite/assets/plugins/fancybox/source/jquery.fancybox.css",
                "~/Content/WebSite/assets/plugins/owl.carousel/assets/owl.carousel.css"
                ));

            bundles.Add(new StyleBundle("~/Bundle/Website/ThemeStyles").Include(
                "~/Content/WebSite/assets/pages/css/components.css",
                "~/Content/WebSite/assets/pages/css/slider.css",
                "~/Content/WebSite/assets/corporate/css/style.css",
                "~/Content/WebSite/assets/pages/css/portfolio.css",
                "~/Content/WebSite/assets/corporate/css/style-responsive.css",
                "~/Content/WebSite/assets/corporate/css/themes/red.css",
                "~/Content/WebSite/assets/corporate/css/custom.css"
                ));

            bundles.Add(new ScriptBundle("~/Bundle/Website/CorePlugins").Include(
                "~/Content/WebSite/assets/plugins/jquery.min.js",
                "~/Content/WebSite/assets/plugins/jquery-migrate.min.js",
                "~/Content/WebSite/assets/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/WebSite/assets/corporate/scripts/back-to-top.js"
                ));

            bundles.Add(new ScriptBundle("~/Bundle/Website/PageLevelJavascripts").Include(
                "~/Content/WebSite/assets/plugins/fancybox/source/jquery.fancybox.pack.js",
                //"~/Content/WebSite/assets/plugins/owl.carousel/owl.carousel.min.js",
                "~/Content/WebSite/assets/corporate/scripts/layout.js",
                //"~/Content/WebSite/assets/plugins/jquery-mixitup/jquery.mixitup.min.js",
                "~/Content/WebSite/assets/pages/scripts/portfolio.js"//,
                //"~/Content/WebSite/assets/pages/scripts/bs-carousel.js"
                ));
            //bundles.Add(new StyleBundle("~/Content/Website/css").Include(
            //    "~/Content/WebSite/vendor/bootstrap/ltr/css/bootstrap.min.css",
            //    "~/Content/WebSite/css/one-page-wonder.css"));

            //bundles.Add(new ScriptBundle("~/Content/Website/js").Include(
            //            "~/Content/WebSite/vendor/jquery/jquery.min.js",
            //            "~/Content/WebSite/vendor/bootstrap/rtl/js/bootstrap-arabic.min.js"));
        }
    }
}