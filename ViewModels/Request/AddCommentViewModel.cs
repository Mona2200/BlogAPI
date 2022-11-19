using BlogProject.ViewModels.Response;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.ViewModels.Request
{
   public class AddCommentViewModel
   {
      public Guid Id { get; set; }
      public string Content { get; set; }
      public Guid PostId { get; set; }
      public Guid UserId { get; set; }
   }
}
