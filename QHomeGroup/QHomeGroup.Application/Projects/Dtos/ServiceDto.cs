using QHomeGroup.Data.Entities.Projects;
using System.Collections.Generic;

namespace QHomeGroup.Application.Projects.Dtos
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string VideoUrl { get; set; }

        public string EmbededId { get; set; }
        public string Thumbnail { get; set; }

        public IList<BlockContent> Block { get; set; }

        public IList<ProjectDto> Projects { get; set; }

        public IList<string> SlideImages { get; set; }
    }
}
