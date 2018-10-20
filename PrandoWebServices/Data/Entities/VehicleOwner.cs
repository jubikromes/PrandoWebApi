using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrandoWebServices.Data.Abstractions;
using PrandoWebServices.Data.Identity;

namespace PrandoWebServices.Data.Entities
{
    public class VehicleOwner : BaseEntity, IParentEntity
    {
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public bool IsCurrentOwner { get; set; }
        public int Id { get; set ; }
    }
}
