using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrandoWebServices.Data.Abstractions;
using PrandoWebServices.Data.Identity;

namespace PrandoWebServices.Data.Entities
{
    public class Attachment : BaseEntity, IParentEntity
    {
        public string BaseUrl { get; set; }
        public string Url { get; set; }
        public int Id { get; set; }
    }
}
