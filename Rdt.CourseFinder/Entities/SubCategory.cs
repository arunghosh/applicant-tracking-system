using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class SubCategory : EntityBase
    {
        [Key]
        public int SubCategoryId { get; set; }

        [StringLength(32)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public override int EntityKey
        {
            get { return SubCategoryId; }
        }
    }
}