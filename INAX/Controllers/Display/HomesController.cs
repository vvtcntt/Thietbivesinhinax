using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INAX.Models;
using System.Text;
namespace INAX.Controllers.Display
{
    public class HomesController : Controller
    {
        //
        // GET: /Homes/
        private DatabaseINAXContext db = new DatabaseINAXContext();
        public ActionResult Index()
        {//connect
            tblConfig config = db.tblConfigs.First();
            ViewBag.Title = "<title>" + config.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + config.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + config.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + config.Keywords + "\" /> ";


            if (Session["registry"] != null)
            {
                ViewBag.Thongbao = Session["registry"].ToString();
            }
            Session["registry"] = null;
            string chuoiparner = "";
            var listparner = db.tblImages.Where(p => p.Active == true && p.idCate == 6).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < listparner.Count; i++)
            {
                if (listparner[i].Link == true)
                { chuoiparner += "<li><a href=\"" + listparner[i].Url + "\" target=\"_blank\" title=\"" + listparner[i].Name + "\" ><img width=\"190\" src=\"" + listparner[i].Images + "\" class=\"img_doitac\" alt=\"" + listparner[i].Name + "\" /></a> </li>"; }
                else
                { chuoiparner += "<li><a href=\"" + listparner[i].Url + "\" title=\"" + listparner[i].Name + "\" target=\"_blank\" rel=\"" + listparner[i].Link + "\"><img width=\"190\" src=\"" + listparner[i].Images + "\" class=\"img_doitac\" alt=\"" + listparner[i].Name + "\" /></a> </li>"; }

            }
            ViewBag.chuoiimage = chuoiparner;
            return View();
        }
        //[OutputCache(Duration = 3600)]

