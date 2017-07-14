using Microsoft.AspNet.Identity;
using Sligo.Areas.Area.Models.ViewModel;
using Sligo.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sligo.Areas.Area.Controllers
{
    public class DocumentController : Controller
    {
        private static bool success = false;

        [HttpGet]
        public ActionResult Index()
        {

            DocumentViewModel viewmodel = new DocumentViewModel();
            viewmodel.DocumentPath = Server.MapPath(viewmodel.DocumentLocation + User.Identity.GetUserId());
            viewmodel.DocumentList = DocumentBusiness.GetDocuments(viewmodel);
            return View(viewmodel);

        }


        public ActionResult LoadDocuments()
        {
            DocumentViewModel viewmodel = new DocumentViewModel();
            viewmodel.DocumentPath = Server.MapPath(viewmodel.DocumentLocation + User.Identity.GetUserId());
            viewmodel.DocumentList = DocumentBusiness.GetDocuments(viewmodel);
            return PartialView("~/Areas/Area/Views/Document/Documents.cshtml", viewmodel);

        }

        public JsonResult UploadDocument()
        {
            string message = "";
            if (ModelState.IsValid)
            {

                DocumentViewModel viewmodel = new DocumentViewModel();
                viewmodel.DocumentPath = Server.MapPath(viewmodel.DocumentLocation + User.Identity.GetUserId());
                viewmodel.DocumentFiles = Request.Files;
                success = DocumentBusiness.UploadDocument(viewmodel);

                if (success)
                {
                    message = "File uploaded successfully";
                }
                else
                {
                    message = "File has not uploaded successfully";
                }
            }
            return Json(new { success = success, message = message }, JsonRequestBehavior.AllowGet);
            //return Json(new { success = result,message = message  ,Url = Url.Action("_Document", LoadDocuments())}, JsonRequestBehavior.AllowGet);

        }


        public FileResult DownloadDocument(string documentName)
        {

            DocumentViewModel viewmodel = new DocumentViewModel();
            viewmodel.DocumentPath = Server.MapPath(viewmodel.DocumentLocation + User.Identity.GetUserId() + Resources.Document.Slash + documentName);
            return File(DocumentBusiness.DownloadDocument(viewmodel), DocumentBusiness.GetTypeOctet(), DocumentBusiness.GetFileName(viewmodel));

        }


    }
}