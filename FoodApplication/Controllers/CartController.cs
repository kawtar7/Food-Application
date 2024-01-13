using FoodApplication.ContextDBConfig;
using FoodApplication.Models;
using FoodApplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodApplication.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        
        private readonly IData data;
        private readonly FoodDBContext context;
        public CartController(IData data,FoodDBContext context)
        {
            this.data = data;
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = await data.GetUser(HttpContext.User);
            var cartList=context.carts.Where(c=>c.UserId==user.Id).ToList();
            return View(cartList);
        }
        [HttpPost]
        public async Task<IActionResult> SaveCart(Cart cart)
        {
            var user =await data.GetUser(HttpContext.User);
            cart.UserId = user?.Id;
            if(ModelState.IsValid)
            {
                context.carts.Add(cart);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAddedCarts()
        {
            var user=await data.GetUser(HttpContext.User);
            var carts = context.carts.Where(c => c.UserId == user.Id).Select(c=>c.RecipeId).ToList();
            return Ok(carts);
        }
        [HttpPost]
        public IActionResult RemoveCartFromList(string Id)
        {
            if(!string.IsNullOrEmpty(Id))
            {
                var cart=context.carts.Where(c=>c.RecipeId == Id).FirstOrDefault();
                if(cart != null)
                {
                    context.carts.Remove(cart);
                    context.SaveChanges();
                    return Ok();
                }
                 
            }
           
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetCartList()
        {
            var user = await data.GetUser(HttpContext.User);
            var cartList = context.carts.Where(c => c.UserId == user.Id).Take(3).ToList();
            return PartialView("CartList", cartList);

        }
    }
}
