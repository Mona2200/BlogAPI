using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace API.ViewModels.Request
{
   public class RegisterViewModel
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Sex { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
      public string PasswordConfirm { get; set; }
   }
}
