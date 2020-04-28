using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Models;

namespace eCart.Areas.Shopper.Models
{
    public class cCart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }

    public class CartClass 
    {
       
    }
}