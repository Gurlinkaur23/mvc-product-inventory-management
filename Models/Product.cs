using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINQ_Lab_2.Models
{
    /// <summary>
    /// This a class for products containing information like product ID, product name, description of the product and the price.
    /// </summary>
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price {  get; set; }
    }
}