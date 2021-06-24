using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
namespace Model.DAO
{
    public class OrderDetailDao
    {
        OnlineShop dt = null;
        public OrderDetailDao()
        {
            dt = new OnlineShop();
        }
        public bool Insert(OrderDetail orderDetail)
        {
            try
            {
                dt.OrderDetails.Add(orderDetail);
                dt.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
