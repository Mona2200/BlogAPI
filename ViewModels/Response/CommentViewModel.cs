﻿using API.Models;

namespace API.ViewModels.Response
{
   public class CommentViewModel
   {
      public Guid Id { get; set; }
      public string Content { get; set; }
      public Post Post { get; set; }
      public User User { get; set; }
   }
}
