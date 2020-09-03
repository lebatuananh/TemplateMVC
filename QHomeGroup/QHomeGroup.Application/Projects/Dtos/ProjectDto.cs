using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Data.Enum;
using System;
using System.Collections.Generic;

namespace QHomeGroup.Application.Projects.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public IList<ProjectBlockContent> Block { get; set; }
        public OptionProject OptionProject { get; set; }
        public int LocationId { get; set; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public string SeoPageTitle { set; get; }
         public string SeoAlias { set; get; }
        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }
        public Status Status { set; get; }
        public Guid CreatedBy { get; set; }
        public int ServiceId { get; set; }
        public IList<string> SlideVideos { get; set; }
        public IList<string> SlideImages { get; set; }
        public SlideOption SlideOption { get; set; }
        public string LinkDetail { get; set; }
        public List<ProjectDto> RelatedProjects { get; set; }
    }
}
