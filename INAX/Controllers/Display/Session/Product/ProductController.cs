using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INAX.Models;
using System.Text;
namespace INAX.Controllers.Display.Session
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        private DatabaseINAXContext db = new DatabaseINAXContext();

        public ActionResult Index()
        {
            return View();
        }
        //[OutputCache(Duration = 36000)]

        public PartialViewResult ProductSaleHomes()
        {
            StringBuilder chuoi = new StringBuilder();
             var listProductHomes = db.tblProducts.Where(p => p.Active == true && p.ProductSale == true).OrderBy(p => p.Ord).ToList();
           for (int i = 0; i < listProductHomes.Count; i++)
            {
                int idcates = int.Parse(listProductHomes[i].idCate.ToString());
                string Url = db.tblGroupProducts.First(p => p.id == idcates).Tag;
                chuoi.Append("<div class=\"Tear_Sale\">");
                //chuoi.Append( "<div class=\"Sale\"><span>");
                float Giany = float.Parse(listProductHomes[i].Price.ToString());
                float Giakm = float.Parse(listProductHomes[i].PriceSale.ToString());
                float km = 100 - (Giakm * 100 / Giany);
                //if(km>0)
                //{ 
                //chuoi+="-"+Convert.ToInt32(km)+"%");
                //}
                string chuoigia = Convert.ToInt32(Giany - Giakm).ToString();
                //chuoi+="</span></div>");
                //if(chuoigia.Length>3)
                //chuoi.Append( "<div class=\"SaleOff\"><span>" + string.Format("{0:#,#}",chuoigia.Substring(0,chuoigia.Length-3)) + "K</span></div>");
                //chuoi.Append( "<div class=\"box_quatang\" style=\"background-image:url("+listProductHomes[i].ImageSale+")\"></div>");
                chuoi.Append( "<div class=\"Content_Sale\">");
                chuoi.Append( "<div class=\"Top_Content_Sale\">");
                chuoi.Append( "<a class=\"img_PdSale\" href=\"/"+ listProductHomes[i].Tag + "-dt\" title=\"" + listProductHomes[i].Name + "\">");
                chuoi.Append( "<img src=\"" + listProductHomes[i].ImageLinkThumb + "\" alt=\"" + listProductHomes[i].Name + "\" title=\"" + listProductHomes[i].Name + "\" />");
                chuoi.Append( "</a>");
                chuoi.Append( "</div>");
                chuoi.Append( "<h3><a href=\"/" + listProductHomes[i].Tag + "-dt\" title=\"" + listProductHomes[i].Name + "\">" + listProductHomes[i].Name + "</a></h3>");
                chuoi.Append( "<span class=\"Price\">Giá NY : <span>" + string.Format("{0:#,#}", listProductHomes[i].Price) + "đ</span></span>");
                string hoigia = "";
                string note = ".";
                 if (listProductHomes[i].PriceSaleActive>1)
                {
                    chuoi.Append("<span class=\"PriceSale\">Giá KM : <span>" + string.Format("{0:#,#}", listProductHomes[i].PriceSaleActive) + "đ</span></span>");
                    hoigia = "Lấy giá tốt hơn ";
                   
                    if(listProductHomes[i].PriceSaleActive>listProductHomes[i].PriceSale)
                    {
                        note = ". Ngoài ra chúng tôi có thể giảm thêm " + string.Format("{0:#,#}", listProductHomes[i].PriceSaleActive - listProductHomes[i].PriceSale) + "đ hoặc cao hơn nữa cho bạn !";
                        Giakm = float.Parse(listProductHomes[i].PriceSaleActive.ToString());
                    }
                }
                else
                {
                    hoigia = "Lấy giá tốt nhất ";
                    chuoi.Append("<span class=\"PriceSale\">Giá KM : <span>Liên hệ</span></span>");
                }              
                chuoi.Append( "<div class=\"tietkiem\">");
                chuoi.Append( "<span class=\"tk1\">Cam kết giá rẻ nhất khi gọi điện hoặc đến trực tiếp hoặc click vào đây :</span>");

                chuoi.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProductHomes[i].id + "\" onClick=\"javascript:return Laygia('lg" + listProductHomes[i].id + "','" + listProductHomes[i].Name + "','" + String.Format("{0:#,#}", Giakm) + "','" + note + "');\" title=\"Để lấy giá hỗ trợ rẻ nhất Hà Nội \">" + hoigia + "</span></div>");
                chuoi.Append( "</div>");
                //chuoi.Append( "<div class=\"sevices\">");
                //if (listProductHomes[i].Status == true)
                //{
                //    chuoi.Append( "<span class=\"Status\"></span>");
                //}
                //else
                //{ chuoi.Append( "<span class=\"StatusNo\"></span>"); }

                //chuoi.Append( "<span class=\"Transport\"><span class=\"icon\">");
                //if (listProductHomes[i].Transport == true)
                //{
                //    chuoi.Append( "</span> Toàn quốc</span>");
                //}
                //else
                //{ chuoi.Append( "</span> Liên hệ</span>"); }
                //chuoi.Append( " </div>");
                chuoi.Append( "</div>");
                chuoi.Append( "</div>");
            }
            ViewBag.chuoilist = chuoi;
            tblConfig config = db.tblConfigs.First();
            ViewBag.h1 = config.Title;
            return PartialView();
        }
        //[OutputCache(Duration = 36000)]
        string nUrl = "";
        public string UrlProduct(int idCate)
        {
            var ListMenu = db.tblGroupProducts.Where(p => p.id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = " <a href=\"/" + ListMenu[i].Tag + ".html\" title=\"" + ListMenu[i].Name + "\"> " + " " + ListMenu[i].Name + "</a> <i></i>" + nUrl;
                string ids = ListMenu[i].ParentID.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentID.ToString());
                    UrlProduct(id);
                }
            }
            return nUrl;
        }
        List<string> Mangphantu = new List<string>();
        public List<string> Arrayid(int idParent)
        {

            var ListMenu = db.tblGroupProducts.Where(p => p.ParentID == idParent).ToList();

            for (int i = 0; i < ListMenu.Count; i++)
            {
                Mangphantu.Add(ListMenu[i].id.ToString());
                int id = int.Parse(ListMenu[i].id.ToString());
                Arrayid(id);

            }

            return Mangphantu;
        }
        public PartialViewResult ListProductHomes()
        {
            StringBuilder chuoi = new StringBuilder();
            var MenuParent = db.tblGroupProducts.Where(p => p.Active == true && p.Priority == true).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < MenuParent.Count; i++)
            {
                chuoi.Append( "<div class=\"Tear_GroupsProduct\">");
                chuoi.Append( "<div class=\"nVar\">");
                chuoi.Append( "<div class=\"Left_nVar\">");
                chuoi.Append( "<div class=\"Name\">");
                chuoi.Append( "<span class=\"iCon\" Style=\"background:url(" + MenuParent[i].iCon + ") no-repeat\"></span>");
                chuoi.Append( "<h2>" + MenuParent[i].Name + "</h2>");
                chuoi.Append( "</div>");
                chuoi.Append( "<div class=\"iCon_nVar\">");
                chuoi.Append( "<span>N" + i + "</span>");
                chuoi.Append( "</div>");
                chuoi.Append( "</div>");

                chuoi.Append( "<div class=\"Right_nVar\">");
                int idParent1 = MenuParent[i].id;
                List<string> Mang = new List<string>();
                Mang = Arrayid(idParent1);
                Mang.Add(idParent1.ToString());
                var Menuchild = db.tblGroupProducts.Where(p => p.ParentID == idParent1 && p.Active == true).OrderBy(p => p.Ord).Take(5).ToList();
                if (Menuchild.Count > 0)
                {
                    chuoi.Append( "<ul class=\"ul_1\">");
                    for (int j = 0; j < Menuchild.Count; j++)
                    {
                        string ntag = Menuchild[j].Tag;

                        chuoi.Append( "<li class=\"li_1\">");
                        chuoi.Append( "<h2><a href=\"/" + ntag + ".html\" title=\"" + Menuchild[j].Name + "\">" + Menuchild[j].Name + " <span></span></a></h2>");
                        int idParent2 = Menuchild[j].id;
                        var Menuchild1 = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idParent2).OrderBy(p => p.Ord).ToList();
                        if (Menuchild1.Count > 0)
                        {
                            chuoi.Append( "<ul class=\"ul_2\">");
                            for (int k = 0; k < Menuchild1.Count; k++)
                            {
                                chuoi.Append( "<li class=\"li_2\">");
                                chuoi.Append( "<h2><a href=\"/" + Menuchild1[k].Tag + ".html\" title=\"" + Menuchild1[k].Name + "\">› " + Menuchild1[k].Name + "</a></h2>");
                                chuoi.Append( "</li>");
                            }
                            chuoi.Append( "</ul>");
                        }
                        chuoi.Append( "</li>");
                    }
                    chuoi.Append( "  </ul>");
                }
                string tag1 = MenuParent[i].Tag;

                chuoi.Append( "<a href=\"/" + tag1 + ".html\" title=\"Xem thêm\" class=\"Xemchitiet\">Xem thêm &raquo;</a>");
                chuoi.Append( "</div>");
                chuoi.Append("</div>"); 
                var listImageadwcenter=(from a in db.tblConnectImages  join b in db.tblImages on a.idImg equals b.id where a.idCate==idParent1 && b.idCate==10  select b).ToList();
                if(listImageadwcenter.Count>0)
                {
                    chuoi.Append("<div class=\"Img_adwcenter\">");
                    for (int j = 0; j < listImageadwcenter.Count;j++ )
                    {
                        chuoi.Append("<a href=\"" + listImageadwcenter[j].Url + "\" title=\"" + listImageadwcenter[j].Name + "\"><img src=\"" + listImageadwcenter[j].Images + "\" alt=\"" + listImageadwcenter[j].Name + "\" /></a>");

                    }
                        
                    chuoi.Append("</div>");
                }
             
                chuoi.Append( "<div class=\"Content_cl_Content\">");
                chuoi.Append( "<div class=\"Content_1\">");
                chuoi.Append( "<div class=\"Left_Top_cl_Content\">");
                int idmenu = int.Parse(MenuParent[i].id.ToString());
                var listImageContent = db.tblConnectImages.Where(p => p.idCate == idmenu).ToList();
                List<int> MangImage = new List<int>();
                for(int k=0;k<listImageContent.Count;k++)
                {
                    int idimg =int.Parse(listImageContent[k].idImg.ToString());
                    MangImage.Add(idimg);
                }
                var listImages = db.tblImages.Where(p => p.idCate == 3 && p.Active == true && MangImage.Contains(p.id)).OrderBy(p => p.Ord).ToList();
                
                for (int x = 0; x < listImages.Count; x++)
                {
                    chuoi.Append( "<div class=\"Tear_AdwLeft\">");
                    chuoi.Append( "<a href=\"" + listImages[x].Url + "\" title=\"" + listImages[x].Name + "\">");
                    chuoi.Append( "<img src=\"" + listImages[x].Images + "\" alt=\"" + listImages[x].Name + "\" />");
                    chuoi.Append( "</a>");
                    chuoi.Append( " </div>");
                }
                chuoi.Append( "</div>");
                chuoi.Append( "<div class=\"Right_Top_cl_Content\">");

                var listProduct = db.tblProducts.Where(p => p.Active == true && Mang.Contains(p.idCate.ToString()) && p.ViewHomes == true).OrderBy(p => p.Ord).ToList();
                for (int y = 0; y < listProduct.Count; y++)
                {
                    chuoi.Append( " <div class=\"Tear_pd\">");
                    chuoi.Append( "<div class=\"img_Tearpd\">");
                    chuoi.Append( "<a href=\"/"+ listProduct[y].Tag + "-dt\" title=\"" + listProduct[y].Name + "\">");
                    chuoi.Append( "<img src=\"" + listProduct[y].ImageLinkThumb + "\" alt=\"" + listProduct[y].Name + "\" title=\"" + listProduct[y].Name + "\"/>");
                    chuoi.Append( "</a>");
                    chuoi.Append( "</div>");
                    float Giany = float.Parse(listProduct[y].Price.ToString());
                    float Giakm = float.Parse(listProduct[y].PriceSale.ToString());
                    float km = 100 - (Giakm * 100 / Giany);
                    chuoi.Append( "<div class=\"box_quatang1\">");
                    chuoi.Append( "</div>");
                    chuoi.Append( "<h3><a class=\"Namepd\" href=\"/" + listProduct[y].Tag + "-dt\" title=\"" + listProduct[y].Name + "\">" + listProduct[y].Name + "</a></h3>");

                    chuoi.Append( "<span class=\"Price\">Giá NY: <span>" + string.Format("{0:#,#}", listProduct[y].Price) + " đ</span></span>");
                    string hoigia = "";
                    string note = "";
                    if (listProduct[y].PriceSaleActive>1)
                    {
                        hoigia = "Lấy giá tốt hơn";
                        chuoi.Append("<span class=\"PriceSale\">Giá KM : <span>" + string.Format("{0:#,#}", listProduct[y].PriceSaleActive) + " đ</span></span>");
                        if (listProduct[y].PriceSaleActive > listProduct[y].PriceSale)
                        {
                            note = ". Ngoài ra chúng tôi có thể giảm thêm " + string.Format("{0:#,#}", listProduct[y].PriceSaleActive - listProduct[y].PriceSale) + "đ hoặc cao hơn nữa cho bạn khi liên hệ !";
                            Giakm = float.Parse(listProduct[y].PriceSaleActive.ToString());
                        }
                    }
                    else
                    {
                        hoigia = "Lấy giá tốt nhất";
                        chuoi.Append("<span class=\"PriceSale\">Giá KM : <span>Liên hệ</span></span>");
                    }
                  
                    chuoi.Append( "<div class=\"tietkiem\">");
                    chuoi.Append( "<span class=\"tk1\">Chúng tôi giảm thêm giá khi bạn gọi điện hoặc ấn vào : </span>");
                    chuoi.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProduct[y].id + "\" onClick=\"javascript:return Laygia('lg" + listProduct[y].id + "','" + listProduct[y].Name + "','" + String.Format("{0:#,#}",Giakm) + "','"+note+"');\" title=\"Để lấy giá hỗ trợ rẻ nhất Hà Nội\">"+hoigia+"</span></div>");
                    chuoi.Append("</div>");
                    chuoi.Append( "</div>");
                }

                chuoi.Append( " </div>");
                chuoi.Append( " </div>");
                chuoi.Append( " </div>");
                chuoi.Append( " </div>");
                chuoi.Append( " <div class=\"Clear\"></div>");
                Mangphantu.Clear();
            }
            ViewBag.ListProduct = chuoi;
            return PartialView();
        }
        //[OutputCache(CacheProfile = "Cachecatchall")]

        public ActionResult ProductDetail(string tag)
        {
              string url = Request.Url.ToString();
            string[] mang = url.Split('/');
            if (mang.Length > 4)
            {
                return Redirect("/" + tag + "-dt");
            }
            var Product = db.tblProducts.FirstOrDefault(p => p.Tag == tag);
            int id = Product.id;
            int visit = int.Parse(Product.Visit.ToString());
            if (visit > 0)
            {
                Product.Visit = Product.Visit + 1;
                db.SaveChanges();
            }
            else
            {
                Product.Visit = Product.Visit + 1;
                db.SaveChanges();
            }
            ViewBag.Title = "<title>" + Product.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + Product.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + Product.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + Product.Keyword + "\" /> ";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://thietbivesinhinax.vn/" + StringClass.NameToTag(tag) + "-dt\" />";
            meta += "<meta itemprop=\"name\" content=\"" + Product.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + Product.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + Product.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://thietbivesinhinax.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + Product.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta; 
            //Load Images Liên Quan
            var listImage = db.tblImageProducts.Where(p => p.idProduct == id).ToList();
            string chuoiimages = "";
            for (int i = 0; i < listImage.Count; i++)
            {
                chuoiimages += " <li class=\"Tear_pl\"><a href=\"" + listImage[i].Images + "\" rel=\"prettyPhoto[gallery1]\" title=\"" + listImage[i].Name + "\"><img src=\"" + listImage[i].Images + "\"   alt=\"" + listImage[i].Name + "\" /></a></li>";
            }
            ViewBag.chuoiimage = chuoiimages;
            int idMenu = int.Parse(Product.idCate.ToString());
            ViewBag.Nhomsp = db.tblGroupProducts.First(p => p.id == idMenu).Name;
            string code = Product.Code;
            //Load sản phẩm đổng bộ
            var ProductSyn = db.ProductConnects.Where(p => p.idpd == code).ToList();
            List<int> exceptionList = new List<int>();
            for (int i = 0; i < ProductSyn.Count; i++)
            {
                exceptionList.Add(int.Parse(ProductSyn[i].idSyn.ToString()));
            }
            StringBuilder chuoisym = new StringBuilder();
            var listSyn = db.tblProductSyns.Where(x => exceptionList.Contains(x.id) && x.Active==true).ToList();
            if (listSyn.Count > 0)
            {
                chuoisym.Append("<div id=\"Top7\">");
                chuoisym.Append("<div id=\"iCon\"></div>");
                chuoisym.Append("<div id=\"Content_Top7\"><p>Hiện tại sản phẩm <span>" + Product.Name + "</span> có giá rẻ hơn khi mua sản phẩm đồng bộ, bạn có thể xem sản phẩm đồng bộ này</p>");
                chuoisym.Append("<ul>");
                for (int i = 0; i < listSyn.Count; i++)
                {
                    chuoisym .Append("<li><a href=\"/Syn/" + listSyn[i].Tag + "\" title=\"" + listSyn[i].Name + "\" class=\"Syn\" rel=\"nofollow\"><span></span> " + listSyn[i].Name + "</a></li>");
                }
              chuoisym.Append("</ul>");
               chuoisym.Append(" </div>");
            chuoisym.Append("</div>");
            }
            ViewBag.chuoisym = chuoisym;
            int idCate = int.Parse(Product.idCate.ToString());
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a> /" + UrlProduct(idCate);
            // Load màu sản phẩm
            StringBuilder chuoicolor =new StringBuilder();
            var listcolor = db.tblColorProducts.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            var kiemtracolor = db.tblConnectColorProducts.Where(p => p.idPro == id).ToList();
            if (kiemtracolor.Count > 0)
            {
                chuoicolor.Append("<div id=\"Top4\">");
                chuoicolor.Append("<span> Màu sản phẩm : </span>");
                chuoicolor.Append(" <div id=\"Left_Top4\">");
                for (int i = 0; i < listcolor.Count; i++)
                {
                    int idcolor = int.Parse(listcolor[i].id.ToString());
                    var listconnectcolor = db.tblConnectColorProducts.Where(p => p.idPro == id && p.idColor == idcolor).ToList();
                    if (listconnectcolor.Count > 0)
                    {
                      chuoicolor.Append( "<span class=\"Mau\" style=\"background:url(" + listcolor[i].Images + ") no-repeat; background-size:100%\"></span> ");
                    }

                }
                chuoicolor.Append( "</div>");
                chuoicolor.Append("</div>");
            }

            ViewBag.color = chuoicolor;


            //load tính năng
            StringBuilder chuoifun = new StringBuilder();
            var listfuc = db.tblFunctionProducts.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            var checkfun = db.tblConnectFunProuducts.Where(p => p.idPro == id).ToList();
            if (checkfun.Count > 0)
            {

                chuoifun.Append(" <div id=\"Tech\">");
                chuoifun.Append("<span class=\"tinhnang\">Những tính năng nổi bật của " + Product.Name + "</span>");
                for (int i = 0; i < listfuc.Count; i++)
                {
                    int idfun = int.Parse(listfuc[i].id.ToString());
                    var connectfun = db.tblConnectFunProuducts.Where(p => p.idFunc == idfun && p.idPro == id).ToList();
                    if (connectfun.Count > 0)
                    {
                        chuoifun.Append("<div class=\"Tear_tech\">");
                        chuoifun.Append("<span class=\"imagetech\" style=\"background:url(" + listfuc[i].Images + ") no-repeat center center scroll transparent;\"></span>");
                        chuoifun.Append("<span class=\"Destech\">" + listfuc[i].Name + "</span>");
                        chuoifun.Append("<p>Xem chi tiết về " + listfuc[i].Name + " <a href=\"" + listfuc[i].Url + "\" title=\"" + listfuc[i].Name + "\">Tại đây &raquo;</a></p>");
                        chuoifun.Append("</div>");
                    }
                }
                chuoifun.Append("</div>");
            }
            ViewBag.chuoifun = chuoifun;
            //load files
            var listfile = db.tblFiles.Where(p => p.idp == id).ToList();
            if (listfile.Count > 0)
            {
                ViewBag.files = "<div class=\"huondansudung\"><a href=\"" + listfile[0].File + "\" title=\"" + listfile[0].Name + "\"><span></span>Tải Hướng dẫn sử dụng " + Product.Name + "</a></div>";
            }
            return View(Product);
        }
        public ActionResult Command(FormCollection collection, string tag)
        {
            if (collection["btnOrder"] != null)
            {

                Session["idProduct"] = collection["idPro"];
                Session["idMenu"] = collection["idCate"];
                Session["OrdProduct"] = collection["txtOrd"];
                Session["Url"] = Request.Url.ToString();
                return RedirectToAction("OrderIndex", "Order");

            }
            return View();
        }
        public PartialViewResult RightProductDetail(string tag)
        {

            tblProduct Product = db.tblProducts.FirstOrDefault(p => p.Tag == tag);
            tblConfig tblconfig = db.tblConfigs.First();
            StringBuilder chuoisupport = new StringBuilder();
            var listSupport = db.tblSupports.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < listSupport.Count; i++)
            {
                chuoisupport.Append("<div class=\"Line_Buttom\"></div>");
                chuoisupport.Append("<div class=\"Tear_Supports\">");
                chuoisupport.Append("<div class=\"Left_Tear_Support\">");
                chuoisupport.Append("<span class=\"htv1\">" + listSupport[i].Mission + ":</span>");
                chuoisupport.Append("<span class=\"htv2\">" + listSupport[i].Name + " :</span>");
                chuoisupport.Append("</div>");
                chuoisupport.Append("<div class=\"Right_Tear_Support\">");
                chuoisupport.Append("<a href=\"ymsgr:sendim?" + listSupport[i].Yahoo + "\">");
                chuoisupport.Append("<img src=\"http://opi.yahoo.com/online?u=" + listSupport[i].Yahoo + "&m=g&t=1\" alt=\"Yahoo\" class=\"imgYahoo\" />");
                chuoisupport.Append(" </a>");
                chuoisupport.Append("<a href=\"Skype:" + listSupport[i].Skyper + "?chat\">");
                chuoisupport.Append("<img class=\"imgSkype\" src=\"/Content/Display/iCon/skype-icon.png\" title=\"" + listSupport[i].Name + "\" alt=\"" + listSupport[i].Name + "\">");
                chuoisupport.Append("</a>");
                chuoisupport.Append("</div>");
                chuoisupport.Append("</div>");
            }
            ViewBag.chuoisupport = chuoisupport;

            //lIST Menu
            int idCate = int.Parse(Product.idCate.ToString());
            string nidparent = db.tblGroupProducts.Find(idCate).ParentID.ToString();
            if(nidparent!=null && nidparent!="")
            {
                int idparent = int.Parse(db.tblGroupProducts.Find(idCate).ParentID.ToString());
                string chuoimenu = "";
                var listGroupProduct = db.tblGroupProducts.Where(p => p.ParentID == idparent && p.Active == true).OrderBy(p => p.Ord).ToList();
                for (int i = 0; i < listGroupProduct.Count; i++)
                {

                    chuoimenu += "<h2><a href=\"/" + listGroupProduct[i].Tag + ".html\" title=\"\">› " + listGroupProduct[i].Name + "</a></h2>";

                }
                ViewBag.chuoimenu = chuoimenu;
            }
            
            //Load sản phẩm liên quan
            StringBuilder chuoiproduct = new StringBuilder();
            var listProduct = db.tblProducts.Where(p => p.Active == true && p.idCate == idCate).OrderByDescending(p => p.Visit).OrderBy(p => p.Ord).Take(5).ToList();
            for (int i = 0; i < listProduct.Count; i++)
            {

                chuoiproduct.Append("<div class=\"Tear_pd\">");
                chuoiproduct.Append("<div class=\"img_Tearpd\">");
                chuoiproduct.Append("<a href=\"/" + listProduct[i].Tag + "-dt\" title=\"\"><img src=\"" + listProduct[i].ImageLinkThumb + "\" alt=\"" + listProduct[i].Name + "\" /></a>");
                chuoiproduct.Append("</div>");
                float Giany = float.Parse(listProduct[i].Price.ToString());
                float Giakm = float.Parse(listProduct[i].PriceSale.ToString());
                float km = 100 - (Giakm * 100 / Giany);
                //chuoiproduct+="<div class=\"Sale\"><span>-"+Convert.ToInt32(km)+"%</span></div>";
                chuoiproduct.Append("<div class=\"box_quatang1\">");
                chuoiproduct.Append("</div>");
                chuoiproduct.Append("<h3><a class=\"Namepd\" href=\"/" + listProduct[i].Tag + "-dt\" title=\"" + listProduct[i].Name + "\">" + listProduct[i].Name + "</a></h3>");
                chuoiproduct.Append("<span class=\"Price\">Giá : <span>" + string.Format("{0:#,#}", listProduct[i].Price) + " đ</span></span>");
                int giakm = int.Parse(listProduct[i].PriceSale.ToString());
                string note = "";
                if (giakm > 50)
                {
                    if(listProduct[i].PriceSaleActive>1)
                    {
                        chuoiproduct.Append("<span class=\"PriceSale\">Giá KM : <span>" + string.Format("{0:#,#}", listProduct[i].PriceSaleActive) + "</span></span>");
                        if (listProduct[i].PriceSaleActive > listProduct[i].PriceSale)
                        {
                            note = ". Ngoài ra chúng tôi có thể giảm thêm " + string.Format("{0:#,#}", listProduct[i].PriceSaleActive - listProduct[i].PriceSale) + "đ hoặc cao hơn nữa cho bạn khi liên hệ !";
                            Giakm = float.Parse(listProduct[i].PriceSaleActive.ToString());
                        }
                    }
                    else
                    {
                        chuoiproduct.Append("<span class=\"PriceSale\">Giá KM : <span>Liên hệ</span></span>");
                    }
                    
                    chuoiproduct.Append("<div class=\"tietkiem\">");
                    chuoiproduct.Append("<span class=\"tk1\">Cam kết giá rẻ nhất khi liên hệ</span>");
                    chuoiproduct.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProduct[i].id + "\" onClick=\"javascript:return Laygia('lg" + listProduct[i].id + "','" + listProduct[i].Name + "','" + String.Format("{0:#,#}", Giakm) + "','"+note+"');\" title=\"Để lấy giá hỗ trợ rẻ nhất Hà Nội\">Lấy giá tốt nhất</span></div>");
                    chuoiproduct.Append("</div>");
                }
                else
                    chuoiproduct.Append(" <p class=\"qua\">Vui lòng liên hệ để nhận được giá khuyến mại</p>");
                
                chuoiproduct.Append("</div>");
            }
            ViewBag.chuoiproduct = chuoiproduct;
            return PartialView(tblconfig);
        }
        public ActionResult SearchProduct(string tag)
        {
            string chuoi = "";
            ViewBag.Title = "<title> Tìm kiếm : " + tag + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tag + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tag + "\" /> ";
            chuoi += "   <div class=\"Name_Cate\">";

            chuoi += " <div class=\"Left_Name_Cate\"></div>";
            chuoi += " <div class=\"Center_Name_Cate\">";
            chuoi += "<h1>" + tag + "</h1>";
            chuoi += " </div>";
            chuoi += " <div class=\"Right_Name_Cate\"></div>";
            chuoi += " </div>";
            chuoi += "<div class=\"Clear\"></div>";
            chuoi += "<div class=\"Content_RightCate\">";
            var listProduct = db.tblProducts.Where(p => p.Active == true && p.Name.Contains(tag)).OrderBy(p => p.Ord).ToList();
            for (int j = 0; j < listProduct.Count; j++)
            {
                int idcate = int.Parse(listProduct[j].idCate.ToString());
                var listgroup = db.tblGroupProducts.Where(p => p.id == idcate && p.Active == true).ToList();
                if (listgroup.Count > 0)
                {
                    string GroupProduct = db.tblGroupProducts.First(p => p.id == idcate).Tag;
                    chuoi += " <div class=\"Tear_pd\">";
                    chuoi += "<div class=\"img_Tearpd\">";
                    chuoi += "<a href=\"/" + listProduct[j].Tag + "-dt\" title=\"" + listProduct[j].Name + "\">";
                    chuoi += "<img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" />";
                    chuoi += "</a>";
                    chuoi += "</div>";
                    float Giany = float.Parse(listProduct[j].Price.ToString());
                    float Giakm = float.Parse(listProduct[j].PriceSale.ToString());
                    float km = 100 - (Giakm * 100 / Giany);
                    chuoi += "<div class=\"Sale\"><span>-" + Convert.ToInt32(km) + "%</span></div>";
                    chuoi += "<div class=\"box_quatang1\">";
                    chuoi += "</div>";
                    chuoi += "<a class=\"Namepd\" href=\"/"+ listProduct[j].Tag + "-dt\" title=\"" + listProduct[j].Name + "\">" + listProduct[j].Name + "</a>";
                    chuoi += "<span class=\"Price\">Giá : <span>" + string.Format("{0:#,#}", listProduct[j].Price) + " đ</span></span>";
                    if (listProduct[j].PriceSaleActive>1)
                    chuoi += "<span class=\"PriceSale\">Giá KM : <span>" + string.Format("{0:#,#}", listProduct[j].PriceSaleActive) + "</span></span>";
                    chuoi += " <p class=\"qua\">Cam kết giá rẻ nhất khi liên hệ</p>";
                    //chuoi += "<div class=\"sevices\">";

                    //if (listProduct[j].Status == true)
                    //{ chuoi += "<span class=\"Status\"></span>"; }
                    //else
                    //{ chuoi += "<span class=\"StatusNo\"></span>"; }

                    //chuoi += "<span class=\"Transport\"><span class=\"icon\">";
                    //if (listProduct[j].Transport == true)
                    //{
                    //    chuoi += "</span> Toàn quốc</span>";
                    //}
                    //else
                    //{ chuoi += "</span> Liên hệ</span>"; }


                    //chuoi += "</div>";
                    chuoi += "</div>";
                }
            }
            chuoi += "<div class=\"Leight\"></div>";
            chuoi += " </div>";
            ViewBag.chuoisanpham = chuoi;
            return View();
        }
        //[OutputCache(CacheProfile = "tag")]

        public ActionResult ListProduct(string tag)
        {
            StringBuilder chuoi = new StringBuilder();
            string url = Request.Url.ToString();
            string[] mang = url.Split('/');
             if(mang.Length>4)
             {
                 return Redirect("/"+tag+".html");
             }
            var GroupProduct = db.tblGroupProducts.First(p => p.Tag == tag);
            ViewBag.Title = "<title>" + GroupProduct.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + GroupProduct.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + GroupProduct.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + GroupProduct.Keyword + "\" /> ";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://thietbivesinhinax.vn/" + StringClass.NameToTag(tag) + ".html\" />";
            meta += "<meta itemprop=\"name\" content=\"" + GroupProduct.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + GroupProduct.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + GroupProduct.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://thietbivesinhinax.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + GroupProduct.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta; 
            int idCate = GroupProduct.id;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a> /" + UrlProduct(idCate) + "/ <h1>"+GroupProduct.Title+"</h1>";
            var listGroupProduct = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idCate).OrderBy(p => p.Ord).ToList();
            if (listGroupProduct.Count > 0)
            {
                chuoi.Append("<div class=\"NamePd\"><span>" + GroupProduct.Name + "</span></div>");
                var listImageadwcenter = (from a in db.tblConnectImages join b in db.tblImages on a.idImg equals b.id where a.idCate == idCate && b.idCate == 10 select b).ToList();
                if (listImageadwcenter.Count > 0)
                {
                    chuoi.Append("<div class=\"Img_adwcenter\">");
                    for (int j = 0; j < listImageadwcenter.Count; j++)
                    {
                        chuoi.Append("<a href=\"" + listImageadwcenter[j].Url + "\" title=\"" + listImageadwcenter[j].Name + "\"><img src=\"" + listImageadwcenter[j].Images + "\" alt=\"" + listImageadwcenter[j].Name + "\" /></a>");

                    }

                    chuoi.Append("</div>");
                }
                chuoi.Append("<div class=\"Description_List\">");
                chuoi.Append("<div class=\"Content_Description_List\">" + GroupProduct.Content + "</div>");
                chuoi.Append("</div>");
                for (int i = 0; i < listGroupProduct.Count; i++)
                {
                    chuoi.Append("   <div class=\"Name_Cate\">");
                    chuoi.Append("<div class=\"Left_Name_Cate\"></div>");
                    chuoi.Append(" <div class=\"Center_Name_Cate\">");
                    chuoi.Append("<h2>" + listGroupProduct[i].Name + "</h2>");
                    chuoi.Append("</div>");
                    chuoi.Append("<div class=\"Right_Name_Cate\"></div>");
                    chuoi.Append(" </div>");
                    chuoi.Append("<div class=\"Clear\"></div>");

                    chuoi.Append("<div class=\"Content_RightCate\">");
                    int idcate = int.Parse(listGroupProduct[i].id.ToString());
                    var listProduct = db.tblProducts.Where(p => p.idCate == idcate && p.Active == true).OrderBy(p => p.Ord).ToList();
                    for (int j = 0; j < listProduct.Count; j++)
                    {
                        chuoi.Append(" <div class=\"Tear_pd\">");
                        chuoi.Append("<div class=\"img_Tearpd\">");
                        chuoi.Append("<a href=\"/" + listProduct[j].Tag + "-dt\" title=\"" + listProduct[j].Name + "\">");
                        chuoi.Append("<img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" title=\"" + listProduct[j].Name + "\" />");
                        chuoi.Append("</a>");
                        chuoi.Append("</div>");
                        float Giany = float.Parse(listProduct[j].Price.ToString());
                        float Giakm = float.Parse(listProduct[j].PriceSale.ToString());
                        float km = 100 - (Giakm * 100 / Giany);
                        //if(Giakm>1)
                        //{ 
                        //chuoi.Append("<div class=\"Sale\"><span>-" + Convert.ToInt32(km) + "%</span></div>");
                        //}
                        chuoi.Append("<div class=\"box_quatang1\">");
                        chuoi.Append("</div>");
                        chuoi.Append("<h3><a class=\"Namepd\" href=\"/" + listProduct[j].Tag + "-dt\" title=\"" + listProduct[j].Name + "\">" + listProduct[j].Name + "</a></h3>");
                        chuoi.Append("<span class=\"Price\">Giá : <span>" + string.Format("{0:#,#}", listProduct[j].Price) + " đ</span></span>");
                        int giakm = int.Parse(listProduct[j].PriceSale.ToString());
                        string note = "";
                        if (giakm > 50)
                        {
                            string hoigia = "";
                            if (listProduct[j].PriceSaleActive>1)
                            {
                                hoigia = "Lấy giá tốt hơn";
                                chuoi.Append("<span class=\"PriceSale\">Giá KM : <span>"+string.Format("{0:#,#}", listProduct[j].PriceSaleActive) +"</span></span>");
                                if (listProduct[j].PriceSaleActive > listProduct[j].PriceSale)
                                {
                                    note = ". Ngoài ra chúng tôi có thể giảm thêm " + string.Format("{0:#,#}", listProduct[j].PriceSaleActive - listProduct[j].PriceSale) + "đ hoặc cao hơn nữa cho bạn khi liên hệ !";
                                    Giakm = float.Parse(listProduct[j].PriceSaleActive.ToString());
                                }
                            }
                            else
                            {
                                hoigia = "Lấy giá tốt nhất";
                                chuoi.Append("<span class=\"PriceSale\">Giá KM : <span>Liên hệ</span></span>");
                            }
                        
                            chuoi.Append("<div class=\"tietkiem\">");
                            chuoi.Append("<span class=\"tk1\">Cam kết giá rẻ nhất khi liên hệ</span>");
                            chuoi.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProduct[j].id + "\" onClick=\"javascript:return Laygia('lg" + listProduct[j].id + "','" + listProduct[j].Name + "','" + String.Format("{0:#,#}", Giakm) + "','" + note + "');\" title=\"Để lấy giá hỗ trợ rẻ nhất Hà Nội\">" + hoigia + "</span></div>");
                            chuoi.Append("</div>");
                        }
                        else
                            chuoi.Append(" <p class=\"qua\">Vui lòng liên hệ để nhận được giá khuyến mại</p>");
                        // chuoi.Append("<div class=\"sevices\">");

                        //if (listProduct[j].Status == true)
                        //{ chuoi.Append("<span class=\"Status\"></span>"); }
                        //else
                        //{ chuoi.Append("<span class=\"StatusNo\"></span>"); }

                        //chuoi.Append("<span class=\"Transport\"><span class=\"icon\">");
                        //if (listProduct[j].Transport == true)
                        //{
                        //    chuoi.Append("</span> Toàn quốc</span>");
                        //}
                        //else
                        //{ chuoi.Append("</span> Liên hệ</span>"); }


                        //chuoi.Append("</div>");
                        chuoi.Append("</div>");
                    }
                    chuoi.Append("<div class=\"Leight\"></div>");
                    chuoi.Append(" </div>");
                }
            }
            else
            {
                chuoi.Append("   <div class=\"Name_Cate\">");

                chuoi.Append(" <div class=\"Left_Name_Cate\"></div>");
                chuoi.Append(" <div class=\"Center_Name_Cate\">");
                chuoi.Append("<span>" + GroupProduct.Name + "</span>");
                chuoi.Append(" </div>");
                var listImageadwcenter = (from a in db.tblConnectImages join b in db.tblImages on a.idImg equals b.id where a.idCate == idCate && b.idCate == 10 select b).ToList();
                if (listImageadwcenter.Count > 0)
                {
                    chuoi.Append("<div class=\"Img_adwcenter\">");
                    for (int j = 0; j < listImageadwcenter.Count; j++)
                    {
                        chuoi.Append("<a href=\"" + listImageadwcenter[j].Url + "\" title=\"" + listImageadwcenter[j].Name + "\"><img src=\"" + listImageadwcenter[j].Images + "\" alt=\"" + listImageadwcenter[j].Name + "\" /></a>");

                    }

                    chuoi.Append("</div>");
                }
                chuoi.Append(" <div class=\"Right_Name_Cate\"></div>");

                chuoi.Append(" </div>");
                chuoi.Append("<div class=\"Description_List\">");
                chuoi.Append("<div class=\"Content_Description_List\">  " + GroupProduct.Content + "   </div>");
                chuoi.Append("</div>");
                chuoi.Append("<div class=\"Clear\"></div>");
                chuoi.Append("<div class=\"Content_RightCate\">");
                int idcate = int.Parse(GroupProduct.id.ToString());
                var listProduct = db.tblProducts.Where(p => p.idCate == idcate && p.Active == true).OrderBy(p => p.Ord).ToList();
                for (int j = 0; j < listProduct.Count; j++)
                {
                    chuoi.Append(" <div class=\"Tear_pd\">");
                    chuoi.Append("<div class=\"img_Tearpd\">");
                    chuoi.Append("<a href=\"/" + listProduct[j].Tag + "-dt\" title=\"" + listProduct[j].Name + "\">");
                    chuoi.Append("<img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" title=\"" + listProduct[j].Name + "\" />");
                    chuoi.Append("</a>");
                    chuoi.Append("</div>");
                    float Giany = float.Parse(listProduct[j].Price.ToString());
                    float Giakm = float.Parse(listProduct[j].PriceSale.ToString());
                    float km = 100 - (Giakm * 100 / Giany);
                    //if (Giakm > 1)
                    //{
                    //    chuoi.Append("<div class=\"Sale\"><span>-" + Convert.ToInt32(km) + "%</span></div>");
                    //}
                    chuoi.Append("<div class=\"box_quatang1\">");
                    chuoi.Append("</div>");
                    chuoi.Append("<a class=\"Namepd\" href=\"/" + listProduct[j].Tag + "-dt\" title=\"" + listProduct[j].Name + "\">" + listProduct[j].Name + "</a>");
                    chuoi.Append("<span class=\"Price\">Giá : <span>" + string.Format("{0:#,#}", listProduct[j].Price) + " đ</span></span>");
                    int giakm = int.Parse(listProduct[j].PriceSale.ToString());
                    string note = "";
                    if (giakm > 50)
                    {
                        string hoigia = "";
                        if (listProduct[j].PriceSaleActive > 1)
                        {
                            hoigia = "Lấy giá tốt hơn";
                            chuoi.Append("<span class=\"PriceSale\">Giá KM : <span>" + string.Format("{0:#,#}", listProduct[j].PriceSaleActive) + "</span></span>");
                            if (listProduct[j].PriceSaleActive > listProduct[j].PriceSale)
                            {
                                note = ". Ngoài ra chúng tôi có thể giảm thêm " + string.Format("{0:#,#}", listProduct[j].PriceSaleActive - listProduct[j].PriceSale) + "đ hoặc cao hơn nữa cho bạn khi liên hệ!";
                                Giakm = float.Parse(listProduct[j].PriceSaleActive.ToString());
                            }
                        }
                        else
                        {
                            hoigia = "Lấy giá tốt nhất";
                            chuoi.Append("<span class=\"PriceSale\">Giá KM : <span>Liên hệ</span></span>");
                        }
                        chuoi.Append("<div class=\"tietkiem\">");
                        chuoi.Append("<span class=\"tk1\">Cam kết giá rẻ nhất khi liên hệ</span>");
                        chuoi.Append("<div class=\"Laygiakm\"><span class=\"laygia lg" + listProduct[j].id + "\" onClick=\"javascript:return Laygia('lg" + listProduct[j].id + "','" + listProduct[j].Name + "','" + String.Format("{0:#,#}", Giakm) + "','" + note + "');\" title=\"Để lấy giá hỗ trợ rẻ nhất Hà Nội\">" + hoigia + "</span></div>");
                        chuoi.Append("</div>");
                    }
                    else
                        chuoi.Append(" <p class=\"qua\">Vui lòng liên hệ để nhận được giá khuyến mại</p>");
                    //chuoi.Append("<div class=\"sevices\">");
                    //if (listProduct[j].Status == true)
                    //{ chuoi.Append("<span class=\"Status\"></span>"); }
                    //else
                    //{ chuoi.Append("<span class=\"StatusNo\"></span>"); }

                    //chuoi.Append("<span class=\"Transport\"><span class=\"icon\">");
                    //if (listProduct[j].Transport == true)
                    //{
                    //    chuoi.Append("</span> Toàn quốc</span>");
                    //}
                    //else
                    //{ chuoi.Append("</span> Liên hệ</span>"); }


                    //chuoi.Append("</div>");
                    chuoi.Append("</div>");
                }
                chuoi.Append("<div class=\"Leight\"></div>");
                chuoi.Append(" </div>");
            }
            ViewBag.chuoisanpham = chuoi;
            return View();

        }
        public PartialViewResult PartialRightListProduct(string tag)
        {
            StringBuilder chuoi = new StringBuilder();

            var GroupProduct = db.tblGroupProducts.First(p => p.Tag == tag);
            int idCate = GroupProduct.id;
            StringBuilder chuoifun = new StringBuilder();
            chuoifun.Append("<div class=\"Tear_Menu\">");
            chuoifun.Append("<div class=\"nVar_Menu\">");
            chuoifun.Append("<span> Cùng danh mục</span>");
            chuoifun.Append("</div>");
            chuoifun.Append("<div class=\"Content_Menu\">");
            chuoifun.Append("<ul>");
            var listGroupProduct = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idCate).OrderBy(p => p.Ord).ToList();

            for (int i = 0; i < listGroupProduct.Count; i++)
            {
                chuoifun.Append("<li>");
                chuoifun.Append("<h2><a href=\"/" + listGroupProduct[i].Tag + ".html\" title=\"" + listGroupProduct[i].Name + "\"><span>&bull;</span> " + listGroupProduct[i].Name + "</a></h2>");
                int ParentID = listGroupProduct[i].id;
                var listGroupProduct1 = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == ParentID).OrderBy(p => p.Ord).ToList();
                if (listGroupProduct.Count > 0)
                {
                    chuoifun.Append("<ul>");
                    for (int j = 0; j < listGroupProduct1.Count; j++)
                    {

                        chuoifun.Append("<li><h2><a href=\"/" + listGroupProduct1[j].Tag + ".html\" title=\"" + listGroupProduct1[j].Name + "\"><span>&raquo; </span> " + listGroupProduct1[j].Name + "</a></h2></li>");
                    };
                    chuoifun.Append("</ul>");
                }
                chuoifun.Append("</li>");
            }
            chuoifun.Append("</ul>");
            chuoifun.Append("</div>");
            chuoifun.Append("</div>");

            ViewBag.nchuoi = chuoifun;


            //Load sản phẩm liên quan
            chuoi.Append("<div class=\"Tear_Menu\">");
            chuoi.Append("<div class=\"nVar_Menu\">");
            chuoi.Append("<span> Sản phẩm khác </span>");
            chuoi.Append("</div>");
            chuoi.Append("<div class=\"Content_Menu\">");
            var listGroup = db.tblGroupProducts.Where(p => p.Active == true && p.Tag != tag && p.ParentID == null).OrderBy(p => p.Ord).ToList();
            chuoi.Append("<ul>");
            for (int i = 0; i < listGroup.Count; i++)
            {

                chuoi.Append("<li>");
                chuoi.Append("<a href=\"/" + listGroup[i].Tag + ".html\" rel=\"nofollow\" title=\"" + listGroup[i].Name + "\"><span>&bull;</span> " + listGroup[i].Name + "</a>");

                int idCate1 = listGroup[i].id;
                var listGroup1 = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idCate1).OrderBy(p => p.Ord).ToList();

                chuoi.Append("<ul>");
                for (int j = 0; j < listGroup1.Count; j++)
                {


                    chuoi.Append("<li><a href=\"/" + listGroup1[j].Tag + ".html\" rel=\"nofollow\" title=\"" + listGroup1[j].Name + "\"><span>&raquo; </span>" + listGroup1[j].Name + "</a></li>");
                }
                chuoi.Append("</ul>");
                chuoi.Append("</li>");



            }
            chuoi.Append("</ul>");
            chuoi.Append("</div>");
            chuoi.Append("</div>");
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {
            string tag = collection["txtSearch"];
            return Redirect("/Search/" + tag + "");
        }
    }
}
