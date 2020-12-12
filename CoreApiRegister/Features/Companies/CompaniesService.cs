
using CoreApiRegister.Data;
using CoreApiRegister.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
                    UserId = company.UserId,
                    Id = company.Id
                };

            }
            catch
            {
                return new CreateCompanyRequestModel();
            }
        }

        public async Task<IEnumerable<CompanyListingResponseModel>> GetCompanyByUserId(string userId)
           => await this.data
                .Companies
                .Where(a => a.UserId == userId)
                .Select(f => new CompanyListingResponseModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    ImageUrl = f.UrlImage
                }).ToListAsync();
        
    }
}
