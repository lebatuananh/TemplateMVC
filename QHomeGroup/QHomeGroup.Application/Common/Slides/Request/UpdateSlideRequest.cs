using QHomeGroup.Data.Entities.Content;
using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Data.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QHomeGroup.Application.Common.Slides.Request
{
    public class UpdateSlideRequest
    {
        public string Name { set; get; }
        public Status Status { set; get; }
        public IList<string> SlideImages { get; set; }
        public IList<string> SlideVideos { get; set; }
        public SlideOption SlideOption { get; set; }
        public string Description { get; set; }
        public SlideType SlideType { get; set; }
    }
}