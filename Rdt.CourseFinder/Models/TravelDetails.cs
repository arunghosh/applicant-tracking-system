using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
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


        [NotMapped]
        public string FormattedDate
        {
            get
            {
                return TravelDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }


        public TravelDetailsVm()
        {
            TravelDate = DateTime.Now;
        }
    }
}