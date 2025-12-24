using Microsoft.AspNetCore.Mvc;

namespace WebApp6ByJessica.Controllers
{
    public class StateDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // ---------------- SESSION STATE ----------------
        [HttpPost]
        public IActionResult SetSession(string name)
        {
            HttpContext.Session.SetString("SessionName", name);
            ViewBag.Message = "Session value set successfully!";
            return View("Index");
        }

        public IActionResult GetSession()
        {
            ViewBag.SessionValue = HttpContext.Session.GetString("SessionName");
            return View("Index");
        }

        // ---------------- COOKIES ----------------
        [HttpPost]
        public IActionResult SetCookie(string email)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Append("UserEmail", email, option);
            ViewBag.Message = "Cookie set successfully!";
            return View("Index");
        }

        public IActionResult GetCookie()
        {
            ViewBag.CookieValue = Request.Cookies["UserEmail"];
            return View("Index");
        }

        // ---------------- TEMPDATA ----------------
        public IActionResult SetTempData()
        {
            TempData["TempMessage"] = "This message is stored in TempData!";
            return RedirectToAction("ShowTempData");
        }

        public IActionResult ShowTempData()
        {
            ViewBag.TempMessage = TempData["TempMessage"];
            return View("Index");
        }

        // ---------------- QUERY STRING ----------------
        public IActionResult QueryExample(string name, int age)
        {
            ViewBag.QueryName = name;
            ViewBag.QueryAge = age;
            return View("Index");
        }

        // ---------------- HIDDEN FIELD ----------------
        [HttpPost]
        public IActionResult HiddenExample(string hiddenValue)
        {
            ViewBag.HiddenValue = hiddenValue;
            return View("Index");
        }

        // ---------------- HTTP CONTEXT ----------------
        public IActionResult HttpContextExample()
        {
            HttpContext.Items["RequestID"] = Guid.NewGuid().ToString();
            ViewBag.RequestId = HttpContext.Items["RequestID"];
            return View("Index");
        }
    }
}
