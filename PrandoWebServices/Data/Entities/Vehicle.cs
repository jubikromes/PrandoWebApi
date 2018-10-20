using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrandoWebServices.Data.Abstractions;
using PrandoWebServices.Data.Identity;

namespace PrandoWebServices.Data.Entities
{
    public class Vehicle : BaseEntity, IParentEntity
    {
        public string RegistrationNumber { get; set; }
        public string VIN { get; set; }
        public string Colour { get; set; }
        public string BodyType { get; set; }
        public int? YearOfManufacture { get; set; }

        public VehicleState VehicleState { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
        public int Id { get ; set ; }
    }

    public enum VehicleStatus {
        For_Sale = 1, Stolen, In_Use, Newly_Acquired
    }
    public enum VehicleState
    {
        New = 1, 
    }

}
