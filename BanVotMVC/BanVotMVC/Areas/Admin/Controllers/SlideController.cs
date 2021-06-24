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
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index(string SearchString, int page = 1, int pagesize = 10)
        {
            var slidedao = new SlideDao();
            var model = slidedao.ListAllSlide(SearchString, page, pagesize);
            ViewBag.SearchString = SearchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var slidedao = new SlideDao();
            var model = slidedao.ViewSlideDeltail(id);
            return View(model);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new SlideDao().DeleteSlide(id);
            return View("Index");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Slide slide)
        {
            if(ModelState.IsValid)
            {
                var slidedao = new SlideDao();
                var check = slidedao.CheckSlide(slide.Name);
                if(check == 1)
                {
                    slide.CreatedDate = DateTime.Now;
                    long id = slidedao.InsertSlide(slide);
                    if(id > 0)
                    {
                        return RedirectToAction("Index", "Slide");
                    }    
                }    
                else
                {
                    ModelState.AddModelError("", "Slide này đã tồn tại !!!");
                }    
            }
            return View("Create");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Slide slide)
        {
            if(ModelState.IsValid)
            {
                var slidedao = new SlideDao();
                var result = slidedao.UpdateSlide(slide);
                if(result)
                {
                    return RedirectToAction("Index", "Slide");
                }    
                else
                {
                    ModelState.AddModelError("", "Sửa slide không thành công !!!");
                }    
            }
            return View("Edit");
        }
    }
}