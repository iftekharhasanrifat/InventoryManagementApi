namespace InventoryManagementSystemApi.Models.DTO
{
    public class CreateItemDto
    {
        public string ItemName { get; set; }
        public int ReorderLevel { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
    }
}
