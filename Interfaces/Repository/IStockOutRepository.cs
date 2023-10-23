using EF.Core.Repository.Interface.Repository;
using InventoryManagementSystemApi.Models;

namespace InventoryManagementSystemApi.Interfaces.Repository
{
    public interface IStockOutRepository:ICommonRepository<StockOut>
    {
    }
}
