﻿namespace SimplePizzaOrderApi.Entities
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool InStorck { get; set; }
    }
}
