using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystemApi.Models
{
    public class StockOut
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Item")]
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        [Required(ErrorMessage ="Quanity is required.")]
        public int Quantity { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
