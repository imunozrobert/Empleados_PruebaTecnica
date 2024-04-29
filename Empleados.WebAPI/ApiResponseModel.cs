using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empleados.WebAPI
{
    public class ApiResponseModel
    {
        public int result { get; set; }
        public object data { get; set; }
        public string msg { get; set; }

        public ApiResponseModel()
        {
            result = 1;
        }
    }
}
