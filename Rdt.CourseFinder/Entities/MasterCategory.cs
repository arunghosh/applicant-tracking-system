using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class MasterCategory: EntityBase
    {
        [Key]
        public int MasterCategoryId { get; set; }

        [StringLength(32)]
        public string Name { get; set; }

        public virtual List<Category> Categories { get; set; }

        public override int EntityKey
        {
            get {return MasterCategoryId; }
        }
    }
}