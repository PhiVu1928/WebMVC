using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
using BanVotMVC.Common;
using BanVotMVC.Models;

namespace BanVotMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Register(RegisterUser Model)
        {
            if (ModelState.IsValid)
            {
                var UserDao = new UserDao();
                if (UserDao.CheckUserName(Model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (UserDao.CheckEmail(Model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var User = new User();
                    User.UserName = Model.UserName;
                    User.Password = MaHoa.MD5Hash(Model.Password);
                    User.Name = Model.Name;
                    User.Phone = Model.Phone;
                    User.Email = Model.Email;
                    User.CreatedDate = DateTime.Now;
                    User.Status = true;
                    User.Address = Model.Address;
                    var result = UserDao.Insert(User);
                    if (result > 0)
                    {
                        ModelState.AddModelError("", "Đăng ký thành công !!!");
                        Model = new RegisterUser();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công");
                    }
                }
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult Login(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                var UserDao = new UserDao();
                var result = UserDao.Login(loginUser.Username, MaHoa.MD5Hash(loginUser.Password));
                if (result == 1)
                {
                    var user = UserDao.GetById(loginUser.Username);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
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
            }
            return View(loginUser);
        }
    }
}