        public PartialViewResult Top()
        {
            string chuoiagency = "";
            var tblconfig = db.tblConfigs.First();
            var listagency = db.tblGroupAgencies.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < listagency.Count; i++)
            {
                chuoiagency += "<li><a href=\"/4/" + listagency[i].Tag + "\" title=\"" + listagency[i].Name + "\">&bull; " + listagency[i].Name + "</a></li>";
            }
            ViewBag.chuoiagency = chuoiagency;
            return PartialView(tblconfig);
        }
        [OutputCache(Duration = 3600)]

        public PartialViewResult PartialCategories()
        {
            StringBuilder chuoi = new StringBuilder();
            var listmenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == null).OrderBy(p => p.Ord).Take(9).ToList();
            for (int i = 0; i < listmenu.Count; i++)
            {
                string tag = listmenu[i].Tag;                   
                chuoi.Append("<li class=\"li_Ct1\">");
                chuoi.Append("<div class=\"Content_ct\">");
                chuoi.Append("<div class=\"img_Content_ct\">");
                chuoi.Append("<a href=\"/" + tag + ".html\" title=\"" + listmenu[i].Name + "\" class=\"tb_img\">");
                chuoi.Append("<img src=\"" + listmenu[i].Images + "\" alt=\"" + listmenu[i].Name + "\" />");
                chuoi.Append("</a>");
                chuoi.Append("</div>");
                chuoi.Append("<div class=\"Clear\"></div>");
                chuoi.Append("<a class=\"Name_pd\" title=\"" + listmenu[i].Name + "\" href=\"/" + tag + ".html\"> " + listmenu[i].Name + "</a>");
                chuoi.Append("</div>");
                chuoi.Append("</li>");
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
        [OutputCache(Duration = 3600)]

        public PartialViewResult Adw()
        {
            string chuoileft = "";
            string chuoiright = "";
            var listLeft = db.tblImages.Where(p => p.Active == true & p.idCate == 4).ToList();
            for (int i = 0; i < listLeft.Count; i++)
            {
                chuoileft += "<a href=\"" + listLeft[i].Url + "\" title=\"" + listLeft[i].Name + "\"><img src=\"" + listLeft[i].Images + "\" alt=\"" + listLeft[i].Name + "\" title=\"" + listLeft[i].Name + "\" width=\"90\" /></a>";
            }
            var listright = db.tblImages.Where(p => p.Active == true & p.idCate == 5).ToList();
            for (int i = 0; i < listright.Count; i++)
            {
                chuoiright += "<a href=\"" + listright[i].Url + "\" title=\"" + listright[i].Name + "\"><img src=\"" + listright[i].Images + "\" alt=\"" + listright[i].Name + "\" title=\"" + listright[i].Name + "\" width=\"90\" /></a>";
            }
            ViewBag.chuoileft = chuoileft;
            ViewBag.chuoiright = chuoiright;
            return PartialView();
        }
        [OutputCache(Duration = 3600)]

        public PartialViewResult partialBanner()
        {
            var tblconfig = db.tblConfigs.First();
            return PartialView(tblconfig);
        }
        [OutputCache(Duration = 3600)]

        public PartialViewResult PartialPopup()
        {
            tblConfig config = db.tblConfigs.First();
            string chuoi = "";
            if (config.Popup == true)
            {
                var listImage = db.tblImages.Where(p => p.Active == true && p.idCate == 7).OrderByDescending(p => p.Ord).Take(1).ToList();
                if (listImage.Count > 0)
                {
                    chuoi += "<div id=\"myModal\" class=\"linhnguyen-modal\">";
                    chuoi += "<a class=\"close-linhnguyen-modal\" title=\"đóng\">X</a>";
                    if (listImage[0].Link == true)
                    { chuoi += "<a href=\"" + listImage[0].Url + "\" target=\"_blank\" title=\"" + listImage[0].Name + "\"><img src=\"" + listImage[0].Images + "\" alt=\"" + listImage[0].Name + "\"/></a>"; }
                    else
                    { chuoi += "<a href=\"" + listImage[0].Url + "\" target=\"_blank\" title=\"" + listImage[0].Name + "\" rel=\"" + listImage[0].Link + "\"><img src=\"" + listImage[0].Images + "\" alt=\"" + listImage[0].Name + "\"/></a>"; }

                    chuoi += "</div>";
                }
            }
            ViewBag.Popup = chuoi;
            return PartialView();
        }
        [OutputCache(Duration = 3600)]

        public PartialViewResult PartialAdwleft()
        {
            tblConfig config = db.tblConfigs.First();
            string chuoi = "";
            if (config.PopupSupport == true)
            {
                var listImage = db.tblImages.Where(p => p.Active == true && p.idCate == 8).OrderByDescending(p => p.Ord).Take(1).ToList();
                if (listImage.Count > 0)
                {
                    chuoi += "<div class=\"float-ck\" style=\"left: 0px\">";
                    chuoi += "<div id=\"hide_float_left\">";
                    chuoi += "<a href=\"javascript:hide_float_left()\">Tắt Quảng Cáo [X]</a>";
                    chuoi += "</div>";
                    chuoi += "<div id=\"float_content_left\"> ";
                    if (listImage[0].Link == true)
                    { chuoi += "<a href=\"" + listImage[0].Url + "\" target=\"_blank\" title=\"" + listImage[0].Name + "\"><img src=\"" + listImage[0].Images + "\" alt=\"" + listImage[0].Name + "\"/></a>"; }
                    else
                    { chuoi += "<a href=\"" + listImage[0].Url + "\" target=\"_blank\" title=\"" + listImage[0].Name + "\" rel=\"" + listImage[0].Link + "\"><img src=\"" + listImage[0].Images + "\" alt=\"" + listImage[0].Name + "\"/></a>"; }

                    chuoi += "</div>";
                    chuoi += "</div>";
                }
            }
            ViewBag.popupSuport = chuoi;
            return PartialView();
        }
        [OutputCache(Duration = 3600)]

        public PartialViewResult Thongbao()
        {
            return PartialView();
        }
        public PartialViewResult adwadsense()
        {

            return PartialView(db.tblConfigs.First());
        }
        public ActionResult Action()
        {
            var ListGroupProduct = db.tblGroupProducts.ToList();
            foreach (var item in ListGroupProduct)
            {
                int id = item.id;
                string name=item.Name;
                string tag = StringClass.NameToTag(name);
                clsSitemap.CreateSitemap(tag+ ".html", id.ToString(), "GroupProduct");
            }
            var ListProduct = db.tblProducts.ToList();
            foreach (var item in ListProduct)
            {
                int id = item.id;
                string name = item.Name;
                string tag = StringClass.NameToTag(name);
                clsSitemap.CreateSitemap(tag + "-dt", id.ToString(), "Product");
            }
            var ListNews = db.tblNews.ToList();
            foreach (var item in ListNews)
            {
                int id = item.id;
                string name = item.Name;
                string tag = StringClass.NameToTag(name);
                clsSitemap.CreateSitemap("tin-tuc/"+tag, id.ToString(), "News");
            }
            return View();
        }

    }
}
