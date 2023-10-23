using EF.Core.Repository.Repository;
using InventoryManagementSystemApi.Context;
using InventoryManagementSystemApi.Interfaces.Repository;
using InventoryManagementSystemApi.Models;

namespace InventoryManagementSystemApi.Repository
{
    public class ItemRepository:CommonRepository<Item>,IItemRepository
    {
        public ItemRepository(InventoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
