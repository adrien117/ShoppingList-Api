using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopListAPI.Dtos
{
    public class ShopItemDto
    {
        public string ItemName { get; set; }
        public int ItemQuantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public bool CheckedOut { get; set; }
    }
}