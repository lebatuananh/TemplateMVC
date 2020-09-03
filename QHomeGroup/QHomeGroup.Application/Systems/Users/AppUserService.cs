using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QHomeGroup.Application.Systems.Users.Dtos;
using QHomeGroup.Data.Entities.System;
using QHomeGroup.Data.Enum;
using QHomeGroup.Infrastructure.Interfaces;
using QHomeGroup.Utilities.Dtos;
using QHomeGroup.Utilities.Extensions;

namespace QHomeGroup.Application.Systems.Users
{
    public class AppUserService : IAppUserService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AppUserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<bool> AddAsync(AppUserViewModel viewModel)
        {
            var findByEmail = await _userManager.FindByEmailAsync(viewModel.Email);
            var findByUsername = await _userManager.FindByNameAsync(viewModel.UserName);
            var findByPhoneNumber =
                _userManager.Users.SingleOrDefault(n => n.PhoneNumber.Equals(viewModel.PhoneNumber));


            if (findByEmail != null || findByUsername != null || findByPhoneNumber != null) return false;


            var user = new AppUser
            {
                Gender = viewModel.Gender,
                Address = viewModel.Address,
                UserName = viewModel.UserName,
                Avatar = viewModel.Avatar,
                FullName = viewModel.FullName,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Status = viewModel.Status,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);

            if (result.Succeeded && viewModel.Roles.Count > 0)
            {
                var appUser = await _userManager.FindByNameAsync(user.UserName);

                if (appUser != null)
                    await _userManager.AddToRolesAsync(appUser, viewModel.Roles);
            }

            return true;
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

        public IList<AppUserViewModel> GetAll()
        {
            return _userManager.Users.To<IList<AppUserViewModel>>();
        }

        public async Task<QueryResult<AppUserViewModel>> GetAllPagingAsync(string keyword, int skip, int take)
        {
            var queryable = _userManager.Users.Where(t => string.IsNullOrEmpty(keyword) || EF.Functions.Like(t.UserName, $"%{keyword}%") || EF.Functions.Like(t.Email, $"%{keyword}%") || EF.Functions.Like(t.FullName, $"%{keyword}%"));
            return (await queryable.ToQueryResultAsync(skip, take)).To<QueryResult<AppUserViewModel>>();
        }

        public async Task<AppUserViewModel> GetByIdAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var userRole = await _userManager.GetRolesAsync(user);
                var model = user.To<AppUserViewModel>();
                model.Roles = userRole.ToList();
                return model;
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AppUserViewModel userVm)
        {
            var user = await _userManager.FindByIdAsync(userVm.Id.ToString());
            var findByEmail = await _userManager.FindByEmailAsync(userVm.Email);
            var findByPhoneNumber = _userManager.Users.SingleOrDefault(n => n.PhoneNumber.Equals(userVm.PhoneNumber));


            if (!user.Email.Equals(userVm.Email) && findByEmail != null) return false;


            if (string.IsNullOrEmpty(user.PhoneNumber) && findByPhoneNumber != null)
                return false;
            if (findByPhoneNumber != null && !user.PhoneNumber.Equals(userVm.PhoneNumber)) return false;

            //remove current roles in db
            var currentRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, userVm.Roles.Except(currentRoles).ToArray());

            if (result.Succeeded)
            {
                string[] needRemoveRoles = currentRoles.Except(userVm.Roles).ToArray();
                await _userManager.RemoveFromRolesAsync(user, needRemoveRoles);

                //Update user detail
                user.Gender = userVm.Gender;
                user.Address = userVm.Address;
                user.FullName = userVm.FullName;
                user.Email = userVm.Email;
                user.Status = userVm.Status;
                user.PhoneNumber = userVm.PhoneNumber;
                user.DateModified = DateTime.Now;
                await _userManager.UpdateAsync(user);

                if (user.Status == Status.InActive) await _userManager.UpdateSecurityStampAsync(user);

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAccount(AppUserViewModel userVm)
        {
            var user = await _userManager.FindByIdAsync(userVm.Id.ToString());
            var findByEmail = await _userManager.FindByEmailAsync(userVm.Email);

            if (!user.Email.Equals(userVm.Email) && findByEmail != null)
                return false;

            //Update user detail
            user.Gender = userVm.Gender;
            user.Address = userVm.Address;
            user.FullName = userVm.FullName;
            user.Email = userVm.Email;
            user.Status = userVm.Status;
            user.PhoneNumber = userVm.PhoneNumber;
            user.DateModified = DateTime.Now;
            user.Avatar = userVm.Avatar;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> ChangePassword(string userId, string oldPassword, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var checkPassword = await _userManager.CheckPasswordAsync(user, oldPassword);

            if (checkPassword == false) return false;

            await _userManager.ChangePasswordAsync(user, oldPassword, password);

            await _userManager.UpdateSecurityStampAsync(user);

            return true;
        }

        public async Task<bool> ResetPassword(string userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || string.IsNullOrEmpty(password)) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, password);

            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                return true;
            }

            return false;
        }

        public bool CheckPhoneNumber(string phoneNumber)
        {
            var user = _userManager.Users.SingleOrDefault(n => n.PhoneNumber.Equals(phoneNumber));

            if (user != null) return false;

            return true;
        }

        public async Task<bool> CheckUpdatePhoneNumber(string phoneNumber, string userId)
        {
            var findByPhone = _userManager.Users.SingleOrDefault(n => n.PhoneNumber.Equals(phoneNumber));

            var user = await _userManager.FindByIdAsync(userId);

            if (string.IsNullOrEmpty(phoneNumber) && user != null) return false;

            if (user.PhoneNumber.Equals(phoneNumber) && findByPhone != null) return true;

            return false;
        }

        public async Task<bool> UpdateToken(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                if (user.Token != token)
                {
                    user.UpdateToken(token);
                    await _userManager.UpdateAsync(user);
                }
            }
            return false;
        }

        public async Task<bool> DeleteToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.DeleteToken();
                await _userManager.UpdateAsync(user);
                return true;
            }
            return false;
        }
    }
}