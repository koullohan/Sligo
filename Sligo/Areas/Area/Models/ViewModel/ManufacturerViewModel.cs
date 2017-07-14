using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sligo.Areas.Area.Models.ViewModel
{
    public class ManufacturerViewModel
    {
        [Key]
        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        public string ManufacturerName { get; set; }

        [Required]
        public string ManufacturerLocation { get; set; }

        [Required]
        public string ManufacturerManager { get; set; }

        [Required]
        public string ManufacturerTelephone { get; set; }


        public string ManufacturerFax { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string ManufacturerEmail { get; set; }

        [Required]
        public string ManufacturerMobile { get; set; }

        [Required]
        public bool ManufacturerIsDelete { get; set; }

        [Required]
        public DateTime ManufacturerCreatedDate { get; set; }

        [Required]
        public DateTime ManufacturerModifiedDate { get; set; }

        [Required]
        public string ManufacturerCreatedBy { get; set; }

        [Required]
        public string ManufacturerModifiedBy { get; set; }

        public List<ManufacturerViewModel> ManufacturerList { get; set; }
    }
}