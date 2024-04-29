using Empleados.Models;

namespace Empleados.Web.Models
{
    public class EmployeeViewModel : EmployeeModel
    {
        public IFormFile ArchivoFotografia { get; set; }
    }
}
