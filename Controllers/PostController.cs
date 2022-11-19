using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class PostController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }
   }
}
