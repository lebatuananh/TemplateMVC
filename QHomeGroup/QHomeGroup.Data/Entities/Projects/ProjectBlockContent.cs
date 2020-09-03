using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QHomeGroup.Data.Entities.Projects
{
    public class ProjectBlockContent
    {
        public Guid Id { get; set; }
        public string Images { get; set; }
        public BlockType BlockType { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        [NotMapped]
        public virtual IList<string> ImageContents
        {
            get => string.IsNullOrEmpty(Images) ? default : JsonConvert.DeserializeObject<IList<string>>(Images);
            set => Images = JsonConvert.SerializeObject(value);
        }
    }
}