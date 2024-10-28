using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplePizzaOrderApi.Context;
using SimplePizzaOrderApi.Entities;

namespace SimplePizzaOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzaOrderContext context;

        public PizzasController(PizzaOrderContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Pizza>> GetAll()
        {
            return Ok(context.Pizzas.ToList());
        }


        [HttpGet("{id}")]
        public ActionResult<Pizza> GetById(int id)
        {
            var car = context.Pizzas.FirstOrDefault(c => c.Id == id);
            return Ok(context.Pizzas.ToList());//replace cars with Cars
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }
        [HttpPost]
        public ActionResult<Pizza> Create(Pizza pizza)
        {
            context.Pizzas.Add(pizza);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = pizza.Id }, pizza);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var carToRemove = context.Pizzas.FirstOrDefault(c => c.Id == id);
            if (carToRemove == null)
            {
                return NotFound();
            }
            context.Pizzas.Remove(carToRemove);
            context.SaveChanges();
            return NoContent();
        }
        [HttpPut("Id")]
        public IActionResult Update(int id, Pizza updatedPizza)
        {
            var pizzaToUpdate = context.Pizzas.FirstOrDefault(c => c.Id == id);
            if (pizzaToUpdate == null)
            {
                return NotFound();
            }
            pizzaToUpdate.Name = updatedPizza.Name;
            pizzaToUpdate.Price = updatedPizza.Price;
            pizzaToUpdate.Description = updatedPizza.Description;
            pizzaToUpdate.InStorck = updatedPizza.InStorck;

            context.SaveChanges();
            return NoContent();
        }

    }

}

