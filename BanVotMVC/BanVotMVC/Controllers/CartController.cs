using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using BanVotMVC.Common;
using BanVotMVC.Models;
using System.Web.Script.Serialization;
using System.Configuration;
using Common;

namespace BanVotMVC.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {            
            var cart = Session[Common.CommonConstants.CartSession];
            var list = new List<Cart>();
            if(cart != null)
            {
                list = (List<Cart>)cart;
            }    
            return View(list);
        }
        public JsonResult DeleteAll()
        {
            Session[Common.CommonConstants.CartSession] = null;
            return Json(new
            {

                status = true
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<Cart>)Session[Common.CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[Common.CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<Cart>>(cartModel);
            var sessionCart = (List<Cart>)Session[Common.CommonConstants.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[Common.CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddToCart(long Id, int Quantity)
        {
            if (Session[Common.CommonConstants.USER_SESSION] == null)
            {
                var product = new ProductDao().Viewdetail(Id);
                var cart = Session[Common.CommonConstants.CartSession];
                if (cart != null)
                {
                    var list = (List<Cart>)cart;
                    if (list.Exists(x => x.Product.ID == Id))
                    {

                        foreach (var item in list)
                        {
                            if (item.Product.ID == Id)
                            {
                                item.Quantity += Quantity;
                            }
                        }
                    }
                    else
                    {
                        //tạo mới đối tượng cart item
                        var item = new Cart();
                        item.Product = product;
                        item.Quantity = Quantity;
                        list.Add(item);
                    }
                    //Gán vào session
                    Session[Common.CommonConstants.CartSession] = list;
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new Cart();
                    item.Product = product;
                    item.Quantity = Quantity;
                    var list = new List<Cart>();
                    list.Add(item);
                    //Gán vào session
                    Session[Common.CommonConstants.CartSession] = list;
                }
            }
            else
            {
                return Redirect("/dang-nhap");
            }
            return Redirect("/");
        }
        [HttpGet]
        public ActionResult Payment()
        {           
            var cart = Session[Common.CommonConstants.CartSession];
            var list = new List<Cart>();
            if (cart != null)
            {
                list = (List<Cart>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string ShipName, string ShipMobile, string ShipAddress, string ShipEmail)
        {
            var Order = new Order();
            Order.CreatedDate = DateTime.Now;
            Order.ShipName = ShipName;
            Order.ShipMobile = ShipMobile;
            Order.ShipAddress = ShipAddress;
            Order.ShipEmail = ShipEmail;
            var id = new OrderDao().Insert(Order);
            var cart = (List<Cart>)Session[Common.CommonConstants.CartSession];
            var OrderDetailDao = new OrderDetailDao();
            decimal Total = 0;
            string ProductName = "";
            try
            {
                foreach (var item in cart)
                {
                    var OrderDetail = new OrderDetail();
                    OrderDetail.ProductID = item.Product.ID;
                    OrderDetail.Price = item.Product.Price;
                    OrderDetail.OrderID = id;
                    OrderDetail.Quantity = item.Quantity;
                    OrderDetailDao.Insert(OrderDetail);
                    Session[Common.CommonConstants.CartSession] = null;
                    ProductName += (item.Product.Name);
                    Total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/Template/NewOrder.html"));

                content = content.Replace("{{Customer}}", ShipName);
                content = content.Replace("{{Phone}}", ShipMobile);
                content = content.Replace("{{Product}}", ProductName);
                content = content.Replace("{{Email}}", ShipEmail);
                content = content.Replace("{{Address}}", ShipAddress);
                content = content.Replace("{{Total}}", Total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(ShipEmail, "Đơn hàng mới từ OnlineShop", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
            }
            catch (Exception)
            {
                throw;
            }
            return Redirect("/thanh-cong");
        }
        public ActionResult Success()
        {            
            return View();
        }
    }
}
