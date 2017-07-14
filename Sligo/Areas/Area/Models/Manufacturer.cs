using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sligo.Areas.Area.Models
{
    public class Manufacturer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Manager { get; set; }

        [Required]
        public string Telephone { get; set; }


        public string Fax { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public string ModifiedBy { get; set; }
    }
}