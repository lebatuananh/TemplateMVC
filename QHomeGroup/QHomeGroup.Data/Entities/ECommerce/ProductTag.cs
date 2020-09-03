using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QHomeGroup.Data.Entities.Content;
using QHomeGroup.Infrastructure.SharedKernel;

namespace QHomeGroup.Data.Entities.ECommerce
{
    [Table("ProductTags")]
    public class ProductTag : DomainEntity<int>
    {
        public int ProductId { get; set; }

        [StringLength(50)] public string TagId { set; get; }

        [ForeignKey("ProductId")] public virtual Product Product { set; get; }

        [ForeignKey("TagId")] public virtual Tag Tag { set; get; }
    }
}