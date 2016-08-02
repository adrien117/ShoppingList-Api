using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopListAPI.Dtos
{
    public class ShopItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public bool CheckedOut { get; set; } = false;
    }
}