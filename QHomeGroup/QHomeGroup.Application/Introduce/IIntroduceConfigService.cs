using QHomeGroup.Application.Introduce.Dto;
using QHomeGroup.Application.Introduce.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QHomeGroup.Application.Introduce
{
    public interface IIntroduceConfigService
    {
        Task<IntroduceConfigDto> GetByKey(string key);

        Task Update(string key, IntroduceConfigRequest request);

        Task<IList<IntroduceConfigDto>> GetAll();
    }
}
