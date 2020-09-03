using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Data.Enum;
using System;
using System.Collections.Generic;

namespace QHomeGroup.Application.Content.Blogs.Dtos
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Thumbnail { set; get; }
        public string Description { set; get; }
        public IList<BlockContent> Block { get; set; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public string Tags { get; set; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Status Status { set; get; }
        public string SeoPageTitle { set; get; }
        public string SeoAlias { set; get; }
        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }
        public IList<string> SlideVideos { get; set; }
        public IList<string> SlideImages { get; set; }
        public Guid CreatedBy { get; set; }
        public SlideOption SlideOption { get; set; }
        public string LinkDetail { get; set; }
        public List<BlogDto> BlogInterest { get; set; }
    }
}