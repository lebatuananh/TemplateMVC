using System;
using Microsoft.EntityFrameworkCore;
using QHomeGroup.Application.Common.Slides.Dtos;
using QHomeGroup.Application.Common.Slides.Request;
using QHomeGroup.Data.Entities.Content;
using QHomeGroup.Infrastructure.Interfaces;
using QHomeGroup.Utilities.Dtos;
using QHomeGroup.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QHomeGroup.Application.Common.Slides.ViewModels;
using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Utilities.Helpers;

namespace QHomeGroup.Application.Common.Slides
{
    public class SlideService : ISlideService
    {
        private readonly IRepository<Slide, int> _slideRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SlideService(IRepository<Slide, int> slideRepository, IUnitOfWork unitOfWork)
        {
            _slideRepository = slideRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(UpdateSlideRequest updateSlideRequest)
        {
            var slide = new Slide(updateSlideRequest.Name, updateSlideRequest.SlideOption, updateSlideRequest.SlideVideos, updateSlideRequest.SlideImages, updateSlideRequest.Status, updateSlideRequest.Description, updateSlideRequest.SlideType);
            _slideRepository.Add(slide);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(int id, UpdateSlideRequest updateSlideRequest)
        {
            try
            {
                var slide = await _slideRepository.FindByIdAsync(id);
                if (slide != null)
                {
                    slide.Update(updateSlideRequest.Name, updateSlideRequest.SlideOption, updateSlideRequest.SlideVideos, updateSlideRequest.SlideImages, updateSlideRequest.Status, updateSlideRequest.Description, updateSlideRequest.SlideType);
                    _slideRepository.Update(slide);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

            }
        }

        public async Task<bool> Delete(int id)
        {
            var slide = await _slideRepository.FindByIdAsync(id);
            if (slide != null)
            {
                _slideRepository.Remove(slide);
                return true;
            }
            return false;
        }

        public async Task<SlideDto> Get(int id)
        {
            var slide = await _slideRepository.FindByIdAsync(id);
            var slideResult = slide.To<SlideDto>();
            return slideResult;
        }

        public async Task<List<SlideViewModel>> GetAll(SlideType type)
        {
            var listSlideViewModel = new List<SlideViewModel>();
            var result = await _slideRepository.GetManyAsync(s => s.SlideType == type);
            foreach (var item in result)
            {
                if (item.SlideOption == SlideOption.Image)
                {
                    foreach (var itemI in item.SlideImages)
                    {
                        listSlideViewModel.Add(new SlideViewModel()
                        {
                            SlideOption = item.SlideOption,
                            Status = item.Status,
                            Description = item.Description,
                            Link = itemI,
                            Name = item.Name
                        });
                    }
                }
                else if (item.SlideOption == SlideOption.Video)
                {
                    foreach (var itemI in item.SlideVideos)
                    {
                        listSlideViewModel.Add(new SlideViewModel()
                        {
                            SlideOption = item.SlideOption,
                            Status = item.Status,
                            Description = item.Description,
                            Link = itemI,
                            Name = item.Name
                        });
                    }
                }
            }
            return listSlideViewModel;
        }

        public async Task<QueryResult<SlideDto>> GetAllPaging(string keyword, int skip, int take)
        {
            var queryResult = await _slideRepository.QueryAsync(t => string.IsNullOrEmpty(keyword) || EF.Functions.Like(t.Name, $"%{keyword}%"), skip, take);
            return queryResult.To<QueryResult<SlideDto>>();
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}