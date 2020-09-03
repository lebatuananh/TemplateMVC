using QHomeGroup.Application.Introduce.Dto;
using QHomeGroup.Application.Introduce.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QHomeGroup.Application.Introduce
{
    public interface ICompanyIntroService
    {
        Task<CompanyIntroDto> Get();

        Task Update(CompanyIntroRequest request);
    }
}
