using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Data.Enum;
using QHomeGroup.Infrastructure.SharedKernel;

namespace QHomeGroup.Data.Entities.Content
{
    [Table("Slides")]
    public class Slide : DomainEntity<int>
    {
        [StringLength(250)] [Required] public string Name { set; get; }
        [StringLength(250)] public string Description { set; get; }
        public SlideOption SlideOption { get; set; }
        public SlideType SlideType { get; set; }
        public string SlideVideoContents { get; set; }
        public Status Status { set; get; }

        [NotMapped]
        public virtual IList<string> SlideVideos
        {
            get => string.IsNullOrEmpty(SlideVideoContents) ? default : JsonConvert.DeserializeObject<IList<string>>(SlideVideoContents);
            set => SlideVideoContents = JsonConvert.SerializeObject(value);
        }

        public string SlideImageContents { get; set; }

        [NotMapped]
        public virtual IList<string> SlideImages
        {
            get => string.IsNullOrEmpty(SlideImageContents) ? default : JsonConvert.DeserializeObject<IList<string>>(SlideImageContents);
            set => SlideImageContents = JsonConvert.SerializeObject(value);
        }

        public Slide()
        {

        }

        public Slide(string name, SlideOption slideOption, IList<string> slideVideos, IList<string> slideImages, Status status, string description, SlideType slideType = SlideType.Home) : this()
        {
            Name = name;
            SlideOption = slideOption;
            SlideImages = slideImages;
            SlideVideos = slideVideos;
            Status = status;
            Description = description;
            SlideType = slideType;
        }

        public void Update(string name, SlideOption slideOption, IList<string> slideVideos, IList<string> slideImages, Status status, string description, SlideType slideType = SlideType.Home)
        {
            Name = name;
            SlideOption = slideOption;
            SlideVideos = slideVideos;
            SlideImages = slideImages;
            Status = status;
            Description = description;
            SlideType = slideType;
        }

    }
}