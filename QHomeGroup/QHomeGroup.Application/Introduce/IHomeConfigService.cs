using QHomeGroup.Application.Introduce.Dto;
using QHomeGroup.Application.Introduce.Request;
using System.Threading.Tasks;

namespace QHomeGroup.Application.Introduce
{
    public interface IHomeConfigService
    {
        Task<HomeConfigDto> Get();

        Task UpdateHomeConfig(HomeConfigRequest request);
    }
}
