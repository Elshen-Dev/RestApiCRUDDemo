using RestApiCRUDDemo.DAL;
using RestApiCRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.EmployeeData
{
    public class SqlEmployeesData : IEmployeeData
    {
        private EmployeeDbContext _employeeDb;
        public SqlEmployeesData(EmployeeDbContext employeeDb)
        {
            _employeeDb = employeeDb;
        }
        public Employee AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _employeeDb.Employees.Add(employee);
            _employeeDb.SaveChanges();
            return employee;
        }

        public void DeleteEmployee(Employee employee)
        {
            _employeeDb.Employees.Remove(employee);
            _employeeDb.SaveChanges();
        }

        public Employee EditEmployee(Employee employee)
        {
            var existingEmployee = _employeeDb.Employees.Find(employee.Id);
            if (existingEmployee!=null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Id = employee.Id;
                _employeeDb.Employees.Update(existingEmployee);
                _employeeDb.SaveChanges();
            }
            return employee;
        }

        public Employee GetEmployee(Guid id)
        {
            var employee = _employeeDb.Employees.Find(id);
            return employee;
        }

        public List<Employee> GetEmployees()
        {
            return _employeeDb.Employees.ToList();
        }
    }
}
