using CoreApiResponse;
using InventoryManagementSystemApi.Interfaces.Manager;
using InventoryManagementSystemApi.Manager;
using InventoryManagementSystemApi.Models;
using InventoryManagementSystemApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InventoryManagementSystemApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : BaseController
    {
        IItemManager _itemManager;
        public ItemController(IItemManager itemManager)
        {
            _itemManager = itemManager;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = _itemManager.GetAll(c => c.Category, x => x.Company).OrderBy(c=>c.Id);
                return CustomResult("Data Loaded Successfully", data, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public IActionResult GetItemsbyCategoryId(int categoryId)
        {
            try
            {
                var data = _itemManager.GetItemsByCategoryId(categoryId);
                if (data.Count > 0)
                {
                    return CustomResult("Data Loaded Successfully", data, HttpStatusCode.OK);
                }
                return CustomResult("No records found", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public IActionResult GetItemsbyCompanyId(int companyId)
        {
            try
            {
                var data = _itemManager.GetItemsByCompanyId(companyId);
                if (data.Count > 0)
                {
                    return CustomResult("Data Loaded Successfully", data, HttpStatusCode.OK);
                }
                return CustomResult("No records found", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        public IActionResult Save(CreateItemDto request)
        {
            try
            {
                bool isSameItemexist = _itemManager.IsSameItemexist(request.CompanyId, request.CategoryId, request.ItemName);

                if (isSameItemexist)
                {
                    return CustomResult("This item already exist.", HttpStatusCode.BadRequest);
                }

                var _item = new Item
                {
                    ItemName = request.ItemName,
                    CategoryId = request.CategoryId,
                    CompanyId= request.CompanyId,
                    ReorderLevel = request.ReorderLevel
                };
                
                bool isSaved = _itemManager.Add(_item);
                if (isSaved)
                {
                    return CustomResult("Data saved Successfully.", HttpStatusCode.Created);
                }
                return CustomResult("Something went wrong!.", HttpStatusCode.BadRequest);
                
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]

        public IActionResult StockIn(int itemId, int quantity)
        {
            try
            {
                if (itemId == 0)
                {
                    return CustomResult("Id is missing", HttpStatusCode.BadRequest);
                }
                if(quantity == 0)
                {
                    return CustomResult("please enter quantity", HttpStatusCode.BadRequest);
                }
                int availableQuantity = _itemManager.GetQuantityByItemId(itemId);
                if (availableQuantity == -1)
                {
                    return CustomResult("item not found to be updated.",HttpStatusCode.NotFound);
                }
                int newQuantity = availableQuantity + quantity;

                var item = _itemManager.GetItemByItemId(itemId);

                var newItem = new Item
                {
                    Id = itemId,
                    CategoryId = item.CategoryId,
                    CompanyId = item.CompanyId,
                    ItemName = item.ItemName,
                    ReorderLevel = item.ReorderLevel,
                    Quantity = newQuantity
                };

                bool isUpdated = _itemManager.Update(newItem);
                if (isUpdated)
                {
                    return CustomResult("Quanity has been updated Successfully!", HttpStatusCode.OK);
                }
                return CustomResult("Something went wrong!", HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpPut]

        public IActionResult StockOutQuantity(ICollection<UpdateItemDto> items)
        {
            try
            {
                List<Item> _items = new List<Item>();
                bool isAdded =  false;
                foreach(var data in items)
                {
                    if (data.Id == 0)
                    {
                        isAdded = false;
                    }
                    if (data.Quantity == 0)
                    {
                        isAdded = false;
                    }
                    int availableQuantity = _itemManager.GetQuantityByItemId(data.Id);
                    if (availableQuantity == -1)
                    {
                        isAdded = false;
                    }
                    int newQuantity;
                    if (data.Quantity > availableQuantity)
                    {
                        isAdded = false;
                    }

                    isAdded = true;
                    newQuantity = availableQuantity - data.Quantity;

                    var item = _itemManager.GetItemByItemId(data.Id);

                    var newItem = new Item
                    {
                        Id = data.Id,
                        CategoryId = item.CategoryId,
                        CompanyId = item.CompanyId,
                        ItemName = item.ItemName,
                        ReorderLevel = item.ReorderLevel,
                        Quantity = newQuantity
                    };
                    _items.Add(newItem);
                }
                bool isUpdated = _itemManager.Update(_items);
                if (isUpdated)
                {
                    return CustomResult("Quanity has been updated Successfully!", HttpStatusCode.OK);
                }
                return CustomResult("Something went wrong!", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpDelete]
        public IActionResult Delete(int itemId)
        {
            try
            {
                var item = _itemManager.GetItemByItemId(itemId);
                if (item != null)
                {
                    bool isDeleted = _itemManager.Delete(item);
                    if (isDeleted)
                    {
                        return CustomResult("Item is deleted successfully", HttpStatusCode.OK);
                    }
                    return CustomResult("Something went wrong!", HttpStatusCode.BadRequest);
                }
                return CustomResult("item not found to be deleted", HttpStatusCode.NotFound);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
