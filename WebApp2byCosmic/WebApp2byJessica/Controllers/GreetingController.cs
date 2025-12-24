using Microsoft.AspNetCore.Mvc;
using WebApp2byJessica.Services;


namespace WebAppbyJessica.Controllers
{
    [ApiController]
    [Route("greeting")]   // fixed route, ignores controller name
    public class GreetingController : ControllerBase
    {
        private readonly IGreetingService _greetingService;

        public GreetingController(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        [HttpGet]
        public ActionResult<string> GetGreeting()
        {
            return Ok(_greetingService.GetGreeting());
        }
    }

}
