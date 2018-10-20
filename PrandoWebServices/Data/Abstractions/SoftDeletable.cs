using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrandoWebServices.Data.Abstractions
{
    public  class SoftDeletable
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
