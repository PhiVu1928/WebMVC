using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;

namespace BanVotMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Category(long CategoryId, int page = 1 , int pageSize = 12)
        {
            var slideDao = new SlideDao().SlideShow();
            ViewBag.Slider = slideDao;
            var ProductCategoryDao = new CategoryDao().ViewCategorybyID(CategoryId);
            ViewBag.ProductCategoryID = ProductCategoryDao;
            var ProductCategory = new CategoryDao().listCategory();
            ViewBag.ProductCategory = ProductCategory;
            var ProductDao = new ProductDao().ListallProductbyCateID(CategoryId, page, pageSize);
            return View(ProductDao);
        }
        public ActionResult Detail(long ProductId)
        {
            var SlideDao = new SlideDao().SlideShow();
            ViewBag.Slider = SlideDao;
            var ProductDao = new ProductDao().Viewdetail(ProductId);
            ViewBag.Category = new CategoryDao().ViewCategorybyID(ProductDao.CategoryID.Value);
            var ProductCategoryDao = new CategoryDao().listCategory();
            ViewBag.ProductCategory = ProductCategoryDao;
            return View(ProductDao);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyword, int page = 1 , int pageSize = 12)
        {
            var slideDao = new SlideDao().SlideShow();
            ViewBag.Slider = slideDao;            
            var ProductCategory = new CategoryDao().listCategory();
            ViewBag.ProductCategory = ProductCategory;
            var ProductDao = new ProductDao().SearchProduct(keyword, page, pageSize);
            return View(ProductDao);
        }
    }
}