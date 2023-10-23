using EF.Core.Repository.Interface.Manager;
using EF.Core.Repository.Manager;
using InventoryManagementSystemApi.Context;
using InventoryManagementSystemApi.Interfaces.Manager;
using InventoryManagementSystemApi.Models;
using InventoryManagementSystemApi.Repository;

namespace InventoryManagementSystemApi.Manager
{
    public class CompanyManager:CommonManager<Company>,ICompanyManager
    {
        public CompanyManager(InventoryDbContext _dbContext) : base(new CompanyRepository(_dbContext))
        {

        }
        public Company GetById(int id)
        {
            return GetFirstOrDefault(c => c.Id == id);
        }

        public bool isCompanyExist(string name)
        {
            var category = GetFirstOrDefault(c => c.CompanyName.ToLower() == name.ToLower());
            if (category != null)
            {
                return true;
            }
            return false;
        }
    }
}
