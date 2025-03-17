using dotnet_crud.Data;
using dotnet_crud.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_crud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesApiController : ControllerBase
    {
        private readonly ApiContext _context;

        public EmployeesApiController(ApiContext context){
            _context = context;
        }

        [HttpGet("all/{his?}")]
        public IActionResult getAllEmployee(int? his = 0){
            dynamic result;
            if(his == 0)
                result = _context.Employees.Select(e => (new {Id = e.Id, FirstName = e.FirstName, LastName = e.LastName})).ToList();
            else
                result = _context.Employees.Select(e => (new {Id = e.Id, FirstName = e.FirstName, LastName = e.LastName, HistoryItems = e.HistoryItems})).ToList();
            return new JsonResult(Ok(result));
        }
        
        [HttpGet("{id}/{his?}")]
        public IActionResult getEmployee(int id, int his = 0){
            dynamic result;
            if(his == 0)
                result = _context.Employees.Where(e => e.Id == id).Select(e => (new {Id = e.Id, FirstName = e.FirstName, LastName = e.LastName}));
            else
                result = _context.Employees.Where(e => e.Id == id).Select(e => (new {Id = e.Id, FirstName = e.FirstName, LastName = e.LastName, HistoryItems = e.HistoryItems}));
            return new JsonResult(Ok(result));
        }

        [HttpPost]
        public IActionResult createEmployee(Employee emp){
            // if have empty field return
            if(
                string.IsNullOrEmpty(emp.FirstName) ||
                string.IsNullOrEmpty(emp.LastName)
            )
                return BadRequest(new {message = "have empty field"});

            _context.Add(emp);
            _context.SaveChanges();
            return new JsonResult(Ok(emp));
        }

        [HttpPost("list")]
        public IActionResult createEmployees(List<Employee> emps){
            foreach(var emp in emps){
                if(
                    string.IsNullOrEmpty(emp.FirstName) ||
                    string.IsNullOrEmpty(emp.LastName)
                )
                    return BadRequest(new {message = "have empty field", emp});
            }

            _context.AddRange(emps);
            _context.SaveChanges();

            return new JsonResult(Ok(emps));
        }

        [HttpPut("{id}")]
        public IActionResult updateEmployee(int id, Employee emp){
            var isFind = _context.Employees.Find(id);
            if(isFind == null)
                return BadRequest(NotFound());

            if(!string.IsNullOrEmpty(emp.FirstName))
                isFind.FirstName = emp.FirstName;
            if(!string.IsNullOrEmpty(emp.LastName))
                isFind.LastName = emp.LastName;
            _context.SaveChanges();

            return new JsonResult(Ok(isFind));
        }

        [HttpDelete("{id}")]
        public IActionResult deleteEmployee(int id){
            var isFind = _context.Employees.Find(id);
            if(isFind == null)
                return BadRequest(NotFound());

            _context.Employees.Remove(isFind);
            _context.SaveChanges();
            return new JsonResult(new {message = $"remove employee id ${id}"});
        }
    }
}