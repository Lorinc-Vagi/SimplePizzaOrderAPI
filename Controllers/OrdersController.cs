using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePizzaOrderApi.Context;
using SimplePizzaOrderApi.Entities;

namespace SimplePizzaOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PizzaOrderContext context;

        public OrdersController(PizzaOrderContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("get-orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return await context.Orders.ToListAsync();
        }
        [HttpGet]
        [Route("get-order/{id}")]
        public async Task<ActionResult<Order>> GetOne(int id)
        {
            var unit = await context.Orders.FindAsync(id);
            if (unit == null) { return NotFound(); }
            return Ok(unit);
        }
        [HttpPost]
        [Route("create-order")]
        public async Task<ActionResult<Order>> Create([FromBody] Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            return Ok(order);
        }
        [HttpPut]
        [Route("update-order/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }
            if (!context.Orders.Any(b => b.Id == id))
            {
                return NotFound();
            }
            context.Entry(order).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("delete-order/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
