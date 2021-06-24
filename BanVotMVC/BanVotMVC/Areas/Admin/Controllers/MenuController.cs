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
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string SearchString, int page = 1 , int pagesize = 10)
        {
            var Menudao = new MenuDao();
            var menu = Menudao.ListallMenu(SearchString, page, pagesize);
            ViewBag.SearchString = SearchString;
            return View(menu);
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
            var menu = new MenuDao().MenuDetail(id);
            return View(menu);
        }
        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            if(ModelState.IsValid)
            {
                var Menudao = new MenuDao();
                var check = Menudao.CheckMenu(menu.Text);
                if (check == 1)
                {
                    long id = Menudao.Insert(menu);
                    if(id > 0)
                    {
                        return RedirectToAction("Index", "Menu");
                    }    
                }
            }   else
            {
                ModelState.AddModelError("", "Menu này đã tồn tại !!!");
            }
            return View("Create");
        }
        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if(ModelState.IsValid)
            {
                var Menudao = new MenuDao();
                var result = Menudao.UpdateMenu(menu);
                if (result)
                {
                    return RedirectToAction("Index", "Menu");
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
            new MenuDao().DeleteMenu(id);
            return View("Index");
        }
        public void setviewbag(long? selectedID = null)
        {
            var dao = new MenuDao();
            ViewBag.TypeID = new SelectList(dao.listAllMenutype(), "ID", "Name", selectedID);
        }
    }
}