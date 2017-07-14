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
    public class ManufacturerController : Controller
    {

        private static bool success = false;
        // Display Index
        public async Task<ActionResult> Index()
        {
            ManufacturerViewModel viewmodel = new ManufacturerViewModel();
            viewmodel.ManufacturerList = await ManufacturerBusiness.GetManufacturers();
            return View(viewmodel);
        }


        // Load Manufacturers
        public async Task<ActionResult> LoadManufacturers()
        {
            ManufacturerViewModel viewmodel = new ManufacturerViewModel();
            viewmodel.ManufacturerList = await ManufacturerBusiness.GetManufacturers();
            return PartialView("Manufacturers", viewmodel);
        }


        // Redirect to Add Manufacturer Form
        public ActionResult AddManufacturer()
        {
            return View("AddManufacturer", new Manufacturer());
        }


        // Redirect to Edit Manufacturer Form
        public async Task<ActionResult> EditManufacturer(int Id)
        {

            var manufacturer = await ManufacturerBusiness.GetManufacturerByManufacturerId(Id);
            return View("AddManufacturer", manufacturer);
        }


        // Delete Manufacturer
        public async Task<ActionResult> DeleteManufacturer(int Id)
        {

            var result = await ManufacturerBusiness.DeleteManufacturer(Id);
            return View("Delete", result);
        }


        // Create Manufacturer
        public async Task<ActionResult> CreateManufacturer(Manufacturer manufacturer)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("ModifiedBy");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("ModifiedDate");

            var viewmodel = new ManufacturerViewModel();
            if (ModelState.IsValid)
            {


                if (manufacturer.Id <= 0)
                {
                    viewmodel = await ManufacturerBusiness.AddManufacturer(manufacturer);
                    if (viewmodel.ManufacturerId != 0)
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
                    viewmodel = await ManufacturerBusiness.EditManufacturer(manufacturer);
                    if (viewmodel.ManufacturerId != 0)
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

            return RedirectToAction("AddManufacturer", ModelState);
        }

    }
}