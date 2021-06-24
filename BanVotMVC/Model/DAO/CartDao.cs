using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.ViewModel;
using PagedList;

namespace Model.DAO
{
    public class CartDao
    {
        OnlineShop dt = null;
        public CartDao()
        {
            dt = new OnlineShop();
        }
        public IEnumerable<Order> ListAllCustomer(string SearchString, int page, int pagesize)
        {
            IQueryable<Order> model = dt.Orders;
            if (!string.IsNullOrEmpty(SearchString))
            {
                model = model.Where(x => x.ShipName.Contains(SearchString) || x.ShipName.Contains(SearchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
        public List<CartViewModel> CartDetail(int id)
        {
            var model = from a in dt.OrderDetails
                        join b in dt.Products
                        on a.ProductID equals b.ID
                        where a.ProductID == b.ID
                        where a.OrderID == id
                        select new CartViewModel()
                        {
                            Images = b.Image,
                            Name = b.Name,
                            Price = a.Price,
                            Quantity = a.Quantity,
                        };
            return model.ToList();
        }
        public bool Delete(long id)
        {
            try
            {
                var ID = dt.Orders.Find(id);
                dt.Orders.Remove(ID);
                dt.OrderDetails.RemoveRange(dt.OrderDetails.Where(x => x.OrderID == id));
                dt.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
    }
}