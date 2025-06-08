using Microsoft.AspNetCore.Mvc;

namespace appdev_final_req.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
