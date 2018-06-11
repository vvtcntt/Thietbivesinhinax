using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace INAX
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("ListManufacturers", "9/{Tag}/{*catchall}", new { controller = "MenufacturersDisplay", action = "MenufacturerList", tag = UrlParameter.Optional }, new { controller = "^M.*", action = "^MenufacturerList$" });
            routes.MapRoute("DetailManufacturers", "NhaPhanPhoi/{Tag}/{*catchall}", new { controller = "MenufacturersDisplay", action = "MenufacturerDetail", tag = UrlParameter.Optional }, new { controller = "^M.*", action = "^MenufacturerDetail$" });
            routes.MapRoute("ListNews", "2/{tag}/{*catchall}", new { controller = "News", action = "ListNews", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^ListNews$" });
            routes.MapRoute("ChitietNew", "vn/home/tin-tuc/{tag}_{idnew}.html", new { controller = "News", action = "NewsDetail", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^NewsDetail$" });
            routes.MapRoute("NewsDetail", "tin-tuc/{tag}", new { controller = "News", action = "NewsDetail", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^NewsDetail$" });
            //Danh sách sản phẩm
            routes.MapRoute("ProductList", "vn/san-pham/{tag}", new { controller = "Product", action = "ListProduct", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ListProduct$" });
            routes.MapRoute("ProductList-0", "vn/san-pham/{tag1}/{tag}", new { controller = "Product", action = "ListProduct", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ListProduct$" });
            routes.MapRoute("ProductList-1", "0/vn/{tag1}/{tag}", new { controller = "Product", action = "ListProduct", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ListProduct$" });
            routes.MapRoute("ProductList-2", "0/vn/{tag}", new { controller = "Product", action = "ListProduct", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ListProduct$" });
            routes.MapRoute("ProductList-4", "0/{tag}", new { controller = "Product", action = "ListProduct", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ListProduct$" });            //Load chi tiết sản phẩm
            routes.MapRoute("Chi_Tiet", "vn/{tag1}/{tag2}/{tag}_{idp}.html", new { controller = "Product", action = "ProductDetail", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ProductDetail$" });
            routes.MapRoute("Chi_Tiet_2", "vn/{tag1}/{tag}_{idp}.html", new { controller = "Product", action = "ProductDetail", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ProductDetail$" });
            routes.MapRoute("Chi_Tiet_3", "{tag}-dt", new { controller = "Product", action = "ProductDetail", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ProductDetail$" });
            routes.MapRoute("ProductList-3", "{tag}.html", new { controller = "Product", action = "ListProduct", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ListProduct$" });
  routes.MapRoute("Bao-gia", "Bao-gia/{Tag}/{*catchall}", new { controller = "Baogia", action = "BaogiaDetail", tag = UrlParameter.Optional }, new { controller = "^B.*", action = "^BaogiaDetail$" });

            routes.MapRoute("Chi_Tiet-1", "1/{tag}/{*catchall}", new { controller = "Product", action = "ProductDetail", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ProductDetail$" });
            routes.MapRoute("ProductSyn", "Syn/{Tag}/{*catchall}", new { controller = "ProductSysDislay", action = "ProductSyn_Detail", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ProductSyn_Detail$" });
            routes.MapRoute("ProductSearch", "Search/{Tag}/{*catchall}", new { controller = "Product", action = "SearchProduct", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^SearchProduct$" });
            routes.MapRoute("ProductSale", "Sale/{Tag}/{*catchall}", new { controller = "Sale2018", action = "Index", tag = UrlParameter.Optional }, new { controller = "^S.*", action = "^Index$" });
            routes.MapRoute("Danh_Sach", "0/{Tag}/{*catchall}", new { controller = "Product", action = "ListProduct", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^ListProduct$" });
            routes.MapRoute("ListAgency", "4/{Tag}/{*catchall}", new { controller = "Agency", action = "AgencyList", tag = UrlParameter.Optional }, new { controller = "^A.*", action = "^AgencyList$" });
            routes.MapRoute("DetailAgency", "5/{Tag}/{*catchall}", new { controller = "AgencyDisplay", action = "AgencyDetail", tag = UrlParameter.Optional }, new { controller = "^A.*", action = "^AgencyDetail$" });
            routes.MapRoute("Danh_Sach_manufactures", "6/{Manu}/{*catchall}", new { controller = "Product", action = "ListProductManufactures", manu = UrlParameter.Optional }, new { controller = "^P.*", action = "^ListProductManufactures$" });
            routes.MapRoute(name: "Tin-tuc", url: "Tin-tuc", defaults: new { controller = "NewDisplay", action = "ListNew" });
            routes.MapRoute(name: "Hang-san-xuat", url: "Hang-san-xuat", defaults: new { controller = "ManufacturesDeplay", action = "ListManufactures" });
            routes.MapRoute(name: "He-Thong-phan-phoi", url: "He-Thong-phan-phoi", defaults: new { controller = "Agencys", action = "ListAgency" });
            routes.MapRoute(name: "Contact", url: "Lien-he", defaults: new { controller = "ContactDisplay", action = "Index" });
            routes.MapRoute(name: "SearchProduct", url: "SearchProduct", defaults: new { controller = "Products", action = "SearchProduct" });
            routes.MapRoute(name: "Order", url: "Order", defaults: new { controller = "Order", action = "OrderIndex" });
            routes.MapRoute(name: "Khuyenmai", url: "Khuyen-mai-inax", defaults: new { controller = "Sale", action = "ListSale" });
            routes.MapRoute(name: "Maps", url: "Ban-do", defaults: new { controller = "MapsDisplay", action = "Index" });
            routes.MapRoute(name: "Spdongbo", url: "san-pham-inax-dong-bo", defaults: new { controller = "ProductSysDislay", action = "Hienthidongbo" });
            routes.MapRoute(name: "Admin", url: "Admin", defaults: new { controller = "Login", action = "LoginIndex" });
            routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}", defaults: new { controller = "Homes", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
