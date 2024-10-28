using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePizzaOrderApi.Context;
using SimplePizzaOrderApi.Entities;

namespace SimplePizzaOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly PizzaOrderContext context;

        public CustomersController(PizzaOrderContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("get-costumers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            return await context.Customers.ToListAsync();
        }
        [HttpGet]
        [Route("get-Customer/{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var unit = await context.Customers.FindAsync(id);
            if (unit == null) { return NotFound(); }
            return Ok(unit);
        }
        [HttpPost]
        [Route("create-customer")]
        public async Task<ActionResult<Customer>> Create([FromBody] Customer customer)
        {
            context.Customers.Add(customer); 
            await context.SaveChangesAsync();
            return Ok(customer);
        }
        [HttpPut]
        [Route("update-customer/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
        {
            if (id!=customer.Id)
            {
                return BadRequest();
            }
            if (!context.Customers.Any(b=>b.Id==id))
            {
                return NotFound();
            }
            context.Entry(customer).State=EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("delete-customer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer==null)
            {
                return NotFound();
            }
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
