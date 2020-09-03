using QHomeGroup.Data.Enum;

namespace QHomeGroup.Application.Projects.Dtos
{
    public class LocationDto
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public OptionProject OptionProject { get; set; }
    }
}
