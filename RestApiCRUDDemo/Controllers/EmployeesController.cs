﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiCRUDDemo.EmployeeData;
using RestApiCRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.Controllers
{
    
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeData _employeeData;

        public EmployeesController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);
            if (employee!=null)
            {
                return Ok(employee);
            }
            return NotFound($"Employee with Id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult GetEmployee(Employee employee)
        {
            _employeeData.AddEmployee(employee);
            return Created(HttpContext.Request.Scheme+ "://" + HttpContext.Request.Host + HttpContext.Request.Path+ "/" + employee.Id,
                employee);
            
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);
            if (employee!=null)
            {
                _employeeData.DeleteEmployee(employee);
                return Ok();
            }
            return NotFound($"Employee with Id: {id} was not found");

        }
        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditEmployee(Guid id,Employee employee)
        {
            var existingEmployee = _employeeData.GetEmployee(id);
            if (existingEmployee!=null)
            {
                employee.Id = existingEmployee.Id;
                _employeeData.EditEmployee(employee);
            }
            else
            {
                return NotFound($"There is no such employee");
            }
            if (employee.Name==null)
            {
                return NotFound($"{employee} don't be null");
            }
           
            return Ok(employee);
        }

    }
}
