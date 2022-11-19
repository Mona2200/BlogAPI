using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class TagController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }
   }
}
