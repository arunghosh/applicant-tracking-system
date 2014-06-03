using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class Category : EntityBase
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(32)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public int MasterCategoryId { get; set; }

        [ForeignKey("MasterCategoryId")]
        public virtual MasterCategory MasterCategory { get; set; }

        public virtual List<SubCategory> SubCategories { get; set; }

        public override int EntityKey
        {
            get { return CategoryId; }
        }
    }
}