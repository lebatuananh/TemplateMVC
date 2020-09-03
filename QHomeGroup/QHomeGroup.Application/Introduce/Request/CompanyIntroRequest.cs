using QHomeGroup.Data.Entities.Introduce;
using System.Collections.Generic;

namespace QHomeGroup.Application.Introduce.Request
{
    public class CompanyIntroRequest
    {
        public string Name { get; set; }
        public string OfficeAddress { get; set; }
        public string ShowroomAddress { get; set; }
        public virtual IList<FactoryAddress> FactoryAddress { get; set; }
        public string Tel { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }
}
