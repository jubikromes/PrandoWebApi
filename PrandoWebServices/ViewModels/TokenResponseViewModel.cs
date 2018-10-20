using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrandoWebServices.ViewModels
{
    public class TokenResponseViewModel
    {
        public string id { get; set; }
        public string auth_token { get; set; }
        public double expires_in { get; set; }


    }
}
