using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Model;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        [HttpGet("GetPizzas")]
        public async Task<ActionResult< List<Pizza> >> GetPizzas() 
        { 
            var listPizzas = await GetListPizzas();
            if ( !listPizzas.Any()) return NotFound();
            
            return listPizzas;
        }

        [HttpGet ("GetPizza") ]
        public async Task<ActionResult< Pizza >> GetPizza( int Id)
        {
            var listPizzas = await GetListPizzas();

            if (!listPizzas.Any()) return NotFound();
            var pizza = listPizzas.FirstOrDefault(p => p.Id == Id);

            if ( pizza == null ) return NotFound();
            return pizza;
        }

        [HttpGet("GetToppingsForPizza/{id}")]
        public async Task<ActionResult<List<Topping>>> GetToppingsForPizza( int id )
        {
            var listPizzas = await GetListPizzas();

            return listPizzas.First(x => x.Id == id).Toppings.ToList(); 
        }

        [HttpPost("AddPizza")]
        public async Task< ActionResult< List<Pizza> >> AddPizza( Pizza pizza )
        {
            var listPizzas = await GetListPizzas();

            listPizzas.Add(new Pizza()
                            {
                                Id = listPizzas.Count + 1 ,
                                Name = pizza.Name ,
                                Toppings = pizza.Toppings
                            }
            );

            return listPizzas;
        }
        
        [HttpPut("AddToppingToPizza/{id}")]
        public async Task<ActionResult< List<Pizza> >> AddToppingToPizza( Topping topping )
        {
            int Id = int.Parse( this.RouteData.Values["id"].ToString() );
            var listPizzas = await GetListPizzas();
            listPizzas.Find(x => x.Id == Id).Toppings.Add(topping);

            return listPizzas;
        }

        [HttpDelete("DeletePizza/{id}")]
        public async Task<ActionResult<List<Pizza>>> DeletePizza(int id)
        {
            var listPizzas = await GetListPizzas();
            var pizzaDelete = listPizzas.Find( p => p.Id == id);

            if (pizzaDelete == null) return NotFound();

            listPizzas.Remove(pizzaDelete);

            return listPizzas;
        }

        //DATA BASE
        private async Task< List<Pizza> > GetListPizzas()
        {
            var listPizzas = new List<Pizza>()
                    {
                        new Pizza() { Id = 1, Name = "Hawaiian", Toppings = new List<Topping>{ new Topping { Id = 1, Name = "Ham" } , new Topping { Id = 2, Name = "Pineapple" } } },
                        new Pizza() { Id = 2, Name = "Pepperoni", Toppings = new List<Topping>{ new Topping { Id = 3, Name = "Peperoni" } } },
                        new Pizza() { Id = 3, Name = "Irish", Toppings = new List<Topping>{ new Topping { Id = 4, Name = "Potatoes" } , new Topping { Id = 5, Name = "Cabbage" } } }
            };
            return listPizzas;
        }

    }
}