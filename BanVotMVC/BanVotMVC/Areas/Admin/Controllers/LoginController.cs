using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVotMVC.Areas.Admin.Models;
using BanVotMVC.Common;
using Model.DAO;

namespace BanVotMVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, MaHoa.MD5Hash(model.PassWord),true);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.GroupID = user.GroupID;
                    var listCredential = dao.GetListCredential(model.UserName);
                    Session.Add(CommonConstants.CREDENTIAL_SESSION, listCredential);
                    Session.Add(CommonConstants.USER_SESSION,userSession );
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }   
                else if(result == -3)
                {
                    ModelState.AddModelError("", "Bạn không có quyền truy cập vào trang này");
                }    
            }
            return View("Index");
            }
    }
}