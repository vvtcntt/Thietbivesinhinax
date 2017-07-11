using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INAX.Models;
namespace INAX.Controllers.Display.Session.Maps
{
    public class MapsDisplayController : Controller
    {
        //
        // GET: /MapsDisplay/
        DatabaseINAXContext db = new DatabaseINAXContext();
        public ActionResult Index()
        {
            tblMap map = db.tblMaps.First();
            ViewBag.Title = "<title>" + map.Name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + map.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + map.Name + "\" /> ";
            return View(map);
        }

    }
}
