using PrandoWebServices.Data.Abstractions;
using PrandoWebServices.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrandoWebServices.Data.Entities
{
    public class Owner : BaseEntity, IParentEntity
    {
        public string IdentityUserId { get; set; }
        public AppUser IdentityUser { get; set; }
        public char Gender { get; set; }
        public DateTime? DateofBirth { get; set; }
        public int Id { get ; set ; }
    }
}
