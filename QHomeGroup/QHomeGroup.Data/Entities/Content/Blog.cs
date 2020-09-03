using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Data.Entities.System;
using QHomeGroup.Data.Enum;
using QHomeGroup.Data.Interfaces;
using QHomeGroup.Infrastructure.SharedKernel;
using QHomeGroup.Utilities.Extensions;

namespace QHomeGroup.Data.Entities.Content
{
    [Table("Blogs")]
    public class Blog : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public Blog()
        {
        }
        public Blog(string name, string thumbnail, string description, IList<BlockContent> blockContents, bool? hotFlag, Guid createdBy, Status status, string tags, SlideOption slideOption, IList<string> slideVideos, IList<string> slideImages) : this()
        {
            Name = name;
            Thumbnail = thumbnail;
            Description = description;
            Block = blockContents;
            HotFlag = hotFlag;
            CreatedBy = createdBy;
            Status = status;
            SlideOption = slideOption;
            SlideVideos = slideVideos;
            SlideImages = slideImages;
            Tags = tags;
            BlogTags = new List<BlogTag>();
            SeoAlias = Name.GetSeoTitle();
        }
        public void Update(string name, string thumbnail, string description, IList<BlockContent> blockContents, bool? hotFlag, Status status, string tags, SlideOption slideOption, IList<string> slideVideos, IList<string> slideImages)
        {
            Name = name;
            Thumbnail = thumbnail;
            Description = description;
            Block = blockContents;
            HotFlag = hotFlag;
            Status = status;
            SlideOption = slideOption;
            SlideVideos = slideVideos;
            SlideImages = slideImages;
            Tags = tags;
            BlogTags = new List<BlogTag>();
            SeoAlias = Name.GetSeoTitle();
        }
        [Required] [MaxLength(256)] public string Name { set; get; }
        [MaxLength(256)] public string Thumbnail { set; get; }
        [MaxLength(500)] public string Description { set; get; }
        public string BlockContents { get; set; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        [MaxLength(256)] public string SeoPageTitle { set; get; }
        [MaxLength(256)] public string SeoAlias { set; get; }
        [MaxLength(256)] public string SeoKeywords { set; get; }
        [MaxLength(256)] public string SeoDescription { set; get; }
        public Guid CreatedBy { get; set; }
        public virtual AppUser AppUser { get; set; }
        public Status Status { set; get; }
        public SlideOption SlideOption { get; set; }
        public string SlideVideoContents { get; set; }
        public string Tags { get; set; }

        public virtual ICollection<BlogTag> BlogTags { set; get; }
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
                        item.Text="Mặc định";
                    }
                }
                BlockContents = JsonConvert.SerializeObject(value);
            }
        }
    }
}