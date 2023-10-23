using EF.Core.Repository.Interface.Manager;
using InventoryManagementSystemApi.Models;

namespace InventoryManagementSystemApi.Interfaces.Manager
{
    public interface ICompanyManager:ICommonManager<Company>
    {
        bool isCompanyExist(string name);
        Company GetById(int id);
    }
}
