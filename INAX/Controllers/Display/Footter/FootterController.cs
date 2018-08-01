using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INAX.Models;
using System.Text;
namespace INAX.Controllers.Display.Footter
{

    public class FootterController : Controller
    {
        private DatabaseINAXContext db = new DatabaseINAXContext();
        //
        // GET: /Footter/

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PartialFootter()
        {
            tblConfig tblconfig = db.tblConfigs.First();
            ViewBag.codechat = tblconfig.CodeChat;

            var listBaogia = db.tblBaogias.Where(p => p.Active == true).OrderByDescending(p => p.Active == true).Take(6).ToList();
            StringBuilder chuoibaogia =new StringBuilder();
            foreach (var item in listBaogia)
            {
                chuoibaogia.Append("<a href=\"/Bao-gia/" + item.Tag + "\" title=\"" + item.Name + "\">" + item.Name + "</a>");
            }
            ViewBag.chuoibaogia = chuoibaogia;
            StringBuilder chuoi = new StringBuilder();
            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == null).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                chuoi.Append("<li><a href=\"/" + ListMenu[i].Tag + "\" title=\"" + ListMenu[i].Name + "\">" + ListMenu[i].Name + "</a>");
                int idCate = ListMenu[i].id;
                var listmenuchild = db.tblGroupProducts.Where(p => p.ParentID == idCate && p.Active == true).OrderBy(p => p.Ord).ToList();
                if (listmenuchild.Count > 0)
                {
                    chuoi.Append("<ul>");
                    for (int j = 0; j < listmenuchild.Count; j++)
                    {
                         chuoi.Append( "<li><a href=\"/0/" + listmenuchild[j].Tag + "\" title=\"" + listmenuchild[j].Name + "\">" + listmenuchild[j].Name + "</a></li>");
                    }
                     chuoi.Append("</ul>");
                }
                chuoi.Append( "</li>");
            }
            ViewBag.chuoi = chuoi;
            string link = "";
            var listlink = db.tblUrls.Where(p => p.Active == true).OrderByDescending(p => p.Ord).ToList();
            for (int i = 0; i < listlink.Count; i++)
            {
                if (listlink[i].Rel == true)
                { link += "<a href=\"" + listlink[i].Url + "\" title=\"" + listlink[i].Name + "\">" + listlink[i].Name + "</a>,"; }
                else
                { link += "<a href=\"" + listlink[i].Url + "\" title=\"" + listlink[i].Name + "\" rel=\"" + listlink[i].Rel + "\">" + listlink[i].Name + "</a>,"; }


            }
            ViewBag.Link = link;
            var maps = db.tblMaps.First();
            ViewBag.maps = maps.Content;
            Session["Adsense"] = tblconfig.Content;
            var Imagesadw = db.tblImages.Where(p => p.Active == true && p.idCate == 9).OrderByDescending(p => p.Ord).Take(1).ToList();
            if (Imagesadw.Count > 0)
                ViewBag.Chuoiimg = "<a href=\"" + Imagesadw[0].Url + "\" title=\"" + Imagesadw[0].Name + "\"><img src=\"" + Imagesadw[0].Images + "\" alt=\"" + Imagesadw[0].Name + "\" style=\"max-width:100%;\" /> </a>";

            var listParner = db.tblHotlines.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder resultParner = new StringBuilder();
            for(int i=0;i<listParner.Count;i++)
            {
                resultParner.Append("<p class=\"p2\"><span style=\"color:#f9ff5f\">"+listParner[i].Name+" </span>: <span> "+listParner[i].Hotline+"</span></p>");
                  resultParner.Append("<p class=\"p2\">Điện thoại : <span>" + listParner[i].Mobile + "</span></p>");
            }
            ViewBag.resultParner = resultParner.ToString();
 
            StringBuilder Chuoiimg = new StringBuilder();
            if (Request.Browser.IsMobileDevice)
            {
                Chuoiimg.Append("<div id=\"adwfooter\"><div class=\"support\">");
                Chuoiimg.Append("<div class=\"leftSupport\">");
                Chuoiimg.Append("<p><i class=\"fa fa-comments-o\" aria-hidden=\"true\"></i> Hỗ trợ khách hàng</p>");
                Chuoiimg.Append("<a href=\"tel:" + tblconfig.HotlineIN + "\"> " + tblconfig.HotlineIN + "</a>");
                Chuoiimg.Append("<a href=\"tel:" + tblconfig.HotlineOUT + "\">" + tblconfig.HotlineOUT + "</a>");
                Chuoiimg.Append("</div>");
                Chuoiimg.Append("<div class=\"rightSupport\">");
                Chuoiimg.Append("<p><i class=\"fa fa-clock-o\" aria-hidden=\"true\"></i> Thời gian làm việc</p>");
                Chuoiimg.Append("<span class=\"sp1\"> 8H đến 19H30</span>");
                Chuoiimg.Append("<span class=\"sp2\"> Làm cả thứ 7 & Chủ nhật</span>");
                Chuoiimg.Append("</div>");
                Chuoiimg.Append("</div></div>");

                }
        ViewBag.results = Chuoiimg.ToString();
            return PartialView(tblconfig);
        }
        public ActionResult Command(FormCollection collection, tblRegister registry)
        {
            if (collection["dkKM"] != null)
            {
                string Email = collection["txtRegis"];
                var listemail = db.tblRegisters.Where(p => p.Email == Email).ToList();
                if (listemail.Count > 0)
                { Session["registry"] = "<script>$(document).ready(function(){ alert('Không thành công ! Email của bạn đã có trên hệ thống !') });</script>"; }
                else
                {
                    registry.Email = Email;
                    db.tblRegisters.Add(registry);
                    db.SaveChanges();
                    Session["registry"] = "<script>$(document).ready(function(){ alert('Bạn đã đăng ký email thành công') });</script>";
                }
            }
            return Redirect("/Homes/Index");
        }
    }
}
