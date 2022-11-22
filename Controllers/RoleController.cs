using AutoMapper;
using BlogProject.Data;
using BlogProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class RoleController : Controller
   {
      private readonly IMapper _mapper;
      private readonly UserService _userService = new UserService();
      private readonly CommentService _commentService = new CommentService();
      private readonly PostService _postService = new PostService();
      private readonly RoleService _roleService = new RoleService();

      public RoleController(IMapper mapper)
      {
         _mapper = mapper;
      }

      [HttpGet]
      [Route("GetRoles")]
      public async Task<Role[]> GetRoles()
      {
         return await _roleService.GetRoles();
      }

      [HttpGet]
      [Route("GetRoleByUserId")]
      public async Task<Role[]> GetRoleByUserId(Guid userId)
      {
         return await _roleService.GetRoleByUserId(userId);
      }
   }
}
