using Microsoft.AspNetCore.Mvc;
using dotnet_crud.Data;
using dotnet_crud.Models;

namespace dotnet_crud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsApiController : ControllerBase
    {
        private readonly ApiContext _context;

        public ItemsApiController(ApiContext context){
            _context = context;
        }

        [HttpGet]
        public IActionResult getAllItems(){
            var result = _context.Items.ToList();
            return new JsonResult(Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult getByIdItem(int id){
            var isFind = _context.Items.Find(id);

            if(isFind == null)
                return BadRequest(NotFound());

            return new JsonResult(Ok(isFind));
        }

        [HttpPost]
        public IActionResult createItems(Item item){
            if(item.Id == 0){
                // if field empty return
                if(string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.Detail))
                    return BadRequest(new {Message = "have field empty"});
                if(!item.Qty.HasValue)
                    item.Qty = 0;
                _context.Items.Add(item);
            }
            else {
                // if Id have value for create return
                return BadRequest(NotFound());
            }
            _context.SaveChanges();

            return new JsonResult(Ok(item));
        }

        [HttpPut("{id}")]
        public IActionResult updateItems(int id, Item item){
            var isFind = _context.Items.Find(id);
            if(isFind == null)
                return BadRequest(NotFound());

            // make optional field for update
            if(!string.IsNullOrEmpty(item.Name))
                isFind.Name = item.Name;
            if(!string.IsNullOrEmpty(item.Detail))
                isFind.Detail = item.Detail;
            if(item.Qty.HasValue)
                isFind.Qty = item.Qty;
                
            _context.SaveChanges();
            return new JsonResult(Ok(isFind));
        }

        [HttpDelete("{id}")]
        public IActionResult deleteItem(int id){
            var isFind = _context.Items.Find(id);
            if(isFind == null)
                return BadRequest(NotFound());
            
            _context.Items.Remove(isFind);
            _context.SaveChanges();
            return new JsonResult(new {message = $"remove item id {id}"});
        }
    }
}