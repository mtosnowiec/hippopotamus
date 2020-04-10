using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Hippopotamus.Core.Tests.WebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index([FromQuery]int? delayResposeInMilliseconds = null)
        {
            if (delayResposeInMilliseconds.HasValue)
            {
                Thread.Sleep(delayResposeInMilliseconds.Value);
            }

            return View();
        }
    }
}
