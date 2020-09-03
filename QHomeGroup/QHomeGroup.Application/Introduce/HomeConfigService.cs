using QHomeGroup.Application.Introduce.Dto;
using QHomeGroup.Application.Introduce.Request;
using QHomeGroup.Data.Entities.Introduce;
using QHomeGroup.Infrastructure.Interfaces;
using QHomeGroup.Utilities.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace QHomeGroup.Application.Introduce
{
    public class HomeConfigService : IHomeConfigService
    {
        private readonly IRepository<HomeConfig, int> _homeConfigRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HomeConfigService(IRepository<HomeConfig, int> homeConfigRepository, IUnitOfWork unitOfWork)
        {
            _homeConfigRepository = homeConfigRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<HomeConfigDto> Get()
        {
            var config = (await _homeConfigRepository.GetAllAsync()).First();
            return config.To<HomeConfigDto>();
        }

        public async Task UpdateHomeConfig(HomeConfigRequest request)
        {
            var config = (await _homeConfigRepository.GetAllAsync()).First();
            config.Update(request.VideoUrl, request.Content, request.Link, request.ImageClassic, request.ContentClassic, request.ImageModern, request.ContentModern, request.ProductContent);
            _homeConfigRepository.Update(config);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
