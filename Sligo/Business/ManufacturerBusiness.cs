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
    public class ManufacturerBusiness
    {
        private static ApplicationDbContext identityASPdb = new ApplicationDbContext();

        public static async Task<ManufacturerViewModel> AddManufacturer(Manufacturer model)
        {

            ManufacturerViewModel viewmodel = new ManufacturerViewModel();
            if (model.Id == 0)
            {
                try
                {
                    Manufacturer manufacturer = new Manufacturer();
                    //ManufacturerId is set to autoincrement in Manufacturer table.
                    manufacturer.Name = model.Name;
                    manufacturer.Location = model.Location;
                    manufacturer.Manager = model.Manager;
                    manufacturer.Telephone = model.Telephone;
                    manufacturer.Fax = model.Fax;
                    manufacturer.Email = model.Email;
                    manufacturer.Mobile = model.Mobile;
                    manufacturer.CreatedBy = "ironic";
                    manufacturer.CreatedDate = DateTime.Now;
                    manufacturer.ModifiedBy = "ironic";
                    manufacturer.ModifiedDate = DateTime.Now;
                    identityASPdb.Manufacturer.Add(manufacturer);
                    await identityASPdb.SaveChangesAsync();

                    viewmodel.ManufacturerId = manufacturer.Id;
                    viewmodel.ManufacturerName = manufacturer.Name;
                    viewmodel.ManufacturerLocation = manufacturer.Location;
                    viewmodel.ManufacturerEmail = manufacturer.Email;
                    viewmodel.ManufacturerFax = manufacturer.Fax;
                    viewmodel.ManufacturerMobile = manufacturer.Mobile;
                    viewmodel.ManufacturerTelephone = manufacturer.Telephone;
                    viewmodel.ManufacturerManager = manufacturer.Manager;

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return viewmodel;
        }


        public static async Task<Manufacturer> DeleteManufacturer(int manufacturerId)
        {
            Manufacturer queryResult = new Manufacturer();
            try
            {
                if (manufacturerId != 0)
                {
                    queryResult = identityASPdb.Manufacturer.Where(x => x.Id == manufacturerId).FirstOrDefault();
                    if (queryResult != null)
                    {
                        queryResult.IsDelete = true;
                        identityASPdb.Entry(queryResult).State = EntityState.Modified;
                        await identityASPdb.SaveChangesAsync();
                     
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return queryResult;
        }


        public static async Task<ManufacturerViewModel> EditManufacturer(Manufacturer manufacturer)
        {
            ManufacturerViewModel viewmodel = new ManufacturerViewModel();
            if (manufacturer.Id > 0)
            {
                try
                {
                    var queryResult = await identityASPdb.Manufacturer.Where(x => x.Id == manufacturer.Id).FirstOrDefaultAsync();

                    if (queryResult.Id == manufacturer.Id)
                    {
                        queryResult.Name = manufacturer.Name;
                        queryResult.Location = manufacturer.Location;
                        queryResult.Email = manufacturer.Email;
                        queryResult.Mobile = manufacturer.Mobile;
                        queryResult.Telephone = manufacturer.Telephone;
                        queryResult.Fax = manufacturer.Fax;
                        queryResult.Manager = manufacturer.Manager;
                        queryResult.ModifiedDate = DateTime.Now;
                        queryResult.ModifiedBy = "ironic";
                        identityASPdb.Entry(queryResult).State = EntityState.Modified;
                        await identityASPdb.SaveChangesAsync();

                        viewmodel.ManufacturerId = manufacturer.Id;
                        viewmodel.ManufacturerName = manufacturer.Name;
                        viewmodel.ManufacturerLocation = manufacturer.Location;
                        viewmodel.ManufacturerEmail = manufacturer.Email;
                        viewmodel.ManufacturerMobile = manufacturer.Mobile;
                        viewmodel.ManufacturerTelephone = manufacturer.Telephone;
                        viewmodel.ManufacturerFax = manufacturer.Fax;
                        viewmodel.ManufacturerManager = manufacturer.Manager;

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return viewmodel;
        }


        public static async Task<Manufacturer> GetManufacturerByManufacturerId(int manufacturerId)
        {

            try
            {
                return await identityASPdb.Manufacturer.Where(x => x.IsDelete != true && x.Id == manufacturerId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static async Task<string> GetManufacturerNameByManufacturerId(int id)
        {
            try
            {
                var manufacturer = await identityASPdb.Manufacturer.Where(x => x.IsDelete != true && x.Id == id).FirstOrDefaultAsync();
                return manufacturer.Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static async Task<List<SelectListItem>> GetManufacturersSelectList()
        {
            try
            {
                List<Manufacturer> manufacturers = await identityASPdb.Manufacturer.Where(x => x.IsDelete != true).ToListAsync();
                var manufacturersSelectList = new List<SelectListItem>();
                foreach (var model in manufacturers)
                {
                    var item = new SelectListItem();
                    item.Value = model.Id.ToString();
                    item.Text = model.Name;
                    manufacturersSelectList.Add(item);
                }

                return manufacturersSelectList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<ManufacturerViewModel>> GetManufacturers()
        {
            try
            {
                List<ManufacturerViewModel> manufacturers = await identityASPdb.Manufacturer
                    .Where(x => x.IsDelete != true)
                    .Select(x => new ManufacturerViewModel
                    {
                        ManufacturerId = x.Id,
                        ManufacturerName = x.Name,
                        ManufacturerLocation = x.Location,
                        ManufacturerManager = x.Manager,
                        ManufacturerTelephone = x.Telephone,
                        ManufacturerFax = x.Fax,
                        ManufacturerEmail = x.Email,
                        ManufacturerMobile = x.Mobile
                    }).ToListAsync();

                return manufacturers;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}