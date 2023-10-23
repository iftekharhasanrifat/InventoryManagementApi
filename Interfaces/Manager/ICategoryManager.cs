using EF.Core.Repository.Interface.Manager;
using InventoryManagementSystemApi.Models;

namespace InventoryManagementSystemApi.Interfaces.Manager
{
    public interface ICategoryManager:ICommonManager<Category>
    {
        bool isCategoryExist(string name);

        Category GetById(int id);
    }
}
