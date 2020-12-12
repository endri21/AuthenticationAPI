
using CoreApiRegister.Features.Companies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiRegister.Features.Companies
{
    public interface ICompaniesService
    {
        public Task<CreateCompanyRequestModel> Create(CreateCompanyRequestModel vm);
        public Task<BoolUpdateResponseModel> UpdateCompanyById(UpdateCompanyRequestModel vm);
        public Task<BoolDeleteResponseModel> DeleteCompanyById(int id, string userid);
        public Task<IEnumerable<CompanyListingServiceModel>> GetCompanyByUserId(string userId);
        public Task<CompanyDetailsServiceModel> GetDetailsById(int id, string userId);
    }
}
