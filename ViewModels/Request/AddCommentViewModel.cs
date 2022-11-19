using BlogProject.ViewModels.Response;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Request
{
   public class AddCommentViewModel
   {
      public string Content { get; set; }
      public Guid PostId { get; set; }
      public Guid UserId { get; set; }
   }
}
