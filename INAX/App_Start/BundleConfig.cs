using System.Web;
using System.Web.Optimization;

namespace INAX
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
            bundles.Add(new ScriptBundle("~/Content/Display/Js/Scripts").Include(
                       "~/Content/Display/Js/jquery.prettyPhoto.js",
                       "~/Content/Display/Js/jquery.nivo.slider.pack.js",
                       "~/Content/Display/Js/jquery.jcarousel.min.js", "~/Scripts/jquery.popup.js", "~/Content/Display/Js/jquery.mmenu.min.all.js"


                       ));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.UseCdn = true;
            var jqueryCdnPath = "https://apis.google.com/js/plusone.js";

            bundles.Add(new ScriptBundle("~/Content/Display/Js/Google",
                        jqueryCdnPath).Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/Display/Css/Style").Include(
                "~/Content/Display/Css/jquery.mmenu.all.css",
                "~/Content/Display/Css/Index.css",
                "~/Content/Display/Css/Index_Res.css",
                "~/Content/Display/Css/Contact.css",
                "~/Content/Display/Css/linhnguyen.css",
                "~/Content/Display/Css/Listnews.css",
                "~/Content/Display/Css/ListProduct.css",
                "~/Content/Display/Css/Login.css",
                "~/Content/Display/Css/Maps.css",
                "~/Content/Display/Css/New_Detail.css",
                "~/Content/Display/Css/prettyPhoto.css",
                "~/Content/Display/Css/Order.css",
                "~/Content/Display/Css/Order_res.css",
                "~/Content/Display/Css/Product_Detail.css",
                "~/Content/Display/Css/Product_Detail_Res.css",
                "~/Content/Display/Css/Product_Res.css",
                "~/Content/Display/Css/ProductDetail.css",
"~/Content/Display/Css/Baogia.css",
"~/Content/Display/Css/Baogia_Rs.css",
                "~/Content/Display/Css/skin.css",
                "~/Content/Display/Css/Slide/default.css",
                "~/Content/Display/Css/Slide/nivo-slider.css",
                "~/Content/PagedList.css",
                "~/Content/themes/base/datepicker.css",
                "~/Content/Display/Css/Slide.css",
                "~/Content/Display/Css/Slide/style.css",
                "~/Content/themes/base/jquery.ui.all.css",
                "~/Content/themes/base/all.css", "~/Content/themes/base/jquery.ui.datepicker.css", "~/Content/Display/Css/ListProduct_Res.css", "~/Content/Display/Css/New_Res.css", "~/Content/Display/Css/Slideshow.css"

                ));
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
            BundleTable.EnableOptimizations = true;
        }

    }
}