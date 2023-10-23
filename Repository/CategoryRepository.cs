using EF.Core.Repository.Interface.Repository;
using EF.Core.Repository.Repository;
using InventoryManagementSystemApi.Context;
using InventoryManagementSystemApi.Interfaces.Repository;
using InventoryManagementSystemApi.Models;

namespace InventoryManagementSystemApi.Repository
{
    public class CategoryRepository:CommonRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(InventoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
