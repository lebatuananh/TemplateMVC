using System.ComponentModel.DataAnnotations;

namespace QHomeGroup.Application.Content.Dtos
{
    public class TagViewModel
    {
        public string Id { get; set; }

        [MaxLength(50)] [Required] public string Name { get; set; }

        [MaxLength(50)] [Required] public string Type { get; set; }
    }
}