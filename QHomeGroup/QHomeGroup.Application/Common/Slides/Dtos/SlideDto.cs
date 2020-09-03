using QHomeGroup.Data.Entities.Content;
using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Data.Enum;
using System.Collections.Generic;

namespace QHomeGroup.Application.Common.Slides.Dtos
{
    public class SlideDto
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public IList<string> SlideVideos { get; set; }
        public IList<string> SlideImages { get; set; }
        public Status Status { get; set; }
        public string Description { set; get; }
        public SlideOption SlideOption { get; set; }
        public SlideType SlideType { get; set; }
    }
}