using EF.Core.Repository.Repository;
using InventoryManagementSystemApi.Context;
using InventoryManagementSystemApi.Interfaces.Repository;
using InventoryManagementSystemApi.Models;

namespace InventoryManagementSystemApi.Repository
{
    public class StockOutRepository : CommonRepository<StockOut>, IStockOutRepository
    {
        public StockOutRepository(InventoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
