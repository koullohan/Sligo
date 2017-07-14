using Sligo.Areas.Area.Models;
using Sligo.Areas.Area.Models.ViewModel;
using Sligo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sligo.Business
{
    public class CategoryBusiness
    {
        private static ApplicationDbContext identityASPdb = new ApplicationDbContext();

        public static async Task<CategoryViewModel> AddCategory(Category model)
        {
                       
            CategoryViewModel viewmodel = new CategoryViewModel();
            if (model.Id == 0)
            {
                try
                {
                    Category category = new Category();
                    //CategoryId is set to autoincrement in Category table.
                    category.Name = model.Name;
                    category.CreatedDate = DateTime.Now;
                    category.CreatedBy = "john";
                    category.ModifiedDate = DateTime.Now;
                    category.ModifiedBy = "john";
                    category.IsDelete = false;                  
                    identityASPdb.Category.Add(category);
                    await identityASPdb.SaveChangesAsync();

                    viewmodel.CategoryId = category.Id;
                    viewmodel.CategoryName = category.Name;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
            return viewmodel;
        }



        public static async Task<Category> DeleteCategory(int categoryId)                                    //  Delete
        {
            Category queryResult = new Category();
            if (categoryId > 0)
            {
                try
                {
                    queryResult = await identityASPdb.Category.Where(x => x.Id == categoryId && x.IsDelete != true).FirstOrDefaultAsync();

                    if (queryResult != null)
                    {
                        queryResult.IsDelete = true;                                            //  set flag to true(deleted)
                        identityASPdb.Entry(queryResult).State = EntityState.Modified;
                        await identityASPdb.SaveChangesAsync();

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return queryResult;
        }



        public static async Task<CategoryViewModel> EditCategory(Category category)
        {
            CategoryViewModel viewmodel = new CategoryViewModel();
            if (category.Id > 0)
            {
                try
                {
                    var queryResult = await identityASPdb.Category.Where(x => x.Id == category.Id).FirstOrDefaultAsync();

                    if (queryResult.Id == category.Id)
                    {
                        queryResult.Name = category.Name;
                        queryResult.ModifiedDate = DateTime.Now;
                        identityASPdb.Entry(queryResult).State = EntityState.Modified;
                        await identityASPdb.SaveChangesAsync();

                        viewmodel.CategoryId = queryResult.Id;
                        viewmodel.CategoryName = queryResult.Name;

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return viewmodel;
        }



        public static async Task<List<SelectListItem>> GetCategoriesSelectList()
        {

            try
            {
                return await identityASPdb.Category.Where(x => x.IsDelete != true).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public static async Task<List<CategoryViewModel>> GetCategories()
        {

            try
            {
                return await identityASPdb.Category
                    .Where(x => x.IsDelete != true)                 
                    .Select(x => new CategoryViewModel
                    {
                    CategoryId = x.Id,
                    CategoryName = x.Name               
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public static async Task<Category> GetCategoryByCategoryId(int categoryId)
        {

            try
            {
                return await identityASPdb.Category.Where(x => x.IsDelete != true && x.Id == categoryId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public static async Task<string> GetCategoryNameByCategoryId(int categoryId)
        {

            try
            {

                var categoryName = await (from category in identityASPdb.Category
                                    where (category.Id == categoryId && category.IsDelete != true)
                                    select category.Name).SingleAsync();
                return categoryName;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}