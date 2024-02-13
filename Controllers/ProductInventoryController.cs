using LINQ_Lab_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LINQ_Lab_2.Controllers
{
    public class ProductInventoryController : Controller
    {
        // Creating auto-generated IDs for the Products and Inventories
        private static int productIDCounter = 1;
        private static int inventoryIDCounter = 101;

        // Creating an object of InventoryBusinessLogic class
        InventoryBusinessLogic inventoryBusinessLogic = new InventoryBusinessLogic();

        // GET: ProductList
        /// <summary>
        /// This action method gets the "joined" list of Products and Inventories and passes it to the corresponding view
        /// to render on the page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var productsAndInventories = inventoryBusinessLogic.GetProductsAndInventories();

            return View(productsAndInventories);
        }

        /// <summary>
        /// This action method returns the view page where the user can add new products.
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProduct()
        {
            return View();
        }

        /// <summary>
        /// This action method contains the functionality to add a new product. It receives the inputs from the user,
        /// validates them and displays the appropriate error messages. If the addition is successful, then it redirects the 
        /// user to the index page, where a list of products in the inventory is displayed.
        /// </summary>
        /// <param name="ProductName"></param>
        /// <param name="ProductDescription"></param>
        /// <param name="ProductPrice"></param>
        /// <param name="StockQuantity"></param>
        /// <returns></returns>
        public ActionResult AddProductFunctionality(string ProductName, string ProductDescription, decimal? ProductPrice,
                                                    int? StockQuantity)
        {
            // Creating an instance of Product and Inventory
            Product newProduct = new Product();
            Inventory newInventory = new Inventory();

            // Checking if the product already exists in the Products list.
            // Since, Products list is a static type, so accessing it directly through the class.
            if (InventoryBusinessLogic.Products.Any(x => x.Name.ToLower() == ProductName.ToLower()))
            {
                TempData["Message"] = $"The product {ProductName} already exists in the inventory. Please enter a different product.";
                return RedirectToAction("AddProduct", "ProductInventory");
            }

            // Checking if the ProductName is empty or null
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                TempData["Message"] = "Please enter a valid product name.";
                return RedirectToAction("AddProduct", "ProductInventory");
            }

            // Checking if the price is null or 0 or negative
            if (ProductPrice == null || ProductPrice <= 0)
            {
                TempData["Message"] = "Please enter a valid positive value for the price.";
                return RedirectToAction("AddProduct", "ProductInventory");
            }

            // Checking if the stock quantity is null or 0 or negative
            if (StockQuantity == null || StockQuantity <= 0)
            {
                TempData["Message"] = "Please enter a valid positive value for the quantity.";
                return RedirectToAction("AddProduct", "ProductInventory");
            }

            // After validations, assigning the values of user input to the properties in the Product and Inventory instances.
            // And also updating the counter for auto-generated IDs for product and inventory.
            newProduct.ProductID = productIDCounter++;
            newProduct.Name = ProductName;
            newProduct.Description = ProductDescription;
            newProduct.Price = ProductPrice;
            newInventory.ProductID = newProduct.ProductID;
            newInventory.InventoryID = inventoryIDCounter++;
            newInventory.StockQuantity = StockQuantity;

            // Adding the new product and inventory to the Products and Inventories lists
            inventoryBusinessLogic.AddProductAndInventory(newProduct, newInventory);

            // Redirecting the user to the index page to display a list of all the products
            return RedirectToAction("Index", "ProductInventory");
        }

        /// <summary>
        /// This action method returns the view page where the user can delete a product.
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteProduct()
        {
            return View();
        }

        /// <summary>
        /// This action method contains the functionality to delete the products from the inventory.
        /// If the deletion is succesful, it redirects the user to the index page, where a list of products in the inventory
        /// is displayed. If the deletion is not successful, then it displays an error message to the user and redirects
        /// the user to the DeleteProduct page, so that the user can try again.
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        public ActionResult DeleteProductFunctionality(string ProductName)
        {
            if (inventoryBusinessLogic.RemoveProductAndInventory(ProductName))
            {
                //TempData["Message"] = $"The product {ProductName} has been removed from the inventory.";

                return RedirectToAction("Index", "ProductInventory");
            }
            else
            {
                // Using TempData to persist the data after the redirect
                TempData["Message"] = $"The product {ProductName} doesnot exist in the inventory. Please enter a valid product.";

                return RedirectToAction("DeleteProduct", "ProductInventory");
            }

        }
    }
}