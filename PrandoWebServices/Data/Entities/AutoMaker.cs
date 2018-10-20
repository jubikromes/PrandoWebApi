using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrandoWebServices.Data.Abstractions;
using PrandoWebServices.Data.Identity;

namespace PrandoWebServices.Data.Entities
{
    public class AutoMaker : BaseEntity, IParentEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string CountryofManufacture { get; set; }
        public int Id { get ; set ; }
    }
}
