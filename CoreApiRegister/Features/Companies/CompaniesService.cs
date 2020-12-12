
using CoreApiRegister.Data;
using CoreApiRegister.Data.Models;
using CoreApiRegister.Features.Companies.Models;
using Microsoft.EntityFrameworkCore;
using System;
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


        public async Task<BoolUpdateResponseModel> UpdateCompanyById(UpdateCompanyRequestModel vm)
        {
            try
            {
                var company = await this.GetCompanyByUserAndId(vm.Id, vm.UserId);
                if (company == null)
                {
                    return new BoolUpdateResponseModel
                    {
                        Success = false,
                        Allowed = false,
                        Updated = false
                    };
                }

                company.Address = vm.Address;
                company.UrlImage = vm.UrlImage;
                await data.SaveChangesAsync();
                return new BoolUpdateResponseModel
                {
                    Success = true,
                    Allowed = true,
                    Updated = true
                };
            }
            catch (Exception)
            {
                return new BoolUpdateResponseModel
                {
                    Success = false,
                    Allowed = false,
                    Updated = false
                };
            }
        }

        public async Task<BoolDeleteResponseModel> DeleteCompanyById(int id, string userid)
        {
            try
            {
                var company = await this.GetCompanyByUserAndId(id, userid);
                if (company == null)
                {
                    return new BoolDeleteResponseModel
                    {
                        Success = false,
                        Allowed = false,
                        Deleted = false
                    };
                }
                data.Companies.Remove(company);
                await data.SaveChangesAsync();
                return new BoolDeleteResponseModel
                {
                    Success = true,
                    Allowed = true,
                    Deleted = true

                };
            }
            catch (Exception e)
            {
                return new BoolDeleteResponseModel
                {
                    Success = false,
                    Allowed = false,
                    Deleted = false
                };
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
            .Where(u => u.UserId == userId)
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



        private async Task<Company> GetCompanyByUserAndId(int id, string userId)
          => await this.data
            .Companies
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);


    }
}
