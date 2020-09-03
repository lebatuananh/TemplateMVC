using Newtonsoft.Json;
using QHomeGroup.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QHomeGroup.Data.Entities.Introduce
{
    [Table("CompanyIntros")]
    public class CompanyIntro : DomainEntity<int>
    {
        public string Name { get; set; }
        public string OfficeAddress { get; set; }
        public string ShowroomAddress { get; set; }

        [NotMapped]
        public virtual IList<FactoryAddress> FactoryAddress
        {
            get => string.IsNullOrEmpty(FactoryAddressContents) ? default : JsonConvert.DeserializeObject<IList<FactoryAddress>>(FactoryAddressContents);
            set => FactoryAddressContents = JsonConvert.SerializeObject(value);
        }
        public string Tel { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string FactoryAddressContents { get; set; }

        public CompanyIntro()
        {

        }

        public CompanyIntro(string name, string officeAddress, string showroomAddress, IList<FactoryAddress> factoryAddress, string tel, string phoneNumber, string email, string website): base()
        {
            Name = name;
            OfficeAddress = officeAddress;
            ShowroomAddress = showroomAddress;
            FactoryAddress = factoryAddress;
            Tel = tel;
            PhoneNumber = phoneNumber;
            Email = email;
            Website = website;
        }

        public void Update(string name, string officeAddress, string showroomAddress, IList<FactoryAddress> factoryAddress, string tel, string phoneNumber, string email, string website)
        {
            Name = name;
            OfficeAddress = officeAddress;
            ShowroomAddress = showroomAddress;
            FactoryAddress = factoryAddress;
            Tel = tel;
            PhoneNumber = phoneNumber;
            Email = email;
            Website = website;
        }
    }
}
