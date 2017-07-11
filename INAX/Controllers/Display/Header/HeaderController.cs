using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INAX.Models;
using System.Text;
namespace INAX.Controllers.Display.Header
{
    public class HeaderController : Controller
    {
        
        //
        // GET: /Header/

        private DatabaseINAXContext db = new DatabaseINAXContext();
        public ActionResult Index()
        {
            return View();
        }
                //[OutputCache(Duration = 36000 )]

        public PartialViewResult PartialHeader()
        {
            var imagelist = db.tblImages.Where(p => p.Active == true && p.idCate == 1).OrderBy(p=>p.Ord).ToList();
            string chuoislide = "";
           // background: url(img/dummy-640x310-1.jpg) 0 0 no-repeat, url(img/dummy-640x310-2.jpg) 640px 0 no-repeat, url(img/dummy-640x310-3.jpg) 1280px 0 no-repeat, url(img/dummy-640x310-4.jpg) 1920px 0 no-repeat;
            for(int i=0;i<imagelist.Count;i++)
            { 
                if(i==0)
                {
                    chuoislide += "url("+imagelist[i].Images+") "+(680*i)+"px 0 no-repeat";
                }
                else
                {

                    chuoislide += ", url(" + imagelist[i].Images + ") " + (680 * i) + "px 0 no-repeat";
                }
            }
            ViewBag.chuoislide = chuoislide;
            //load listnew homes
            string chuoinew = "";
            var news = db.tblNews.Where(p => p.Active == true && p.ViewHomes==true).OrderByDescending(p => p.Ord).Take(3).ToList();

            for (int i = 0; i < news.Count;i++ )
            {
                int idCates = int.Parse(news[i].idCate.ToString());
                string Urlgroup = db.tblGroupNews.Find(idCates).Tag;
                chuoinew += "<li><a href=\"/tin-tuc/" + news[i].Tag + "\" title=\"" + news[i].Name + "\">&bull; " + news[i].Name + "</a></li>";
            }
            ViewBag.chuoinew = chuoinew;
            var video = db.tblVideos.First();
            string chuoivideo = "";
            if(video.AutoPlay==true)
            { chuoivideo = " <iframe width=\"289px\" height=\"192px\" src=\"http://www.youtube.com/embed/" + video.Code + "?;hl=en&amp;fs=1&amp;autoplay=1;loop=1;repeat=0;rel=0\" frameborder=\"0\" allowfullscreen></iframe>";  }
            else
            { chuoivideo = " <iframe width=\"289px\" height=\"192px\" src=\"http://www.youtube.com/embed/" + video.Code + "?;hl=en&amp;fs=1&amp;autoplay=0;loop=1;repeat=0;rel=0\" frameborder=\"0\" allowfullscreen></iframe>"; }
            ViewBag.chuoivideo = chuoivideo;


            return PartialView(imagelist);
        }
 
        public PartialViewResult PartialMenu()
        {
            var MenuParent = db.tblGroupProducts.Where(p => p.ParentID==null && p.Active == true).OrderBy(p => p.Ord).Take(10).ToList();
            StringBuilder chuoi = new StringBuilder();
            for (int i = 0; i < MenuParent.Count; i++)
            {
                string tag = MenuParent[i].Tag;
                string[] xuly = tag.Split('/');
                
                chuoi.Append("<li class=\"li1\">");
                   chuoi.Append("<a href=\"/" + MenuParent[i].Tag + ".html\" title=\"" + MenuParent[i].Name + "\"><span class=\"iCon\" style=\"background:url(" + MenuParent[i].iCon + ") no-repeat transparent\"></span> " + MenuParent[i].Name + "</a>"); 
                 
               int idcate=MenuParent[i].id;
                var listMenu = db.tblGroupProducts.Where(p => p.ParentID==idcate && p.Active == true).OrderBy(p => p.Ord).ToList();
                if (listMenu.Count > 0)
                {
                     chuoi.Append("<ul class=\"ul2\" style=\"background: url(" + MenuParent[i].Background + ") no-repeat right bottom scroll #FFF\">");
                    for (int j = 0; j < listMenu.Count; j++)
                    {
                   chuoi.Append( "<li class=\"li2\">");
                          chuoi.Append("<a href=\"/0/"+listMenu[j].Tag+"\" title=\"" + listMenu[j].Name + "\" class=\"Name\">›  " + listMenu[j].Name + "</a>");
                          chuoi.Append( "<hr />");
                          chuoi.Append( "<span class=\"Status\"> " + listMenu[j].Note + " </span>");
                        chuoi.Append( "<a href=\"/0/" + listMenu[j].Tag + "\" class=\"Image\" title=\"" + listMenu[j].Name + "\"><img src=\"" + listMenu[j].Images + "\" alt=\"" + listMenu[j].Name + "\" /></a>");
                          chuoi.Append( "</li> ");
                    }
                    chuoi.Append( "</ul>");
                }
                 chuoi.Append("</li>");
            }
            ViewBag.chuoimenu = chuoi;
          return PartialView();
        }
    }
}
