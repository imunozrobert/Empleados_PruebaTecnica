using Empleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empleados.Data
{
    public class EmployeeData
    {
        const string spCrud = "EmpleadosCRUD_Sp";
       public IEnumerable<EmployeeModel> GetAll()
       {
            using (var dbManager = new DbManager())
            {
                return dbManager.ProcedimientoAlmacenado<EmployeeModel>(spCrud,new { P_Option = "GET" });
            }
        }

        public EmployeeModel Get(int id)
        {
            using (var dbManager = new DbManager())
            {
                return dbManager.ProcedimientoAlmacenado<EmployeeModel>(spCrud, new { P_Option = "GET", P_IdEmpleado = id}).First();
            }
        }

        public EmployeeModel Insert(EmployeeModel employee)
        {
            using (var dbManager = new DbManager())
            {
                var idNewEmployee = dbManager.ProcedimientoAlmacenado<int>(spCrud, new { 
                                                                    P_Option = "INS", 
                                                                    P_Fotografia = employee.Fotografia, 
                                                                    P_Nombre = employee.Nombre, 
                                                                    P_Apellido = employee.Apellido, 
                                                                    P_PuestoId = employee.PuestoId, 
                                                                    P_FechaNacimiento = employee.FechaNacimiento, 
                                                                    P_FechaContratacion = employee.FechaContratacion, 
                                                                    P_Direccion = employee.Direccion, 
                                                                    P_Telefono = employee.Telefono, 
                                                                    P_CorreoElectronico = employee.CorreoElectronico, 
                                                                    P_EstadoId = employee.EstadoId, 
                }).First();

                employee.Id = idNewEmployee;
                return employee;
            }
        }

        public void Update(EmployeeModel employee)
        {
            using (var dbManager = new DbManager())
            {
                dbManager.ProcedimientoAlmacenado(spCrud, new
                {
                    P_Option = "UPD",
                    P_IdEmpleado = employee.Id,
                    P_Fotografia = employee.Fotografia,
                    P_Nombre = employee.Nombre,
                    P_Apellido = employee.Apellido,
                    P_PuestoId = employee.PuestoId,
                    P_FechaNacimiento = employee.FechaNacimiento,
                    P_FechaContratacion = employee.FechaContratacion,
                    P_Direccion = employee.Direccion,
                    P_Telefono = employee.Telefono,
                    P_CorreoElectronico = employee.CorreoElectronico,
                    P_EstadoId = employee.EstadoId,
                });
            }
        }

        public void Delete(int id)
        {
            using (var dbManager = new DbManager())
            {
                dbManager.ProcedimientoAlmacenado(spCrud, new { P_Option = "DEL", P_IdEmpleado = id });
            }
        }

    }
}
