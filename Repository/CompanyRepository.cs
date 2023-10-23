using EF.Core.Repository.Repository;
using InventoryManagementSystemApi.Context;
using InventoryManagementSystemApi.Interfaces.Repository;
using InventoryManagementSystemApi.Models;

namespace InventoryManagementSystemApi.Repository
{
    public class CompanyRepository:CommonRepository<Company>,ICompanyRepository
    {
        public CompanyRepository(InventoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
