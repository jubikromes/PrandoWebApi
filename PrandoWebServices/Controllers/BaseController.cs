using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrandoWebServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
    }
}