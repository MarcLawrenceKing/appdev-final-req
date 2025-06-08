using Microsoft.AspNetCore.Mvc;

namespace appdev_final_req.Controllers
{
    public class MembersController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
