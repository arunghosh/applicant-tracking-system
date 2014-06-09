using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Models
{
    public class MedicalDetailsVm
    {
            [Display(Name = "Medical Done Date")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
            public DateTime DoneDate { get; set; }

            [Display(Name = "Medical Expiry Date")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
            public DateTime ExpiryDate { get; set; }

            public MedicalDetailsVm()
            {
                DoneDate = DateTime.Now;
                ExpiryDate = DateTime.Now.AddDays(30);
            }

    }
}