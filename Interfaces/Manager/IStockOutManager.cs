using EF.Core.Repository.Interface.Manager;
using InventoryManagementSystemApi.Models;

namespace InventoryManagementSystemApi.Interfaces.Manager
{
    public interface IStockOutManager:ICommonManager<StockOut>
    {
        string GetItemNameByItemId(int id);
    }
}
