using Newtonsoft.Json;
using QHomeGroup.Infrastructure.SharedKernel;
using QHomeGroup.Utilities.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QHomeGroup.Data.Entities.Projects
{
    [Table("Services")]
    public class Service : DomainEntity<int>
    {
        public string Name { get; set; }

        public string VideoUrl { get; set; }

        public string SlideImageContents { get; set; }

        [NotMapped]
        public virtual IList<string> SlideImages
        {
            get => string.IsNullOrEmpty(SlideImageContents) ? default : JsonConvert.DeserializeObject<IList<string>>(SlideImageContents);
            set => SlideImageContents = JsonConvert.SerializeObject(value);
        }

        public string BlockContents { get; set; }

        [NotMapped]
        public virtual IList<BlockContent> Block
        {
            get => string.IsNullOrEmpty(BlockContents) ? default : JsonConvert.DeserializeObject<IList<BlockContent>>(BlockContents);
            set
            {
                foreach (var item in value)
                {
                    if (item.Text == null)
                    {
                        item.Text = "Mặc định";
                    }
                }
                BlockContents = JsonConvert.SerializeObject(value);
            }
        }

        [StringLength(250)]
        public string SeoAlias { get; set; }

        public string Thumbnail { get; set; }

        public virtual IList<Project> Projects { get; set; }

        public Service()
        {

        }

        public Service(string name, string videoUrl, string thumbnail, IList<BlockContent> content, IList<string> slideImages)
        {
            Name = name;
            VideoUrl = videoUrl;
            SeoAlias = Name.GetSeoTitle();
            Thumbnail = thumbnail;
            Block = content;
            SlideImages = slideImages;
        }

        public void UpdateService(string name, string videoUrl, string thumbnail, IList<BlockContent> content, IList<string> slideImages)
        {

            Name = name;
            VideoUrl = videoUrl;
            SeoAlias = Name.GetSeoTitle();
            Thumbnail = thumbnail;
            Block = content;
            SlideImages = slideImages;
        }
    }
}
