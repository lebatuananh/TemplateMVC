using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Data.Enum;

namespace QHomeGroup.Application.Content.Blogs.Request
{
    public class UpdateBlogRequest
    {
        [Required] [MaxLength(256)] public string Name { set; get; }
        [MaxLength(256)] public string Thumbnail { set; get; }
        [MaxLength(500)] public string Description { set; get; }
        public string Content { set; get; }
        public bool? HotFlag { set; get; }
        public Status Status { set; get; }
        public IList<string> SlideImages { get; set; }
        public IList<string> SlideVideos { get; set; }
        public SlideOption SlideOption { get; set; }
        public string Tags { get; set; }
        public IList<BlockContent> Block { get; set; }

    }
}