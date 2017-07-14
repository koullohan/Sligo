using Sligo.Areas.Area.Models;
using Sligo.Areas.Area.Models.ViewModel;
using Sligo.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sligo.Areas.Area.Controllers
{
    public class ProductController : Controller
    {

        private static bool success = false;
        // Display Index
        public async Task<ActionResult> Index()
        {
            ProductViewModel viewmodel = new ProductViewModel();
            viewmodel.ProductList = await ProductBusiness.GetProducts();
            return View(viewmodel);
        }


        // Load Products
        public async Task<ActionResult> LoadProducts()
        {
            ProductViewModel viewmodel = new ProductViewModel();
            viewmodel.ProductList = await ProductBusiness.GetProducts();
            return PartialView("Products",viewmodel);
        }


        // Redirect to Add Product Form
        public async Task<ActionResult> AddProduct()
        {
            ViewBag.Categories = await CategoryBusiness.GetCategoriesSelectList();
            ViewBag.Manufacturers = await ManufacturerBusiness.GetManufacturersSelectList();
            return View("AddProduct", new Product());
        }


        // Redirect to Edit Product Form
        public async Task<ActionResult> EditProduct(int Id)
        {
            ViewBag.Categories = await CategoryBusiness.GetCategoriesSelectList();
            ViewBag.Manufacturers = await ManufacturerBusiness.GetManufacturersSelectList();
            var product = await ProductBusiness.GetProductByProductId(Id);
            return View("AddProduct", product);
        }


        // Delete Product
        public async Task<ActionResult> DeleteProduct(int Id)
        {
            var result = await ProductBusiness.DeleteProduct(Id);
            return View("Delete", result);
        }


        // Create Product
        public async Task<ActionResult> CreateProduct(Product product)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("ModifiedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("ModifiedDate");

            var viewmodel = new ProductViewModel();
            if (ModelState.IsValid)
            {
                

                if (product.Id <= 0)
                {
                    viewmodel = await ProductBusiness.AddProduct(product);
                    if (viewmodel.Id != 0)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                    return Json(new { success = success, data = viewmodel , IsEdit = false, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    viewmodel = await ProductBusiness.EditProduct(product);
                    if (viewmodel.Id != 0)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                    return Json(new { success = success, data = viewmodel , IsEdit = true, JsonRequestBehavior.AllowGet });
                }

            }

            return RedirectToAction("AddProduct");
        }

    }
}