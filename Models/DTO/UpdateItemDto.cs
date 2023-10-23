namespace InventoryManagementSystemApi.Models.DTO
{
    public class UpdateItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int ReorderLevel { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
        public int Quantity { get; set; }
    }
}
