using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp7ByJessica.Controllers
{
    public class SecureController : Controller
    {
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Public()
        {
            return View();
        }
    }
}
