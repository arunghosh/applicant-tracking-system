using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class Company:EntityBase
    {
        [Key]
        public int CompanyId { get; set; }

        [StringLength(256)]
        [Required]
        [Display(Name="Company Name")]
        public string CompanyName { get; set; }

        public virtual List<Project> Projects { get; set; }

        [StringLength(128)]
        [Required]
        public string Country { get; set; }

        public override int EntityKey
        {
            get { return CompanyId; }
        }
    }
}