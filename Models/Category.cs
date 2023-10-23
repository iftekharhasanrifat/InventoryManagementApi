using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystemApi.Models
{
    
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter category name.")]
        public string CategoryName { get; set; }

    }
}
