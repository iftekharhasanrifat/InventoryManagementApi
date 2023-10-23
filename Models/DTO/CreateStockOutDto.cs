using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystemApi.Models.DTO
{
    public class CreateStockOutDto
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
