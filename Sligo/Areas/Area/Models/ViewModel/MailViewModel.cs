using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sligo.Areas.Area.Models.ViewModel
{
    public class MailViewModel
    {
        public int MailId { get; set; }

        public string MailBody { get; set; }

        public string MailFileAttachment { get; set; }

        public string MailEmail { get; set; }

        public string MailFrom { get; set; }

        public string MailName { get; set; }

        public string MailMessage { get; set; }

        public string MailSubject { get; set; }

        public List<string> MailTo { get; set; }

    }
}