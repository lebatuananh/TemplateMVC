using QHomeGroup.Infrastructure.SharedKernel;

namespace QHomeGroup.Data.Entities.Introduce
{
    public class IntroduceConfig : DomainEntity<string>
    {
        public string Image { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public IntroduceConfig()
        {

        }

        public IntroduceConfig(string key, string image, string content, string description)
        {
            Id = key;
            Image = image;
            Content = content;
            Description = description;
        }

        public void Update(string image, string content, string description)
        {
            Image = image;
            Content = content;
            Description = description;
        }
    }
}
