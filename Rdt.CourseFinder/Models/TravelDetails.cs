using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Models
{
    public class TravelDetailsVm
    {
        [StringLength(128)]
        public string Airlines { get; set; }

        [Display(Name = "Travel Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime TravelDate { get; set; }

        [StringLength(64)]
        [Display(Name = "Boarding City")]
        public string BoardingCity { get; set; }


        public TravelDetailsVm()
        {
            TravelDate = DateTime.Now;
        }
    }
}