using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Model;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToppingController : ControllerBase
    {
        [HttpGet("GetToppings")]
        public async Task<ActionResult<List<Topping>>> GetToppings()
        {
            var listToppings = await GetListToppings();
            if ( !listToppings.Any() ) return NotFound();

            return listToppings;
        }

        [HttpPost("AddTopping")]
        public async Task<ActionResult<List<Topping>>> AddTopping(Topping topping)
        {
            var listToppings = await GetListToppings();

            if (!listToppings.Any()) return NotFound();

            listToppings.Add(new Topping()
            {
                Id = listToppings.Count + 1,
                Name = topping.Name
            }
            );

            return listToppings;
        }

        [HttpDelete("DeleteTopping/{id}")]
        public async Task<ActionResult<List<Topping>>> DeleteTopping(int id)
        {
            var listToppings = await GetListToppings();
            if (!listToppings.Any()) return NotFound();

            var toppingDelete = listToppings.Find(p => p.Id == id);
            if (toppingDelete == null) return NotFound();

            listToppings.Remove(toppingDelete);

            return listToppings;
        }

        //DATA BASE
        private async Task<List<Topping>> GetListToppings()
        {
            var listToppings = new List<Topping>()
            {
                new Topping() { Id = 1, Name = "Ham" },
                new Topping() { Id = 2, Name = "Pineapple" },
                new Topping() { Id = 3, Name = "Tomatoes" },
                new Topping() { Id = 4, Name = "Cabbage" }
            };
            return listToppings;
        }

    }
}