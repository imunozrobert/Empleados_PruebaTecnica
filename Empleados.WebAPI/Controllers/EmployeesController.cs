using Empleados.Models;
using Empleados.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empleados.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeesController(IEmployeeRepository _employeeRepository)
        {
            this.employeeRepository = _employeeRepository;
        }

        [HttpGet]
        [Route("GetEmployees")]
        public ApiResponseModel Get()
        {
            var response = new ApiResponseModel();
            try
            {
                response.data = employeeRepository.GetAll();
            }
            catch (Exception ex)
            {
                response.result = 0;
                response.msg = ex.Message;
            }
            return response;
        }

        [HttpGet]
        [Route("GetQuery")]
        public ApiResponseModel GetQuery(string? name, string? firstName)
        {
            var response = new ApiResponseModel();
            try
            {
                response.data = employeeRepository.GetQuery(name,firstName);
            }
            catch (Exception ex)
            {
                response.result = 0;
                response.msg = ex.Message;
            }
            return response;
        }

        [HttpGet]
        [Route("GetEmployee")]
        public ApiResponseModel GetEmployee(int id)
        {
            var response = new ApiResponseModel();
            try
            {
                response.data = employeeRepository.Get(id);
            }
            catch (Exception ex)
            {
                response.result = 0;
                response.msg = ex.Message;
            }
            return response;
        }

        [HttpPut]
        [Route("Insert")]
        public ApiResponseModel Insert([FromBody] EmployeeModel employee)
        {
            var response = new ApiResponseModel();
            try
            {
                response.data = employeeRepository.Insert(employee);
                response.msg = "Datos guardados con éxito.";
            }
            catch (Exception ex)
            {
                response.result = 0;
                response.msg = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("Update")]
        public ApiResponseModel Update([FromBody] EmployeeModel employee)
        {
            var response = new ApiResponseModel();
            try
            {
                employeeRepository.Update(employee);
                response.msg = "Datos actualizados con éxito.";
            }
            catch (Exception ex)
            {
                response.result = 0;
                response.msg = ex.Message;
            }
            return response;
        }

        [HttpGet]
        [Route("Delete")]
        public ApiResponseModel Delete(int id)
        {
            var response = new ApiResponseModel();
            try
            {
                employeeRepository.Delete(id);
                response.msg = "Registro eliminado con éxito.";
            }
            catch (Exception ex)
            {
                response.result = 0;
                response.msg = ex.Message;
            }
            return response;
        }
    }
}
