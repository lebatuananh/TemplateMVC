using QHomeGroup.Application.Introduce.Dto;
using QHomeGroup.Application.Introduce.Request;
using QHomeGroup.Data.Entities.Introduce;
using QHomeGroup.Infrastructure.Interfaces;
using QHomeGroup.Utilities.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace QHomeGroup.Application.Introduce
{
    public class CompanyIntroService : ICompanyIntroService
    {
        private readonly IRepository<CompanyIntro, int> _companyIntroRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyIntroService(IRepository<CompanyIntro, int> companyIntroRepository, IUnitOfWork unitOfWork)
        {
            _companyIntroRepository = companyIntroRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CompanyIntroDto> Get()
        {
            var config = (await _companyIntroRepository.GetAllAsync()).First();
            return config.To<CompanyIntroDto>();
        }

        public async Task Update(CompanyIntroRequest request)
        {
            var config = (await _companyIntroRepository.GetAllAsync()).First();
            config.Update(request.Name, request.OfficeAddress, request.ShowroomAddress, request.FactoryAddress, request.Tel, request.PhoneNumber, request.Email, request.Website);
            _companyIntroRepository.Update(config);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
