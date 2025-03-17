using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_crud.Data;
using dotnet_crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_crud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryItemEmployeeApiController : ControllerBase
    {
        private readonly ApiContext _context;

        public HistoryItemEmployeeApiController(ApiContext context){
            _context = context;
        }

        [HttpGet]
        public IActionResult getAllHistory(){
            var result = _context.HistoryItemEmployees
                .Include(h => h.Employee)
                .Include(h => h.Item)
                .ToList();
            return new JsonResult(Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult getHistoryById(int id){
            var isFind = _context.HistoryItemEmployees
                .Include(h => h.Employee)
                .Include(h => h.Item)
                .FirstOrDefault(h => h.Id == id);
            if(isFind == null)
                return BadRequest(NotFound(new {msg = "not found history id"}));
            return new JsonResult(Ok(isFind));
        }

        [HttpGet("emp/{id}")]
        public async Task<IActionResult> getHistoryByEmpId(int id){
            var isFind = await _context.HistoryItemEmployees
                .Where(
                    h => h.EmployeeId == id
                )
                .Include(h => h.Employee)
                .Include(h => h.Item)
                .ToListAsync();
            if(isFind == null)
                return BadRequest(NotFound(new {msg = "not found employee id"}));
            return new JsonResult(Ok(isFind));
        }

        [HttpGet("item/{id}")]
        public async Task<IActionResult> getHistoryByItemId(int id){
            var isFind = await _context.HistoryItemEmployees
                .Where(
                    h => h.ItemId == id
                )
                .Include(h => h.Employee)
                .Include(h => h.Item)
                .ToListAsync();
            if(isFind == null)
                return BadRequest(NotFound(new {msg = "not found item id"}));
            return new JsonResult(Ok(isFind));
        }

        [HttpPost]
        public async Task<IActionResult> postHistoryItemEmployee(PostHistoryItemEmployee obj){
            var emp = await _context.Employees.FindAsync(obj.EmpId);
            var item = await _context.Items.FindAsync(obj.ItemId);

            if(emp == null)
                return BadRequest(NotFound(new {msg = "not found employee id"}));
            else if(item == null)
                return BadRequest(NotFound(new {msg = "not found item id"}));
            else if(obj.Qty < 1)
                return BadRequest(NotFound(new {msg = "QTY must more than 0"}));
            else if(obj.Qty > item.Qty)
                return BadRequest(NotFound(new {msg = "Item qty not enough"}));

            item.Qty -= obj.Qty;

            var data = new HistoryItemEmployee{
                EmployeeId = obj.EmpId,
                Employee = emp,
                ItemId = obj.ItemId,
                Item = item,
                Qty = obj.Qty
            };

            _context.HistoryItemEmployees.Add(data);
            await _context.SaveChangesAsync();
            return new JsonResult(Ok(data));
        }
    }

    public class PostHistoryItemEmployee {
        public int EmpId {get; set;}
        public int ItemId {get; set;}
        public int Qty {get; set;}
    }
}