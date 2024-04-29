using Empleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empleados.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        List<EmployeeModel> GetAll();
        EmployeeModel Get(int id);
        EmployeeModel Insert(EmployeeModel employee);
        void Update(EmployeeModel employee);

        void Delete(int id);
        
    }
}
