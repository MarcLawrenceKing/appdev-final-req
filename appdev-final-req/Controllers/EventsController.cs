using Microsoft.AspNetCore.Mvc;

namespace appdev_final_req.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
