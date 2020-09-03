using AutoMapper;
using QHomeGroup.Application.Common.Contacts.Dtos;
using QHomeGroup.Application.Common.Slides.Dtos;
using QHomeGroup.Application.Content.Blogs.Dtos;
using QHomeGroup.Application.Content.Dtos;
using QHomeGroup.Application.Introduce.Dto;
using QHomeGroup.Application.Projects.Dtos;
using QHomeGroup.Application.Systems.Users.Dtos;
using QHomeGroup.Data.Entities.Content;
using QHomeGroup.Data.Entities.Introduce;
using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Data.Entities.System;

namespace QHomeGroup.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            //Content
            CreateMap<Tag, TagViewModel>();
            CreateMap<Blog, BlogDto>().MaxDepth(2);
            CreateMap<BlogTag, BlogTagDto>().MaxDepth(2);
            CreateMap<Slide, SlideDto>().MaxDepth(2);
            //System
            CreateMap<AppUser, AppUserViewModel>().MaxDepth(2);
            CreateMap<Contact, ContactViewModel>();
            CreateMap<Project, ProjectDto>();
            CreateMap<IntroduceConfig, IntroduceConfigDto>();
            CreateMap<Service, ServiceDto>();
            CreateMap<HomeConfig, HomeConfigDto>();
            CreateMap<CompanyIntro, CompanyIntroDto>();
        }
    }
}