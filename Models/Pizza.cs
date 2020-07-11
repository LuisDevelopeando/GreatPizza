using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PizzaAPI.Model
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Topping> Toppings { get; set; } = new List<Topping>();

    }
}