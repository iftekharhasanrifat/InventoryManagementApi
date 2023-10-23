using EF.Core.Repository.Manager;
using InventoryManagementSystemApi.Context;
using InventoryManagementSystemApi.Interfaces.Manager;
using InventoryManagementSystemApi.Models;
using InventoryManagementSystemApi.Repository;

namespace InventoryManagementSystemApi.Manager
{
    public class CategoryManager:CommonManager<Category>,ICategoryManager
    {
        public CategoryManager(InventoryDbContext _dbContext) : base( new CategoryRepository(_dbContext))
        {

        }

        public Category GetById(int id)
        {
            return GetFirstOrDefault(c => c.Id == id);
        }

        public bool isCategoryExist(string name)
        {
            var category= GetFirstOrDefault(c => c.CategoryName.ToLower() == name.ToLower());
            if (category != null)
            {
                return true;
            }
            return false;
        }
    }
}
