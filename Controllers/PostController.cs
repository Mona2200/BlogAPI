using API.ViewModels.Request;
using AutoMapper;
using BlogProject.Data;
using BlogProject.Models;
using BlogProject.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class PostController : Controller
   {
      private readonly IMapper _mapper;
      private readonly UserService _userService = new UserService();
      private readonly CommentService _commentService = new CommentService();
      private readonly PostService _postService = new PostService();
      private readonly TagService _tagService = new TagService();

      public PostController(IMapper mapper)
      {
         _mapper = mapper;
      }

      [HttpGet]
      [Route("GetPosts")]
      public async Task<PostViewModel[]> GetPosts()
      {
         return await _postService.GetPostsViewModel();
      }

      [HttpGet]
      [Route("GetPostById")]
      public async Task<PostViewModel> GetPostById(Guid id)
      {
         return await _postService.GetPostViewModelById(id);
      }

      [HttpGet]
      [Route("GetPostsByUserId")]
      public async Task<PostViewModel[]> GetPostsByUserId(Guid userId)
      {
         return await _postService.GetPostsViewModelByUserId(userId);
      }

      [HttpPost]
      [Route("AddPost")]
      public async Task<IActionResult> AddPost(AddPostViewModel model)
      {
         if (String.IsNullOrEmpty(model.Title) || String.IsNullOrEmpty(model.Content))
            return BadRequest();

         var user = _userService.GetUserById(model.UserId);
         if (user == null)
            return BadRequest();

         Guid[] tagIds = model.TagIds;

         foreach (var tagId in tagIds)
         {
            var tag = _tagService.GetTagById(tagId);
            if (tag == null)
               return BadRequest();
         }

         var post = _mapper.Map<AddPostViewModel, Post>(model);
         await _postService.Save(post, tagIds);

         return Ok();
      }

      [HttpPut]
      [Route("EditPost")]
      public async Task<IActionResult> EditPost(Guid postId, AddPostViewModel model)
      {
         if (String.IsNullOrEmpty(model.Title) || String.IsNullOrEmpty(model.Content))
            return BadRequest();

         var user = _userService.GetUserById(model.UserId);
         if (user == null)
            return BadRequest();

         var newPost = _mapper.Map<AddPostViewModel, Post>(model);
         var editPost = await _postService.GetPostById(postId);

         if (editPost == null)
            return BadRequest();

         Guid[] tagIds = model.TagIds;

         foreach (var tagId in tagIds)
         {
            var tag = _tagService.GetTagById(tagId);
            if (tag == null)
               return BadRequest();
         }

         await _postService.Update(editPost, newPost, tagIds);

         return Ok();
      }

      [HttpDelete]
      [Route("DeletePost")]
      public async Task<IActionResult> DeletePost(Guid id)
      {
         var post = await _postService.GetPostById(id);
         if (post == null)
            return BadRequest();

         await _postService.Delete(post);

         return Ok();
      }
   }
}
