using System.Collections.Generic;
using System.Threading.Tasks;
using QHomeGroup.Application.Systems.Users.Dtos;
using QHomeGroup.Utilities.Dtos;

namespace QHomeGroup.Application.Systems.Users
{
    public interface IAppUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task DeleteAsync(string id);

        IList<AppUserViewModel> GetAll();

        Task<QueryResult<AppUserViewModel>> GetAllPagingAsync(string keyword, int skip, int take);

        Task<AppUserViewModel> GetByIdAsync(string id);

        Task<bool> UpdateAsync(AppUserViewModel userVm);

        Task<bool> UpdateAccount(AppUserViewModel userVm);

        Task<bool> ChangePassword(string userId, string oldPassword, string password);

        Task<bool> ResetPassword(string userId, string password);

        bool CheckPhoneNumber(string phoneNumber);

        Task<bool> CheckUpdatePhoneNumber(string phoneNumber, string userId);

        Task<bool> UpdateToken(string token, string userId);

        Task<bool> DeleteToken(string userId);
    }
}