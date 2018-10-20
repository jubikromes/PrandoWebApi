using PrandoWebServices.Data.Abstractions;
using PrandoWebServices.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrandoWebServices.Data.Entities
{
    public abstract class BaseEntity 
    {
        public string ModifiedBy_Id { get; set; }
        public AppUser ModifiedBy { get; set; }
        public string CreatedBy_Id { get; set; }
        public AppUser CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
