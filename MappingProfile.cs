using AutoMapper;
using API.ViewModels.Request;
using BlogProject.Models;

namespace API
{
   public class MappingProfile : Profile
   {
      public MappingProfile()
      {
         CreateMap<RegisterViewModel, User>();
         CreateMap<AddPostViewModel, Post>();
         CreateMap<AddCommentViewModel, Comment>();
         CreateMap<AddTagViewModel, Tag>();

         CreateMap<User, RegisterViewModel>().ForMember(m => m.PasswordConfirm, opt => opt.MapFrom(u => u.Password));
         CreateMap<Post, AddPostViewModel>();
         CreateMap<Comment, AddCommentViewModel>();
      }
   }
}
