using QHomeGroup.Data.Entities.Projects;
using System.Collections.Generic;

namespace QHomeGroup.Application.Projects.Requests
{
    public class UpdateServiceRequest
    {
        public string Name { get; set; }

        public string VideoUrl { get; set; }

        public IList<BlockContent> Block { get; set; }

        public string Thumbnail { get; set; }

        public IList<string> SlideImages { get; set; }
    }
}
