using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryManagementSystemApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter item name")]
        public string ItemName { get; set; }
        public int ReorderLevel { get; set; }

        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        public Category Category { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        
        public Company Company { get; set; }

        public int Quantity { get; set; }


    }
}
