using API.ViewModels.Request;
using AutoMapper;
using BlogProject.Data;
using BlogProject.Models;
using BlogProject.ViewModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;

namespace API.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class CommentController : Controller
   {
      private readonly IMapper _mapper;
      private readonly UserService _userService = new UserService();
      private readonly CommentService _commentService = new CommentService();
      private readonly PostService _postService = new PostService();

      public CommentController(IMapper mapper)
      {

         _mapper = mapper;
      }

      [HttpGet]
      [Route("GetComments")]
      public async Task<CommentViewModel[]> GetComments()
      {
         return await _commentService.GetCommentsViewModel();
      }

      [HttpGet]
      [Route("GetCommentById")]
      public async Task<CommentViewModel> GetCommentById(Guid id)
      {
         return await _commentService.GetCommentViewModelById(id);
      }

      [HttpGet]
      [Route("GetCommentsByUserId")]
      public async Task<CommentViewModel[]> GetCommentsByUserId(Guid userId)
      {
         return await _commentService.GetCommentsViewModelByUserId(userId);
      }

      [HttpGet]
      [Route("GetCommentsByPostId")]
      public async Task<CommentViewModel[]> GetCommentsByPostId(Guid postId)
      {
         return await _commentService.GetCommentsViewModelByPostId(postId);
      }

      [HttpPost]
      [Route("AddComment")]
      public async Task<IActionResult> AddComment(AddCommentViewModel view)
      {
         if (String.IsNullOrEmpty(view.Content))
            return BadRequest();

         var user = await _userService.GetUserById(view.UserId);
         var post = await _postService.GetPostById(view.PostId);

         if (user == null || post == null)
            return BadRequest();

         var comment = _mapper.Map<AddCommentViewModel, Comment>(view);
         await _commentService.Save(comment);

         return Ok();
      }

      [HttpPut]
      [Route("EditComment")]
      public async Task<IActionResult> EditComment(Guid commentId, AddCommentViewModel view)
      {
         if (String.IsNullOrEmpty(view.Content))
            return BadRequest();

         var user = await _userService.GetUserById(view.UserId);
         var post = await _postService.GetPostById(view.PostId);
         var comment = await _commentService.GetCommentById(commentId);

         if (user == null || post == null || comment == null)
            return BadRequest();

         var newComment = _mapper.Map<AddCommentViewModel, Comment>(view);
         await _commentService.Update(comment, newComment);

         return Ok();
      }

      [HttpDelete]
      [Route("DeleteComment")]
      public async Task<IActionResult> DeleteComment(Guid commentId)
      {
         var comment = await _commentService.GetCommentById(commentId);
         if (comment == null)
            return BadRequest();

         await _commentService.Delete(comment);

         return Ok();

      }
   }
}
