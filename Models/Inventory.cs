using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINQ_Lab_2.Models
{
    /// <summary>
    /// This is a class for Inventory containing the information such as the inventory ID, product ID and the stock quantity.
    /// </summary>
    public class Inventory
    {
        public int InventoryID { get; set; }
        public int ProductID {  get; set; }
        public int? StockQuantity {  get; set; }
    }
}