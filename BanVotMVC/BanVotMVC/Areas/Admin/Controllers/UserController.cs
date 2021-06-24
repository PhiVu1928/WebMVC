using Model.EF;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVotMVC.Common;
using PagedList.Mvc;
using PagedList;
using BanVotMVC;

namespace BanVotMVC.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        [HasCredential(RoleID = "VIEW_USER")]
        // GET: Admin/User
        public ActionResult Index(string SearchString, int page = 1, int pagesize = 10)
        {
            var dao = new UserDao();
            var Model = dao.ListallPaging(SearchString, page, pagesize);
            ViewBag.SearchString = SearchString;
            return View(Model);
        }
        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int Id)
        {
            setviewbag();
            var dao = new UserDao().ViewDetail(Id);
            return View(dao);
        }
        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var chk = dao.check(user.UserName);
                if(chk == 0)
                {
                    //mã hóa mật khẩu   
                    var MD5 = MaHoa.MD5Hash(user.Password);
                    user.Password = MD5;
                    //check trùng tài khoản
                    long id = dao.Insert(user);
                    if (id > 0)
                    {
                        SetAlert("Thêm user thành công", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm thành công");
                    }
                }
                else
                {
                     ModelState.AddModelError("", "Tên tài khoản đã được sử dụng");
                }
            }
            return View("Create");
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if(!string.IsNullOrEmpty(user.Password))
                {
                    //mã hóa mật khẩu   
                    var MD5 = MaHoa.MD5Hash(user.Password);
                    user.Password = MD5;
                }
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Sửa user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);

            return View("Index");
        } 
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChageStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public void setviewbag(long? selectedID = null)
        {
            var dao = new UserDao();
            ViewBag.GroupID = new SelectList(dao.ListAllGroup(), "ID", "Name", selectedID);
        }
    }
}