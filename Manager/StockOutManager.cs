using EF.Core.Repository.Manager;
using InventoryManagementSystemApi.Context;
using InventoryManagementSystemApi.Interfaces.Manager;
using InventoryManagementSystemApi.Models;
using InventoryManagementSystemApi.Repository;

namespace InventoryManagementSystemApi.Manager
{
    public class StockOutManager:CommonManager<StockOut>,IStockOutManager
    {
        public StockOutManager(InventoryDbContext _dbContext) : base( new StockOutRepository(_dbContext))
        {

        }

        public string GetItemNameByItemId(int id)
        {
            var itemName = GetFirstOrDefault(c => c.ItemId==id,c=>c.Item).Item.ItemName;
            return itemName;
        }
    }
}
