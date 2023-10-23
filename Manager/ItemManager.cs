using EF.Core.Repository.Manager;
using InventoryManagementSystemApi.Context;
using InventoryManagementSystemApi.Interfaces.Manager;
using InventoryManagementSystemApi.Models;
using InventoryManagementSystemApi.Repository;
using System.Collections;

namespace InventoryManagementSystemApi.Manager
{
    public class ItemManager:CommonManager<Item>,IItemManager
    {
        public ItemManager(InventoryDbContext _dbContext) : base(new ItemRepository(_dbContext))
        {
            
        }

        public Item GetItemByItemId(int itemId)
        {
            var item = GetFirstOrDefault(item => item.Id == itemId);
            if (item != null)
            {
                return item;
            }
            return null;
        }

        public ICollection GetItemsByCategoryId(int categoryId)
        {
            var item = Get(c => c.CategoryId == categoryId,c=>c.Company,x=>x.Category).ToList();
            return item;
        }
        public ICollection GetItemsByCompanyId(int companyId)
        {
            var item = Get(c => c.CompanyId == companyId, c => c.Company, x => x.Category).ToList();
            return item;
        }

        public int GetQuantityByItemId(int itemId)
        {
            var item = GetItemByItemId(itemId);
            int quantity = -1;
            if (item != null)
            {
                quantity = item.Quantity;
            }
            return quantity;
        }

        public bool IsSameItemexist(int companyId, int categoryId,string name)
        {
            var data = GetAll().Where(item => item.CategoryId == categoryId && item.CompanyId == companyId)
                           .Select(item => item.ItemName.ToLower())
                           .ToList();
            if (data.Contains(name.ToLower()))
            {
                return true;
            }
            return false; 
        }
    }
}
