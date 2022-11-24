using API.ViewModels.Request;
using AutoMapper;
using BlogProject.Data;
using BlogProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class TagController : Controller
   {
      private readonly IMapper _mapper;
      private readonly UserService _userService = new UserService();
      private readonly CommentService _commentService = new CommentService();
      private readonly PostService _postService = new PostService();
      private readonly TagService _tagService = new TagService();

      public TagController(IMapper mapper)
      {
         _mapper = mapper;
      }

      [HttpGet]
      [Route("GetTags")]
      public async Task<Tag[]> GetTags()
      {
         return await _tagService.GetTags();
      }

      [HttpGet]
      [Route("GetTagById")]
      public async Task<Tag> GetTagById(Guid id)
      {
         return await _tagService.GetTagById(id);
      }

      [HttpGet]
      [Route("GetTagsByPostId")]
      public async Task<Tag[]> GetTagsByPostId(Guid id)
      {
         return await _tagService.GetTagByPostId(id);
      }

      [HttpPost]
      [Route("AddTag")]
      public async Task<IActionResult> AddTag(AddTagViewModel model)
      {
         if (String.IsNullOrEmpty(model.Name))
            return BadRequest();

         var tag = _mapper.Map<AddTagViewModel, Tag>(model);
         await _tagService.Save(tag);

         return Ok();
      }

      [HttpPut]
      [Route("EditTag")]
      public async Task<IActionResult> EditTag(Guid tagId, AddTagViewModel model)
      {
         if (String.IsNullOrEmpty(model.Name))
            return BadRequest();

         var editTag = await _tagService.GetTagById(tagId);

         if (editTag == null)
            return BadRequest();

         var newtag = _mapper.Map<AddTagViewModel, Tag>(model);
         await _tagService.Update(editTag, newtag);

         return Ok();
      }

      [HttpDelete]
      [Route("DeleteTag")]
      public async Task<IActionResult> DeleteTag(Guid id)
      {
         var tag = await _tagService.GetTagById(id);

         if (tag == null)
            return BadRequest();

         await _tagService.Delete(tag);

         return Ok();
      }
   }
}
