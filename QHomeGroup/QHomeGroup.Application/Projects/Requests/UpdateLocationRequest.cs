using QHomeGroup.Data.Enum;

namespace QHomeGroup.Application.Projects.Requests
{
    public class UpdateLocationRequest
    {
        public string Name { get; set; }

        public OptionProject OptionProject { get; set; }
    }
}
