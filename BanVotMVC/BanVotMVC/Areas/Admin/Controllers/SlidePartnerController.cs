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
    public class SlidePartnerController : BaseController
    {
        // GET: Admin/SlidePartner
        public ActionResult Index(string SearchString, int page = 1, int pagesize = 10)
        {
            var SlidePartnerDao = new SlidePartnerDao();
            var model = SlidePartnerDao.ListallSlidePartners(SearchString, page, pagesize);
            ViewBag.SearchString = SearchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = new SlidePartnerDao().SlidePartnerDetail(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(SlidePartner slidePartner)
        {
            if(ModelState.IsValid)
            {
                var SlidePartnerDao = new SlidePartnerDao();
                var check = SlidePartnerDao.Check(slidePartner.Name);
                if(check == 1)
                {
                    slidePartner.CreatedDate = DateTime.Now;
                    long id = SlidePartnerDao.Insert(slidePartner);
                    if(id > 0)
                    {
                        return RedirectToAction("Index", "SlidePartner");
                    }    
                }    
                else
                {
                    ModelState.AddModelError("", "Slide này đã tồn tại");
                }    
            }
            return View("Create");
        }
        [HttpPost]
        public ActionResult Edit(SlidePartner slidePartner)
        {
            if(ModelState.IsValid)
            {
                var SlidePartnerDao = new SlidePartnerDao();
                var result = SlidePartnerDao.Update(slidePartner);
                if (result)
                {
                    return RedirectToAction("Index", "SlidePartner");
                }
                else
                {
                    ModelState.AddModelError("", "Chỉnh sửa thất bại");
                }    
            }
            return View("Edit");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var model = new SlidePartnerDao().Delete(id);
            return View("Index");
        }
    }
}