using Sligo.Areas.Area.Models.ViewModel;
using Sligo.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sligo.Areas.Area.Controllers
{
    public class MailController : Controller
    {
        private static bool success = false;
        // Display Index
        public ActionResult Index()
        {
            return View();
        }

    
        public async Task<ActionResult> SendMail(MailViewModel mail , List<string>To)
        {

            if (ModelState.IsValid)
            {

                success = await MailBusiness.SendMail(mail, To);
               
                if (success)
                {
                    ViewBag.Message = "File sent successfully";
                }
                else
                {
                    ViewBag.Message = "File not sent successfully";
                }
            }
            return PartialView("Mails");
        }
    }
}