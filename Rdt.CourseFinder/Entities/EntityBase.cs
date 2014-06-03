using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Infrastructure;

namespace Rdt.CourseFinder.Entities
{
    public abstract class EntityBase
    {
        [TemplatesVisibility(ShowForEdit = false)]
        public DateTime CreatedOn { get; set; }

        [NotMapped]
        public abstract int EntityKey
        {
            get;
        }

        public EntityBase()
        {
            CreatedOn = DateTime.UtcNow;
        }


        [NotMapped]
        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public virtual bool IsNew
        {
            get
            {
                return EntityKey == 0;
            }
        }

        [NotMapped]
        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public virtual string ChangeStatus
        {
            get
            {
                return EntityKey == 0 ? "Added" : "Updated";
            }
        }
    }
}