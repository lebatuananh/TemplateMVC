using Newtonsoft.Json;
using QHomeGroup.Data.Entities.System;
using QHomeGroup.Data.Enum;
using QHomeGroup.Data.Interfaces;
using QHomeGroup.Infrastructure.SharedKernel;
using QHomeGroup.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace QHomeGroup.Data.Entities.Projects
{
    [Table("Projects")]
    public class Project : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        [StringLength(255)]
        [Required]
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public OptionProject OptionProject { get; set; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public string SeoPageTitle { set; get; }
        [StringLength(255)] public string SeoAlias { set; get; }
        [StringLength(255)] public string SeoKeywords { set; get; }
        [StringLength(255)] public string SeoDescription { set; get; }
        public Status Status { set; get; }
        public Guid CreatedBy { get; set; }
        public virtual AppUser AppUser { get; set; }
        public SlideOption SlideOption { get; set; }
        public string SlideVideoContents { get; set; }
        public string BlockContents { get; set; }

        [NotMapped]
        public virtual IList<ProjectBlockContent> Block
        {
            get => string.IsNullOrEmpty(BlockContents) ? default : JsonConvert.DeserializeObject<IList<ProjectBlockContent>>(BlockContents);
            set
            {
                //foreach (var item in value)
                //{
                //    if (item.Text == null)
                //    {
                //        item.Text = "Mặc định";
                //    }
                //}
                BlockContents = JsonConvert.SerializeObject(value);
            }
        }

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
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public Project()
        {

        }
        public Project(string name, string description, string thumbnail, IList<ProjectBlockContent> blockContents, OptionProject optionProject, Status status, int serviceId, Guid createdBy, SlideOption option, IList<string> slideVideos, IList<string> slideImages) : this()
        {
            Name = name;
            Thumbnail = thumbnail;
            Block = blockContents;
            OptionProject = optionProject;
            Status = status;
            ServiceId = serviceId;
            SeoAlias = Name.GetSeoTitle();
            SeoPageTitle = Name;
            SeoDescription = Name;
            CreatedBy = createdBy;
            SlideOption = option;
            SlideVideos = slideVideos;
            SlideImages = slideImages;
            Description = description;
        }
        public void Update(string name, string description, string thumbnail, IList<ProjectBlockContent> blockContents, OptionProject optionProject, Status status, int serviceId, SlideOption option, IList<string> slideVideos, IList<string> slideImages)
        {
            Name = name;
            Thumbnail = thumbnail;
            Block = blockContents;
            OptionProject = optionProject;
            Status = status;
            ServiceId = serviceId;
            SeoAlias = Name.GetSeoTitle();
            SeoPageTitle = Name;
            SeoDescription = Name;
            SlideOption = option;
            SlideVideos = slideVideos;
            SlideImages = slideImages;
            Description = description;
        }
    }
}
