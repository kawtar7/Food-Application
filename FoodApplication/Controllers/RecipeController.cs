using FoodApplication.ContextDBConfig;
using FoodApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodApplication.Controllers
{
    public class RecipeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FoodDBContext context;
        public RecipeController(UserManager<ApplicationUser> userManager, FoodDBContext dBContext)
        {
            _userManager = userManager;
            context = dBContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetRecipeCard([FromBody] List<Recipe> recipes)
        {
            return PartialView("_RecipeCard", recipes);
        }
        public IActionResult Search([FromQuery] string recipe)
        {
            ViewBag.Recipe = recipe;
            return View();
        } public async Task<IActionResult> OrderAsync([FromQuery] string id)
        {
            ViewBag.id = id;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ShowOrderAsync(OrderRecipeDetails orderRecipeDetails)
        {
            Random random = new Random();
            ViewBag.Price = Math.Round(random.Next(150, 500) / 5.0) * 5;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.UserId = user?.Id;
            ViewBag.Address = user?.Address;
            return PartialView("_ShowOrder", orderRecipeDetails);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Order([FromForm] Order order)
        {

            order.OrderDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                context.orders.Add(order);
                context.SaveChanges();
                return RedirectToAction("Index","Recipe");
            }
            return RedirectToAction("Order", "Recipe", new {id=order.Id});
        }
    }
}
