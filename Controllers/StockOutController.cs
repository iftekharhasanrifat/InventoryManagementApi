using CoreApiResponse;
using InventoryManagementSystemApi.Interfaces.Manager;
using InventoryManagementSystemApi.Models;
using InventoryManagementSystemApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;

namespace InventoryManagementSystemApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockOutController : BaseController
    {
        IStockOutManager _stockOutManager;
        public StockOutController(IStockOutManager stockOutManager)
        {
            _stockOutManager = stockOutManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = _stockOutManager.GetAll(c => c.Item, c => c.Item.Company, c => c.Item.Category);
                if (data.Count > 0)
                {
                    return CustomResult("data loaded successfully", data, HttpStatusCode.OK);
                }
                return CustomResult("data not found", HttpStatusCode.NotFound);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpGet]

        public IActionResult ViewSales(string fromDate, string toDate)
        {
            try
            {
                var data = _stockOutManager.GetAll().Where(c => c.Date >= DateTime.ParseExact(fromDate, "d/M/yyyy", CultureInfo.InvariantCulture) && c.Date <= DateTime.ParseExact(toDate, "d/M/yyyy", CultureInfo.InvariantCulture));
                var quantityByItemId = data
                .GroupBy(item => item.ItemId)
                .Select(group => new
                {
                    ItemId = group.Key,
                    SaleQuantity = group.Sum(item => item.Quantity),
                    ItemName = _stockOutManager.GetItemNameByItemId(group.Key)
                });

                return CustomResult("Data loaded successfully.",quantityByItemId, HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        public IActionResult Save(List<CreateStockOutDto> request, string type)
        {
            try
            {
                List<StockOut> stockOuts = new List<StockOut>();
                foreach (var stock in request)
                {
                    StockOut stockOut = new StockOut();
                    stockOut.ItemId = stock.ItemId;
                    stockOut.Quantity = stock.Quantity;
                    stockOut.Type = type;
                    stockOuts.Add(stockOut);
                }
                bool isSaved = _stockOutManager.Add(stockOuts);
                if (isSaved)
                {
                    return CustomResult("record has been created", HttpStatusCode.Created);
                }
                return CustomResult("Something went wrong ", HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
