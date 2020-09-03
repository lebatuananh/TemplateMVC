using QHomeGroup.Application.Introduce.Dto;
using QHomeGroup.Application.Introduce.Request;
using QHomeGroup.Data.Entities.Introduce;
using QHomeGroup.Infrastructure.Interfaces;
using QHomeGroup.Utilities.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QHomeGroup.Application.Introduce
{
    public class IntroduceConfigService : IIntroduceConfigService
    {
        private readonly IRepository<IntroduceConfig, string> _introduceConfigRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IntroduceConfigService(IRepository<IntroduceConfig, string> introduceConfigRepository, IUnitOfWork unitOfWork)
        {
            _introduceConfigRepository = introduceConfigRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<IntroduceConfigDto>> GetAll()
        {
            var configs = await _introduceConfigRepository.GetAllAsync();
            return configs.To<IList<IntroduceConfigDto>>();
        }

        public async Task<IntroduceConfigDto> GetByKey(string key)
        {
            var config = await _introduceConfigRepository.FindByIdAsync(key);
            return config.To<IntroduceConfigDto>();
        }

        public async Task Update(string key, IntroduceConfigRequest request)
        {
            var config = await _introduceConfigRepository.FindByIdAsync(key);
            if(config != null)
            {
                config.Update(request.Image, request.Content, request.Description);
                _introduceConfigRepository.Update(config);
                await _unitOfWork.SaveChangesAsync();
            }
        }

    }
}
