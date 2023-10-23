using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystemApi.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter company name.")]
        public string CompanyName { get; set; }
    }
}
