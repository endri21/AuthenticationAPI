
using CoreApiRegister.Data;
using CoreApiRegister.Data.Models;
using System.Threading.Tasks;

namespace CoreApiRegister.Features.Companies
{
    public class CompaniesService : ICompaniesService
    {

        private readonly ApplicationDbContext data;

        public CompaniesService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<CreateCompanyRequestModel> Create(CreateCompanyRequestModel vm)
        {
            try
            {
                var company = new Company
                {
                    Address = vm.Address,
                    Name = vm.Name,
                    UrlImage = vm.UrlImage,
                    UserId = vm.UserId
                };
                data.Add(company);
                await data.SaveChangesAsync();

                return new CreateCompanyRequestModel
                {
                    Address = company.Address,
                    Name = company.Name,
                    UrlImage = company.UrlImage,
                    UserId = company.UserId
                };

            }
            catch
            {
                return new CreateCompanyRequestModel();
            }
        }
    }
}
