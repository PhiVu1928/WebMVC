using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using BanVotMVC.Common;
using BanVotMVC.Models;

namespace BanVotMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var ProductDao = new ProductDao();
            var model = ProductDao.ProductNew(6);
            ViewBag.ProductNew = model;
            var model2 = ProductDao.ProductFeature(6);
            ViewBag.ProductFeature = model2;
            var slider =  new SlideDao().SlideShow();
            ViewBag.Slider = slider;
            var ProductCategoryDao = new CategoryDao().ListAllProduct();
            ViewBag.ProductCategory = ProductCategoryDao;
            return View();
        }
        public ActionResult LeftMenu()
        {
            return PartialView();
        }
        public ActionResult MenuHeader()
        {
            var CategoryDao = new CategoryDao();
            var model = CategoryDao.listCategory();
            ViewBag.Category = model;
            var AboutDao = new AboutDao();
            var about = AboutDao.Aboutdetail();
            ViewBag.About = about;
            return PartialView();
        }
        public ActionResult Footer()
        {
            return PartialView();
        }
        public ActionResult StickyCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<Cart>();
            if(cart != null)
            {
                list = (List<Cart>)cart;
            }    
            return PartialView(list);
        }
    }
}