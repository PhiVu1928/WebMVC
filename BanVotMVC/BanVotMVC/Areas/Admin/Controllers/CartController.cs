using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
using PagedList;
using PagedList.Mvc;

namespace BanVotMVC.Areas.Admin.Controllers
{
    public class CartController : Controller
    {
        // GET: Admin/Cart
        public ActionResult Index(string SearchString, int page = 1, int pagesize = 10)
        {
            var CartDao = new CartDao();
            var model = CartDao.ListAllCustomer(SearchString, page, pagesize);
            ViewBag.SearchString = SearchString;
            return View(model);
        }
        public ActionResult Detail(int id)
        {
            var CartDao = new CartDao();
            var model = CartDao.CartDetail(id);
            return View(model);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CartDao().Delete(id);
            return View("Index");
        }
    }
}