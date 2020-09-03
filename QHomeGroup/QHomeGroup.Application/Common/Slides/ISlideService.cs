using System.Collections.Generic;
using System.Threading.Tasks;
using QHomeGroup.Application.Common.Slides.Dtos;
using QHomeGroup.Application.Common.Slides.Request;
using QHomeGroup.Application.Common.Slides.ViewModels;
using QHomeGroup.Data.Entities.Content;
using QHomeGroup.Utilities.Dtos;

namespace QHomeGroup.Application.Common.Slides
{
    public interface ISlideService
    {
        Task Add(UpdateSlideRequest updateSlideRequest);
        Task Update(int id, UpdateSlideRequest updateSlideRequest);
        Task<bool> Delete(int id);
        Task<SlideDto> Get(int id);
        Task<List<SlideViewModel>> GetAll(SlideType type);
        Task<QueryResult<SlideDto>> GetAllPaging(string keyword, int skip, int take);
        Task SaveChangesAsync();

    }
}