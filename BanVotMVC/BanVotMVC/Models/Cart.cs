using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;

namespace BanVotMVC.Models
{
    [Serializable]
    public class Cart
    {
        public Product Product { set; get; }
        public int Quantity { set; get; }
    }
}