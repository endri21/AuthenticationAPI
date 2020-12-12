
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiRegister.Features.Companies
{
    public interface ICompaniesService
    {
        public Task<CreateCompanyRequestModel> Create(CreateCompanyRequestModel vm);
        public Task<IEnumerable<CompanyListingResponseModel>> GetCompanyByUserId(string userId);
    }
}
