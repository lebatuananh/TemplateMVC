using AutoMapper;
using QHomeGroup.Application.Systems.Users.Dtos;
using QHomeGroup.Data.Entities.System;
using System;

namespace QHomeGroup.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AppUserViewModel, AppUser>()
                .ConstructUsing(c => new AppUser(c.Id.GetValueOrDefault(Guid.Empty), c.FullName, c.UserName,
                    c.Email, c.PhoneNumber, c.Avatar, c.Status, c.BirthDay, c.Gender));
        }
    }
}