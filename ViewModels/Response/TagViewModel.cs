using API.Models;

namespace API.ViewModels.Response
{
   public class TagViewModel
   {
      public Tag[] Tags { get; set; }
      public Guid TagId { get; set; }
      public string TagName { get; set; }
   }
}
