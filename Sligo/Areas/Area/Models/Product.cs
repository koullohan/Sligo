using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sligo.Areas.Area.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Model { get; set; }


        public string ReleasedDate { get; set; }


        public int ReleasedYear { get; set; }

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