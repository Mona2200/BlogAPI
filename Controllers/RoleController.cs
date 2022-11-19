using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class RoleController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }
   }
}
