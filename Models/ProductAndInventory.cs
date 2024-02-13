using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINQ_Lab_2.Models
{
    /// <summary>
    /// This is a class which contains the Product and Inventory objects as its properties.
    /// </summary>
    public class ProductAndInventory
    {
        public Product Product { get; set; }
        public Inventory Inventory { get; set; }
    }
}