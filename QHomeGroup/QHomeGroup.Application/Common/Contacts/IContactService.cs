using System.Collections.Generic;
using System.Threading.Tasks;
using QHomeGroup.Application.Common.Contacts.Dtos;
using QHomeGroup.Utilities.Dtos;

namespace QHomeGroup.Application.Common.Contacts
{
    public interface IContactService
    {
        Task Create(ContactViewModel contactVm);

        Task Delete(string id);
        Task<IList<ContactViewModel>> GetAll();

        Task<QueryResult<ContactViewModel>> GetAllPaging(string keyword, int skip, int take);
        Task<ContactViewModel> GetById(string id);

        Task<ExportContactData> Export();
    }
}