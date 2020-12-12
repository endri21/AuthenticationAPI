
using CoreApiRegister.Features.Companies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiRegister.Features.Companies
{
    public interface ICompaniesService
    {
        public Task<CreateCompanyResponseModel> Create(CreateCompanyResponseModel vm);
        public Task<IEnumerable<CompanyListingServiceModel>> GetCompanyByUserId(string userId);
        public Task<CompanyDetailsServiceModel> GetDetailsById(int id, string userId);
    }
}
