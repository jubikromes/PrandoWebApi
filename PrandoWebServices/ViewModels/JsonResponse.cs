using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrandoWebServices.ViewModels
{
    public class JsonWrapperResponse<T> : JsonResponse
    {
        public T Result { get; set; }
    }

    public class JsonResponse
    {
            public bool HasError { get; set; }
            public List<string> Errors { get; set; }
            public string Message { get; set; }
            public int ResponseCode { get; set; }
    }
}
