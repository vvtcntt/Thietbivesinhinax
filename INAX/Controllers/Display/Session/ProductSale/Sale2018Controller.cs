using INAX.Models;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace INAX.Controllers.Display.Session.ProductSale
{
    public class Sale2018Controller : Controller
    {
        private DatabaseINAXContext db = new DatabaseINAXContext();

        // GET: Sale2018
        public ActionResult Index()
        {
            var sales = db.tblProductSales.Where(p => p.Active == true).OrderByDescending(p => p.Ord).ToList()[0];
            ViewBag.resultImage = "<img src=\"" + sales.ImageBanner + "\" alt=\"" + sales.Name + "\" />";
            ViewBag.Title = "<title>" + sales.Name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + sales.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + sales.Name + "\" /> ";
            var listProduct = db.tblProducts.Where(p => p.Active == true && p.ProductSale == true).ToList();
            var listIdCate = listProduct.Select(p => p.idCate).ToList();
            var listgroups = db.tblGroupProducts.Where(p => p.Active == true && listIdCate.Contains(p.id)).ToList();
            var ListGroupProduct = db.tblGroupProducts.Where(p=>p.Active==true && p.ParentID==null).OrderBy(p=>p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < ListGroupProduct.Count; i++)
            {
                int id = ListGroupProduct[i].id;
                var ListId = listgroups.Where(p => p.ParentID == id).Select(p => p.id).ToList();
                if (ListId.Count>0)
                {
                    result.Append("<div class=\"tearListSale\">");
                    if (ListGroupProduct[i].Background != null && ListGroupProduct[i].Background != "")
                    {
                        result.Append("<div class=\"imgTearSale\">");
                        result.Append("<a href=\"/" + ListGroupProduct[i].Tag + ".html\" title=\"" + ListGroupProduct[i].Name + "\"><img src=\"" + ListGroupProduct[i].Background + "\" alt=\"" + ListGroupProduct[i].Name + "\" /></a>");
                        result.Append("</div>");
                    }

                     var listProducts = listProduct.Where(p => p.Active==true && ListId.Contains(p.idCate.Value)).ToList();
                    for (int j = 0; j < listProducts.Count; j++)
                    {
                        if (listProducts[j].Priority == true)
                        {
                            result.Append("<div class=\"tearSale largeTearSale\">");
                            result.Append("<div class=\"contentTearSale\">");
                            result.Append("<div class=\"saleTop\">");
                            result.Append(" </div> ");
                            result.Append("<span class=\"nameSaleTop\">" + sales.Slogan + "</span>");
                            result.Append("<div class=\"infoTear\">");
                            result.Append("<span class=\"name\">" + listProducts[j].Name + "</span>");
                            result.Append("<span class=\"code\">Mã : " + listProducts[j].Code + "</span>");
                            result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProducts[j].PriceSale) + "đ</span>");
                            result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProducts[j].Price) + "đ</span>");
                            result.Append("<div class=\"noteSale\">  " + listProducts[j].Info + "</div>");
                            result.Append("<div class=\"box_Sale\">");
                            result.Append("<div class=\"icon\">Khuyến mại</div>");
                            result.Append("<div class=\"content_BoxSale\">" + listProducts[j].Sale + "</div>");
                            result.Append("</div>");
                            result.Append("</div>");
                            result.Append("<div class=\"ImageInfo\">");
                            result.Append("<div class=\"img\">");
                            result.Append("<a href=\"/" + listProducts[j].Tag + "-dt\"><img src=\"" + listProducts[j].ImageLinkDetail + "\" alt=\"" + listProducts[j].Name + "\" /></a>");
                            result.Append("</div>");
                            result.Append("<div class=\"iconSale\">" + Convert.ToInt32(100 - (listProducts[j].PriceSale * 100 / listProducts[j].Price)) + "%</div>");
                            result.Append("</div>");
                            result.Append("</div>");
                            result.Append("</div>");
                        }
                        else
                        {
                            result.Append("<div class=\"tearSale\">");
                            result.Append("<div class=\"contentTearSale\">");
                            result.Append("<div class=\"Top\">");
                            result.Append("<div class=\"img\">");
                            result.Append("<a href=\"/" + listProducts[j].Tag + "-dt\" title=\"" + listProducts[j].Name + "\"><img src=\"" + listProducts[j].ImageLinkDetail + "\" alt=\"" + listProducts[j].Name + "\" /></a>");
                            result.Append("</div>");
                            result.Append("<div class=\"iconSale\">" + Convert.ToInt32(100 - (listProducts[j].PriceSale * 100 / listProducts[j].Price)) + "%</div>");
                            result.Append("<div class=\"saleTop\"></div>");
                            result.Append("<span class=\"nameSaleTop\">" + sales.Slogan + "</span>");
                            result.Append("</div>");
                            result.Append("<span class=\"code\">Mã : " + listProducts[j].Code + "</span>");
                            result.Append(" <span class=\"name\">" + listProducts[j].Name + "</span>");
                            result.Append(" <span class=\"priceSale\">" + string.Format("{0:#,#}", listProducts[j].PriceSale) + "đ</span>");
                            result.Append(" <span class=\"price\">" + string.Format("{0:#,#}", listProducts[j].Price) + "đ</span>");
                            result.Append(" <div class=\"box_Sale\">");
                            result.Append(" <div class=\"icon\">Khuyến mại</div>");
                            result.Append("<div class=\"content_BoxSale\">" + listProducts[j].Sale + "</div>");
                            result.Append("</div>");
                            result.Append("</div>");
                            result.Append("</div>");
                        }
                    }
                    result.Append("</div>");
                }
                
            }
            ViewBag.result = result.ToString();
            return View();
        }
    }
}