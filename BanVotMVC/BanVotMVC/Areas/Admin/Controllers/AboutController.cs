using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;

namespace BanVotMVC.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        // GET: Admin/About
        public ActionResult Index()
        {
            var AboutDao = new AboutDao().Aboutdetail();
            return View(AboutDao);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = new AboutDao().ViewAboutDetail(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(About about)
        {
            if(ModelState.IsValid)
            {
                var AboutDao = new AboutDao();
                about.CreatedDate = DateTime.Now;
                long id = AboutDao.Insert(about);
                if(id > 0)
                {
                    return RedirectToAction("Create", "About");
                }    
                else
                {
                    ModelState.AddModelError("", "Thêm thất bại");
                }    
            }
            return View("Create");
        }
        [HttpPost]
        public ActionResult Edit(About about)
        {
            if(ModelState.IsValid)
            {
                var AboutDao = new AboutDao();
                var result = AboutDao.Edit(about);
                if(result)
                {
                    return RedirectToAction("Index", "About");
                }    
                else
                {
                    ModelState.AddModelError("", "Sửa thất bại");
                }    
            }
            return View("Edit");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new AboutDao().Delete(id);
            return View("Index");
        }
    }
}