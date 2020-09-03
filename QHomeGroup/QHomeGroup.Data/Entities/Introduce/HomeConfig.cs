using QHomeGroup.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace QHomeGroup.Data.Entities.Introduce
{
    [Table("HomeConfigs")]
    public class HomeConfig : DomainEntity<int>
    {
        public string VideoUrl { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public string ImageClassic { get; set; }
        public string ContentClassic { get; set; }
        public string ImageModern { get; set; }
        public string ContentModern { get; set; }
        public string ProductContent { get; set; }

        public HomeConfig(string videoUrl, string content, string imageClassic, string contentClassic, string imageModern, string contentModern, string productContent)
        {
            VideoUrl = videoUrl;
            Content = content;
            ImageClassic = imageClassic;
            ContentClassic = contentClassic;
            ImageModern = imageModern;
            ContentModern = contentModern;
            ProductContent = productContent;
        }

        public void Update(string videoUrl, string content, string link, string imageClassic, string contentClassic, string imageModern, string contentModern, string productContent)
        {
            VideoUrl = videoUrl;
            Content = content;
            Link = link;
            ImageClassic = imageClassic;
            ContentClassic = contentClassic;
            ImageModern = imageModern;
            ContentModern = contentModern;
            ProductContent = productContent;
        }
    }
}
