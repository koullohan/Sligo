using Sligo.Areas.Area.Models;
using Sligo.Areas.Area.Models.ViewModel;
using Sligo.Business;
using Sligo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sligo.Areas.Area.Controllers
{
    public class CategoryController : Controller
    {
        private static bool success = false;
        // Display Index
        public async Task<ActionResult> Index()
        {
            CategoryViewModel viewmodel = new CategoryViewModel();
            viewmodel.CategoryList = await CategoryBusiness.GetCategories();
            return View(viewmodel);
        }


        // Load Categories
        public async Task<ActionResult> LoadCategories()
        {
            CategoryViewModel viewmodel = new CategoryViewModel();
            viewmodel.CategoryList = await CategoryBusiness.GetCategories();
            return PartialView("Categories", viewmodel);
        }


        // Redirect to Add Category Form
        public ActionResult AddCategory()
        {          
            return View("AddCategory", new Category());
        }


        // Redirect to Edit Category Form
        public async Task<ActionResult> EditCategory(int Id)
        {

            var category = await CategoryBusiness.GetCategoryByCategoryId(Id);
            return View("AddCategory", category);
        }


        // Delete Category
        public async Task<ActionResult> DeleteCategory(int Id)
        {

            var result = await CategoryBusiness.DeleteCategory(Id);     
            return View("Delete", result);
        }


        // Create Category
        public async Task<ActionResult> CreateCategory(Category category)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("ModifiedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("ModifiedDate");

            var viewmodel = new CategoryViewModel();
            if (ModelState.IsValid)
            {


                if (category.Id <= 0)
                {
                    viewmodel = await CategoryBusiness.AddCategory(category);
                    if(viewmodel.CategoryId != 0)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                    return Json(new { success = success, data = viewmodel, IsEdit = false, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    viewmodel = await CategoryBusiness.EditCategory(category);
                    if (viewmodel.CategoryId != 0)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                    return Json(new { success = success, data = viewmodel, IsEdit = true, JsonRequestBehavior.AllowGet });
                }

            }

            return RedirectToAction("AddCategory");
        }


    }
}