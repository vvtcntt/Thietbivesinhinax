using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INAX.Models;
using PagedList;
using PagedList.Mvc;
using System.Globalization;
namespace INAX.Controllers.Display.Session.Agency
{
    public class AgencyDisplayController : Controller
    {
        //
        // GET: /AgencyDisplay/
        private DatabaseINAXContext db = new DatabaseINAXContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListAgency(string tag, int? page, string id)
        {
            int idCate = int.Parse(db.tblGroupAgencies.First(p => p.Tag == tag).id.ToString());
            var listnews = db.tblAgencies.Where(p => p.idMenu == idCate && p.Active == true).OrderByDescending(p => p.DateCreate).ToList();
            const int pageSize = 20;
            var pageNumber = (page ?? 1);
            // Thiết lập phân trang
            var ship = new PagedListRenderOptions
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToIndividualPages = true,
                DisplayPageCountAndCurrentLocation = false,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                EllipsesFormat = "&#8230;",
                LinkToFirstPageFormat = "Trang đầu",
                LinkToPreviousPageFormat = "«",
                LinkToIndividualPageFormat = "{0}",
                LinkToNextPageFormat = "»",
                LinkToLastPageFormat = "Trang cuối",
                PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                FunctionToDisplayEachPageNumber = null,
                ClassToApplyToFirstListItemInPager = null,
                ClassToApplyToLastListItemInPager = null,
                ContainerDivClasses = new[] { "pagination-container" },
                UlElementClasses = new[] { "pagination" },
                LiElementClasses = Enumerable.Empty<string>()
            };
            ViewBag.ship = ship;

            var groupnew = db.tblGroupAgencies.First(p => p.Tag == tag);
            int dodai = groupnew.Level.Length / 5;
            string nUrl = "";
            for (int i = 0; i < dodai; i++)
            {
                var NameGroups = db.tblGroupAgencies.First(p => p.Level.Substring(0, (i + 1) * 5) == groupnew.Level.Substring(0, (i + 1) * 5) && p.Level.Length == (i + 1) * 5);
                nUrl = nUrl + " <a href=\"/4/" + NameGroups.Tag + "\" title=\"" + NameGroups.Name + "\"> " + " " + NameGroups.Name + "</a> /";
            }
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a> /" + nUrl;
            ViewBag.Title = "<title>" + groupnew.Name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + groupnew.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + groupnew.Name + "\" /> ";
            ViewBag.Name = groupnew.Name;
            return View(listnews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AgencyDetail(string tag)
        {

            var tblnews = db.tblAgencies.First(p => p.Tag == tag);
            int id = int.Parse(tblnews.id.ToString());
            ViewBag.Title = "<title>" + tblnews.Name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblnews.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblnews.Name + "\" /> ";
            int idCates = int.Parse(tblnews.idMenu.ToString());
            string Urlgroup = db.tblGroupAgencies.Find(idCates).Tag;
            if (tblnews.Tabs != null)
            {
                string Chuoi = tblnews.Tabs;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {

                    string tabs = Mang[i].ToString();
                    var listnew = db.tblNews.Where(p => p.Tabs.Contains(tabs) && p.id != id && p.Active == true).ToList();
                    for (int j = 0; j < listnew.Count; j++)
                    {
                        araylist.Add(listnew[j].id);
                    }

                }


                var listnewlienquan = db.tblAgencies.Where(p => araylist.Contains(p.id) && p.Active == true && p.id != id).OrderByDescending(p => p.Ord).Take(3).ToList();
                string chuoinew = "";
                if (listnewlienquan.Count > 0)
                {

                    chuoinew += " <div class=\"Tintuclienquan\">";
                    for (int i = 0; i > listnewlienquan.Count; i++)
                    {
                        chuoinew += "<a href=\"/5/"+listnewlienquan[i].Tag+"\" title=\"\">› " + listnewlienquan[i].Name + "</a>";
                    }
                    chuoinew += "</div>";
                }
                ViewBag.chuoinew = chuoinew;


                //Load tin mới

            }
            int iduser = int.Parse(tblnews.idUser.ToString());
            var User = db.tblUsers.Find(iduser);
            ViewBag.UserName = User.FullName;
            string chuoinewnew = "";
            var NewsNew = db.tblAgencies.Where(p => p.Active == true).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            for (int i = 0; i < NewsNew.Count; i++)
            {
                chuoinewnew += "<li><a href=\"/5/"+NewsNew[i].Tag+"\" title=\"" + NewsNew[i].Name + "\" rel=\"nofollow\">› " + NewsNew[i].Name + " <span>" + NewsNew[i].DateCreate + "</span></a></li>";
            }
            ViewBag.chuoinewnews = chuoinewnew;

            //load listnews
            var Groupnews = db.tblGroupAgencies.First(p => p.id == tblnews.idMenu);
            int dodai = Groupnews.Level.Length / 5;
            string nUrl = "";
            for (int i = 0; i < dodai; i++)
            {
                var NameGroups = db.tblGroupAgencies.First(p => p.Level.Substring(0, (i + 1) * 5) == Groupnews.Level.Substring(0, (i + 1) * 5));
                nUrl = nUrl + " <a href=\"/4/" + NameGroups.Tag + "\" title=\"\"> " + " " + NameGroups.Name + "</a> /";
            }
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a> /" + nUrl + " " + tblnews.Name;
            return View(tblnews);
         }
    }
}
