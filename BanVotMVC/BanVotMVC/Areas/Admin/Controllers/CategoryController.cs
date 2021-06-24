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
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string SearchString, int page = 1, int pagesize = 10)
        {
            var ProductCategoryDao = new CategoryDao();
            var model = ProductCategoryDao.ListallProductCategory(SearchString, page, pagesize);
            ViewBag.SearchString = SearchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateParent()
        {
            setviewbag();
            return View();
        }
        public ActionResult Edit(int id)
        {
            setviewbag();
            var dao = new CategoryDao().ViewProductCategoryDetail(id);
            return View(dao);
        }

        [HttpPost]
        public ActionResult CreateParent(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                var categorydao = new CategoryDao();
                var check = categorydao.CheckProductCategory(productCategory.Name);
                if (check == 1)
                {
                    productCategory.CreatedDate = DateTime.Now;
                    long id = categorydao.InsertProductCategory(productCategory);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Category");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên danh mục đã tồn tại !!!");
                }
            }
            return View("CreateParent");
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if(ModelState.IsValid)
            {
                var categorydao = new CategoryDao();
                var check = categorydao.CheckProductCategory(productCategory.Name);
                if (check == 1)
                {
                    productCategory.CreatedDate = DateTime.Now;
                    long id = categorydao.InsertProductCategory(productCategory);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Category");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên danh mục đã tồn tại !!!");
                }
            }
            return View("Create");
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory)
        {
            if(ModelState.IsValid)
            {
                var CategoryDao = new CategoryDao();
                var result = CategoryDao.UpdateProductCategory(productCategory);
                if(result)
                {
                    return RedirectToAction("Index", "Category");
                }    
                else
                {
                    ModelState.AddModelError("", "Sửa thất bại !!!");
                }    
            }
            return View("Edit");            
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryDao().DeleteProductCategory(id);
            return View("Index");
        }
        public void setviewbag(long? selectedID = null)
        {
            var dao = new CategoryDao();
            ViewBag.ParentID = new SelectList(dao.listAllparent(), "ID", "Name", selectedID);
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDao().ChageStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}