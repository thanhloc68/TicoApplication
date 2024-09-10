using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ScaleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
