using BlogProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace API.ViewModels.Request
{
   public class AddPostViewModel
   {
      public string Title { get; set; }
      public string Content { get; set; }
      public Guid[] TagIds { get; set; }
      public Guid UserId { get; set; }
   }
}
