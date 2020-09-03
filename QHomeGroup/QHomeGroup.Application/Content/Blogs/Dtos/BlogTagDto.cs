using System.ComponentModel.DataAnnotations;
using QHomeGroup.Application.Content.Dtos;

namespace QHomeGroup.Application.Content.Blogs.Dtos
{
    public class BlogTagDto
    {
        public int Id { get; set; }

        public int BlogId { set; get; }

        [MaxLength(50)] public string TagId { set; get; }

        public virtual BlogDto Blog { set; get; }

        public virtual TagViewModel Tag { set; get; }
    }
}