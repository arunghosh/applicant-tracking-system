using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class Log : EntityBase, IReadEntity
    {
        [Key]
        public int LogId { get; set; }

        [StringLength(256)]
        public string LogMessage { get; set; }

        public int UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [StringLength(32)]
        public string IPAddress { get; set; }

        public bool IsRead { get; set; }

        public LogTypes LogType { get; set; }

        public override int EntityKey
        {
            get { return LogId; }
        }
    }
}