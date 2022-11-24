using API.ViewModels.Request;
using AutoMapper;
using BlogProject.Data;
using BlogProject.Models;
using BlogProject.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class UserController : Controller
   {
      private readonly IMapper _mapper;
      private readonly UserService _userService = new UserService();
      private readonly CommentService _commentService = new CommentService();
      private readonly PostService _postService = new PostService();
      private readonly TagService _tagService = new TagService();
      private readonly RoleService _roleService = new RoleService();

      public UserController(IMapper mapper)
      {
         _mapper = mapper;
      }

      [HttpGet]
      [Route("GetUsers")]
      public async Task<UserViewModel[]> GetUsers()
      {
         return await _userService.GetUserViewModels();
      }

      [HttpGet]
      [Route("GetUserById")]
      public async Task<UserViewModel> GetUserById(Guid id)
      {
         return await _userService.GetUserViewModelById(id);
      }

      [HttpGet]
      [Route("GetUserByEmail")]
      public async Task<UserViewModel> GetUserByEmail(string email)
      {
         return await _userService.GetUserViewModelByEmail(email);
      }

      [HttpPost]
      [Route("Register")]
      public async Task<IActionResult> Register(RegisterViewModel model)
      {
         if (String.IsNullOrEmpty(model.LastName) || String.IsNullOrEmpty(model.FirstName) || String.IsNullOrEmpty(model.Sex) || String.IsNullOrEmpty(model.Email) || String.IsNullOrEmpty(model.Password) || String.IsNullOrEmpty(model.PasswordConfirm))
            return BadRequest();
         if (model.LastName.Length < 2 || model.FirstName.Length > 50)
            return BadRequest();
         if (model.LastName.Length < 2 || model.LastName.Length > 50)
            return BadRequest();
         if (model.Sex.Length > 20)
            return BadRequest();
         if (model.Password.Length < 8 || model.Password.Length > 20 || model.Password != model.PasswordConfirm)
            return BadRequest();

         var user = _mapper.Map<RegisterViewModel, User>(model);
         var role = await _roleService.GetRoleByName("user");
         await _roleService.Save(user.Id, role.Id);
         await _userService.Save(user);

         return Ok();
      }

      [HttpPut]
      [Route("EditUser")]
      public async Task<IActionResult> EditUser(Guid userId, RegisterViewModel model)
      {
         if (String.IsNullOrEmpty(model.LastName) || String.IsNullOrEmpty(model.FirstName) || String.IsNullOrEmpty(model.Sex) || String.IsNullOrEmpty(model.Email) || String.IsNullOrEmpty(model.Password) || String.IsNullOrEmpty(model.PasswordConfirm))
            return BadRequest();
         if (model.LastName.Length < 2 || model.FirstName.Length > 50)
            return BadRequest();
         if (model.LastName.Length < 2 || model.LastName.Length > 50)
            return BadRequest();
         if (model.Sex.Length > 20)
            return BadRequest();
         if (model.Password.Length < 8 || model.Password.Length > 20 || model.Password != model.PasswordConfirm)
            return BadRequest();

         var newUser = _mapper.Map<RegisterViewModel, User>(model);
         var editUser = await _userService.GetUserById(userId);

         if (editUser == null)
            return BadRequest();

         await _userService.Update(editUser, newUser);

         return Ok();
      }

      [HttpDelete]
      [Route("DeleteUser")]
      public async Task<IActionResult> DeleteUser(Guid id)
      {
         var user = await _userService.GetUserById(id);

         if (user == null)

            return BadRequest();

         await _roleService.Delete(user.Id);
         await _userService.Delete(user);

         return Ok();
      }
   }
}
