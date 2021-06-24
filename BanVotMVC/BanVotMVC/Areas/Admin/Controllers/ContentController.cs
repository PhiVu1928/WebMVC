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
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string SearchString, int page = 1, int pagesize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListallContent(SearchString, page, pagesize);
            ViewBag.SearchString = SearchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        public ActionResult Edit(int id)
        {
            SetViewBag();
            var dao = new UserDao().ViewContentDetail(id);
            return View(dao);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content content)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var chk = dao.checkContent(content.Name);
                if(chk == 0)
                {
                    long id = dao.InsertContent(content);
                    if(id > 0)
                    {
                        return RedirectToAction("Index", "Content");
                    }    
                }   
                else
                {
                    ModelState.AddModelError("", "Tên bài viết đã tồn tại");
                }    
            }
            return View("Create");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content content)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.UpdateContent(content);
                if (result)
                {
                    return RedirectToAction("Index", "Content");
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
            new UserDao().DeleteContent(id);

            return View("Index");
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedID);
        }
    }
}