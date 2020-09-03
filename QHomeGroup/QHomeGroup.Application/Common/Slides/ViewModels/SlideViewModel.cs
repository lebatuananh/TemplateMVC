using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Data.Enum;

namespace QHomeGroup.Application.Common.Slides.ViewModels
{
    public class SlideViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public SlideOption SlideOption { get; set; }
        public Status Status { get; set; }
    }
}