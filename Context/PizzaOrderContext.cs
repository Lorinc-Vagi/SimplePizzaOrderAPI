using Microsoft.EntityFrameworkCore;
using SimplePizzaOrderApi.Entities;

namespace SimplePizzaOrderApi.Context
{
    public class PizzaOrderContext : DbContext
    {
        public PizzaOrderContext(DbContextOptions<PizzaOrderContext> options) : base(options) { }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
