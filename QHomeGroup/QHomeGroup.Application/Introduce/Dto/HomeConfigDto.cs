using QHomeGroup.Infrastructure.SharedKernel;

namespace QHomeGroup.Application.Introduce.Dto
{
    public class HomeConfigDto : DomainEntity<int>
    {
        public string VideoUrl { get; set; }

        public string Content { get; set; }

        public string Link { get; set; }

        public string ImageClassic { get; set; }

        public string ContentClassic { get; set; }

        public string ImageModern { get; set; }

        public string ContentModern { get; set; }

        public string ProductContent { get; set; }
    }
}
