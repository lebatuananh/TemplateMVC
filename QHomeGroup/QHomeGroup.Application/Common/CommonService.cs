using QHomeGroup.Data.Entities.Content;
using QHomeGroup.Infrastructure.Interfaces;

namespace QHomeGroup.Application.Common
{
    public class CommonService : ICommonService
    {
        private IRepository<Slide, int> _slideRepository;

        private IUnitOfWork _unitOfWork;

        public CommonService(IUnitOfWork unitOfWork, IRepository<Slide, int> slideRepository)
        {
            _unitOfWork = unitOfWork;
            _slideRepository = slideRepository;
        }
    }
}