using Empleados.Models;
using Empleados.Web.General;
using Empleados.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Empleados.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        IHostingEnvironment _hostingEnvironment;


        public HomeController(ILogger<HomeController> logger)
        {
            //_logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                ViewBag.LstEstados = new List<SelectListItem> { 
                                new SelectListItem { Text = "Activo", Value = "1" },
                                new SelectListItem { Text = "Vacaciones", Value = "2" },
                                new SelectListItem { Text = "Incapacitado", Value = "3" },
                };

                ViewBag.LstPuestos = new List<SelectListItem> {
                                new SelectListItem { Text = "Desarrollador", Value = "1" },
                                new SelectListItem { Text = "Recursos Humanos", Value = "2" },
                                new SelectListItem { Text = "Project Manager", Value = "3" },
                };

                var url = "http://localhost:5062/api/Employees/GetEmployees";

                var service = new ServiceHelper<ResponseApi>();

                var responseApi = await service.GetRestServiceDataAsync(url);

                if (responseApi.result == 1)
                {
                    var lstEmpleados = JsonConvert.DeserializeObject<List<EmployeeModel>>(responseApi.data.ToString());

                    ViewBag.LstEmpleados = lstEmpleados;
                }
                else
                {
                    TempData["MensajeError"] = responseApi.msg;
                }
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var response = new Response();
            try
            {
                var url = string.Format("http://localhost:5062/api/Employees/GetEmployee?id={0}",id);

                var service = new ServiceHelper<ResponseApi>();

                var responseApi = await service.GetRestServiceDataAsync(url);

                if (responseApi.result == 1)
                {
                    var employee = JsonConvert.DeserializeObject<EmployeeModel>(responseApi.data.ToString());
                    response.data = employee;
                }
                else
                {
                    response.success = false;
                    response.msg = responseApi.msg;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.msg = ex.Message;
            }
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Save(EmployeeViewModel _employee)
        {
            var response = new Response();
            try
            {
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                
                if (_employee.ArchivoFotografia.Length > 0)
                {
                    string filePath = Path.Combine(uploads, _employee.ArchivoFotografia.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await _employee.ArchivoFotografia.CopyToAsync(fileStream);
                    }
                }

                EmployeeModel employee = new EmployeeModel
                {
                    Id = _employee.Id,
                    Nombre = _employee.Nombre,
                    Apellido = _employee.Apellido,
                    PuestoId = _employee.PuestoId,
                    FechaNacimiento = _employee.FechaNacimiento,
                    FechaContratacion = _employee.FechaContratacion,
                    Direccion = _employee.Direccion,
                    Telefono = _employee.Telefono,
                    CorreoElectronico = _employee.CorreoElectronico,
                    EstadoId = _employee.EstadoId,
                    Fotografia = "Sin Foto",
                };

                var service = new ServiceHelper<ResponseApi>();

                var url = employee.Id == 0 ? "http://localhost:5062/api/Employees/Insert" : "http://localhost:5062/api/Employees/Update";
                var responseApi = employee.Id == 0 ? await service.PutRestServiceDataAsync(url, employee) : await service.PostRestServiceDataAsync(url, employee);

                if (responseApi.result == 1)
                {
                    var newEmployee = employee.Id == 0 ? JsonConvert.DeserializeObject<EmployeeModel>(responseApi.data.ToString()) : null;
                    response.data = newEmployee;
                }
                else
                {
                    response.success = false;
                    response.msg = responseApi.msg;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.msg = ex.Message;
            }
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var response = new Response();
            try
            {
                var url = string.Format("http://localhost:5062/api/Employees/Delete?id={0}", id);

                var service = new ServiceHelper<ResponseApi>();

                var responseApi = await service.GetRestServiceDataAsync(url);

                if (responseApi.result == 1)
                {
                    //var employee = JsonConvert.DeserializeObject<EmployeeModel>(responseApi.data.ToString());
                    //response.data = employee;
                }
                else
                {
                    response.success = false;
                    response.msg = responseApi.msg;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.msg = ex.Message;
            }
            return Json(response);
        }




    }
}
