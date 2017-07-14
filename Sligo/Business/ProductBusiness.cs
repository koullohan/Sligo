using Sligo.Areas.Area.Models;
using Sligo.Areas.Area.Models.ViewModel;
using Sligo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Sligo.Business
{
    public class ProductBusiness
    {

        private static ApplicationDbContext identityASPdb = new ApplicationDbContext();

        public static async Task<ProductViewModel> AddProduct(Product model)
        {
            
            ProductViewModel viewmodel = new ProductViewModel();
            if (model.Id == 0)
            {
                try
                {
                    Product product = new Product();
                    //ProductId is set to autoincrement in Product table.       
                    product.ManufacturerId = model.ManufacturerId;
                    product.CategoryId = model.CategoryId;
                    product.Name = model.Name;
                    product.Description = model.Description;
                    product.Model = model.Model;
                    product.ReleasedDate = model.ReleasedDate;
                    product.ReleasedYear = model.ReleasedYear;
                    product.CreatedDate = DateTime.Now;
                    product.ModifiedDate = DateTime.Now;
                    product.CreatedBy = "ironic";
                    product.ModifiedBy = "ironic";
                    identityASPdb.Product.Add(product);
                    await identityASPdb.SaveChangesAsync();
                    

                    viewmodel = await identityASPdb.Product
                                        .Join(identityASPdb.Category, p => p.CategoryId, pc => pc.Id, (p, pc) => new { p, pc })
                                        .Join(identityASPdb.Manufacturer, ppc => ppc.p.ManufacturerId, c => c.Id, (ppc, c) => new { ppc, c })
                                        .Where(x => x.ppc.p.IsDelete != true && x.ppc.p.Id == product.Id)
                                        .OrderByDescending(x => x.ppc.p.CreatedDate)
                                        .Select(m => new ProductViewModel
                                        {
                                            Id = m.ppc.p.Id,
                                            CategoryId = m.ppc.pc.Id,
                                            CategoryName = m.ppc.pc.Name,
                                            ManufacturerId = m.ppc.p.ManufacturerId,
                                            ManufacturerName = m.c.Name,
                                            Name = m.ppc.p.Name,
                                            Description = m.ppc.p.Description,
                                            Model = m.ppc.p.Model,
                                            ReleasedDate = m.ppc.p.ReleasedDate,
                                            ReleasedYear = m.ppc.p.ReleasedYear
                                        }).FirstOrDefaultAsync();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

           
            return viewmodel;
        }


        public static async Task<Product> DeleteProduct(int productId)
        {
            Product product = new Product();
            if (productId > 0)
            {
                try
                {
                    product = await identityASPdb.Product.Where(x => x.Id == productId).FirstOrDefaultAsync();
                    if (product.Id == productId)
                    {
                        product.IsDelete = true;
                        identityASPdb.Entry(product).State = EntityState.Modified;
                        await identityASPdb.SaveChangesAsync();                       

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
            return product;
        }


        public static async Task<ProductViewModel> EditProduct(Product product)
        {
            ProductViewModel viewmodel = new ProductViewModel();

            if (product.Id > 0)
            {
                try
                {
                    var queryResult = await identityASPdb.Product.Where(x => x.Id == product.Id).FirstOrDefaultAsync();
                    
                    if (queryResult.Id == product.Id)
                    {                       
                        queryResult.ManufacturerId = product.ManufacturerId;
                        queryResult.CategoryId = product.CategoryId;
                        queryResult.Name = product.Name;
                        queryResult.Description = product.Description;
                        queryResult.Model = product.Model;
                        queryResult.ReleasedDate = product.ReleasedDate;
                        queryResult.ReleasedYear = product.ReleasedYear;
                        identityASPdb.Entry(queryResult).State = EntityState.Modified;
                        await identityASPdb.SaveChangesAsync();


                        viewmodel = await identityASPdb.Product
                                        .Join(identityASPdb.Category, p => p.CategoryId, pc => pc.Id, (p, pc) => new { p, pc })
                                        .Join(identityASPdb.Manufacturer, ppc => ppc.p.ManufacturerId, c => c.Id, (ppc, c) => new { ppc, c })
                                        .Where(x => x.ppc.p.IsDelete != true && x.ppc.p.Id == product.Id)
                                        .OrderByDescending(x => x.ppc.p.CreatedDate)
                                        .Select(m => new ProductViewModel
                                        {
                                            Id = m.ppc.p.Id,
                                            CategoryId = m.ppc.pc.Id,
                                            CategoryName = m.ppc.pc.Name,
                                            ManufacturerId = m.ppc.p.ManufacturerId,
                                            ManufacturerName = m.c.Name,
                                            Name = m.ppc.p.Name,
                                            Description = m.ppc.p.Description,
                                            Model = m.ppc.p.Model,
                                            ReleasedDate = m.ppc.p.ReleasedDate,
                                            ReleasedYear = m.ppc.p.ReleasedYear
                                        }).FirstOrDefaultAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return viewmodel;
        }


        public static async Task<List<ProductViewModel>> GetProducts()
        {
            List<ProductViewModel> viewmodel = new List<ProductViewModel>();
            try
            {
                viewmodel = await identityASPdb.Product
                .Join(identityASPdb.Category, p => p.CategoryId, pc => pc.Id, (p, pc) => new { p, pc })
                .Join(identityASPdb.Manufacturer, ppc => ppc.p.ManufacturerId, c => c.Id, (ppc, c) => new { ppc, c })
                .Where(x => x.ppc.p.IsDelete != true)
                .OrderByDescending(x => x.ppc.p.CreatedDate)
                .Select(m => new ProductViewModel
                {
                    Id = m.ppc.p.Id,
                    CategoryId = m.ppc.pc.Id,
                    CategoryName = m.ppc.pc.Name,
                    ManufacturerId = m.ppc.p.ManufacturerId,
                    ManufacturerName = m.c.Name,
                    Name = m.ppc.p.Name,
                    Description = m.ppc.p.Description,
                    Model = m.ppc.p.Model,
                    ReleasedDate = m.ppc.p.ReleasedDate,
                    ReleasedYear = m.ppc.p.ReleasedYear
                }).ToListAsync();

                return viewmodel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static async Task<Product> GetProductByProductId(int productId)
        {
            try
            {
                var product = await identityASPdb.Product.Where(x => x.Id == productId).FirstOrDefaultAsync();    
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}