using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class OrderDao
    {
        OnlineShop dt = null;
        public OrderDao()
        {
            dt = new OnlineShop();
        }
        public long Insert(Order order)
        {
            dt.Orders.Add(order);
            dt.SaveChanges();
            return order.ID;
        }
    }
}
