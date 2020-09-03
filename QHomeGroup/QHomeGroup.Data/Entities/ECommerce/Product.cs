using QHomeGroup.Data.Enum;
using QHomeGroup.Data.Interfaces;
using QHomeGroup.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QHomeGroup.Data.Entities.ECommerce
{
    [Table("Products")]
    public class Product : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public Product()
        {
            ProductTags = new List<ProductTag>();
        }

        [StringLength(255)] [Required] public string Name { get; set; }
        [StringLength(255)] public string Image { get; set; }
        [StringLength(255)] public string Description { get; set; }
        [StringLength(255)] public string CompanyName { get; set; }
        public string Content { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        [StringLength(255)] public string Tags { get; set; }
        public virtual ICollection<ProductTag> ProductTags { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public string SeoPageTitle { set; get; }
        [StringLength(255)] public string SeoAlias { set; get; }
        [StringLength(255)] public string SeoKeywords { set; get; }
        [StringLength(255)] public string SeoDescription { set; get; }
        public Status Status { set; get; }
        public TypeProject TypeProject { set; get; }
        public OptionProject OptionProject { set; get; }
    }
}