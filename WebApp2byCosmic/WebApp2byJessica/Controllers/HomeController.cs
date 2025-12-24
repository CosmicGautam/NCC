
using Microsoft.AspNetCore.Mvc;
namespace WebApp2byJessica.Services
{
    public class HomeController : Controller
    {
        private readonly IEnumerable<IGreetingService> _greetingServices;

        public HomeController(IEnumerable<IGreetingService> greetingServices)
        {
            _greetingServices = greetingServices;
        }

        public IActionResult Index()
        {
            // Call GetGreeting() on each service and join them
            var messages = _greetingServices.Select(s => s.GetGreeting());
            ViewData["Message"] = string.Join("<br>", messages);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}