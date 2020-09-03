using System;

namespace QHomeGroup.Data.Entities.Projects
{
    public class BlockContent
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public BlockType BlockType { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public ImagePosition ImagePosition { get; set; }
    }
}
