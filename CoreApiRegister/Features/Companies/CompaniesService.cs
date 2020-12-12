
using CoreApiRegister.Data;
using CoreApiRegister.Data.Models;
using CoreApiRegister.Features.Companies.Models;
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

        public async Task<CreateCompanyResponseModel> Create(CreateCompanyResponseModel vm)
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

                return new CreateCompanyResponseModel
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
                return new CreateCompanyResponseModel();
            }
        }

        public async Task<IEnumerable<CompanyListingServiceModel>> GetCompanyByUserId(string userId)
           => await this.data
                .Companies
                .Where(a => a.UserId == userId)
                .Select(f => new CompanyListingServiceModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    ImageUrl = f.UrlImage
                }).ToListAsync();

        public async Task<CompanyDetailsServiceModel> GetDetailsById(int id, string userId)
           => await this.data
            .Companies
            .Where(u=>u.UserId == userId)
            .Select(f => new CompanyDetailsServiceModel
            {
                Id = f.Id,
                Name = f.Name,
                Address = f.Address,
                ImageUrl = f.UrlImage,
                UserId = f.UserId,
                UserName = f.user.UserName
            })
            .FirstOrDefaultAsync(a => a.Id == id);
    }

}
