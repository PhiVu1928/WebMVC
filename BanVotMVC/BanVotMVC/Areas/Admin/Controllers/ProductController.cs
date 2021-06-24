using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using PagedList;
using PagedList.Mvc;

namespace BanVotMVC.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string SearchString, int page = 1 , int pagesize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListallProduct(SearchString, page, pagesize);
            ViewBag.SearchString = SearchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            setviewbag();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            setviewbag();
            var dao = new UserDao().ViewProductDetail(id);
            return View(dao);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var check = dao.CheckProduct(product.Name);
                if (check == 1)
                {
                    product.CreatedDate = DateTime.Now;
                    long id = dao.InsertProduct(product);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Sản phẩm đã tồn tại");
                }
            }
            return View("Create");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.UpdateProduct(product);
                if(result)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thất bại");
                }
            }
            return View("Edit");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().DeleteProduct(id);
            return View("Index");
        }
        public void setviewbag(long? selectedID = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAllProduct(), "ID", "Name", selectedID);
        }
    }
}