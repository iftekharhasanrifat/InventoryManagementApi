using EF.Core.Repository.Interface.Manager;
using InventoryManagementSystemApi.Models;
using System.Collections;

namespace InventoryManagementSystemApi.Interfaces.Manager
{
    public interface IItemManager:ICommonManager<Item>
    {
        ICollection GetItemsByCompanyId(int companyId);
        ICollection GetItemsByCategoryId(int categoryId);
        bool IsSameItemexist(int companyId, int categoryId, string name);

        Item GetItemByItemId(int itemId);
        int GetQuantityByItemId(int itemId);
    }
}
