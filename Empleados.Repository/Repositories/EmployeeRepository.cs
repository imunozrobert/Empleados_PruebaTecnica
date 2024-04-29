using Empleados.Data;
using Empleados.Models;
using Empleados.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empleados.Repository.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeData employeeContext;

        public EmployeeRepository(EmployeeData _employeeContext)
        {
            employeeContext = _employeeContext;
        }

        public List<EmployeeModel> GetAll()
        {
            return employeeContext.GetAll().ToList();
        }

        public EmployeeModel Get(int id)
        {
            return employeeContext.Get(id);
        }

        public EmployeeModel Insert(EmployeeModel employee)
        {
            return employeeContext.Insert(employee);
        }

        public void Update(EmployeeModel employee)
        {
            employeeContext.Update(employee);
        }

        public void Delete(int id)
        {
            employeeContext.Delete(id);
        }
    }
}
