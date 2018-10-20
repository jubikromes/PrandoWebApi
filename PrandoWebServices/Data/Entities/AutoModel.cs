using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrandoWebServices.Data.Abstractions;
using PrandoWebServices.Data.Identity;

namespace PrandoWebServices.Data.Entities
{
    public class AutoModel :  BaseEntity, IParentEntity
    {
        public string Title { get; set; }
        public string Number { get; set; }
        public int? YearofRelease { get; set; }
        public string Series { get; set; }

        public int AutoMakerId { get; set; }
        public AutoMaker AutoMaker { get; set; }
        public int Id { get ; set; }
    }
}
