using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LINQ_Lab_2.Models
{
    /// <summary>
    /// The InventoryBusinessLogic class contains the business logic for the Product and Inventory Management System.
    /// </summary>
    public class InventoryBusinessLogic
    {
        // Creating static list of Product objects
        public static List<Product> Products = new List<Product>();

        // Creating static list of Inventory objects
        public static List<Inventory> Inventories = new List<Inventory>();

        /// <summary>
        /// This method takes in the Product and Inventory objects as its parameters. It adds the Product object to the Products
        /// list and the Inventory object to the Inventories list.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="inventory"></param>
        public void AddProductAndInventory(Product product, Inventory inventory)
        {
            Products.Add(product);
            Inventories.Add(inventory);
        }

        /// <summary>
        /// This method takes in the name of the product as a parameter. It checks if the Products list contains any product
        /// with the given product name. If so, then it fetches the product from the Products list as well as the Inventories
        /// list and removes it and hence returns true. Otherwise, it returns false.
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public bool RemoveProductAndInventory(string productName)
        {
            if (Products.Any(x => x.Name.ToLower() == productName.ToLower()))
            {
                var productToRemove = Products.FirstOrDefault(x => x.Name.ToLower() == productName.ToLower());
                Products.Remove(productToRemove);

                var inventoryToRemove = Inventories.FirstOrDefault(x => x.ProductID == productToRemove.ProductID);
                Inventories.Remove(inventoryToRemove);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This method returns a list of products and inventories which is created by joining the 'Products' and 'Inventories' 
        /// lists by their commom attribute 'ProductID'
        /// </summary>
        /// <returns></returns>
        public List<ProductAndInventory> GetProductsAndInventories()
        {
            var productsAndInventories = Products.Join(
                                                        Inventories,
                                                        product => product.ProductID,
                                                        inventory => inventory.ProductID,
                                                        (product, inventory) => new ProductAndInventory
                                                        {
                                                            Product = product,
                                                            Inventory = inventory
                                                        });

            return productsAndInventories.ToList();
        }
    }
}