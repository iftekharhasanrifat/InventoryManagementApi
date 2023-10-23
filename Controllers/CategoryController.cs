using CoreApiResponse;
using InventoryManagementSystemApi.Context;
using InventoryManagementSystemApi.Interfaces.Manager;
using InventoryManagementSystemApi.Manager;
using InventoryManagementSystemApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InventoryManagementSystemApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        //InventoryDbContext _dbContext;
        //CategoryManager categoryManager;
        //public CategoryController(InventoryDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //    categoryManager = new CategoryManager(_dbContext);
        //}

        ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var categories = _categoryManager.GetAll().ToList();
                return CustomResult("Data loaded Successfully", categories, HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message,HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        public IActionResult Save(Category category) 
        {
            try
            {
                bool isCategoryExist = _categoryManager.isCategoryExist(category.CategoryName);
                if (!isCategoryExist)
                {
                    bool isSaved = _categoryManager.Add(category);
                    if (isSaved)
                    {
                        return CustomResult("Data Saved Successfully", category, HttpStatusCode.Created);
                    }
                    return CustomResult("Something went wrong!", HttpStatusCode.BadRequest);
                }
                return CustomResult("Category name must be unique.", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpPut]
        public IActionResult Update(int id,Category category)
        {
            try
            {
                var data = _categoryManager.GetById(id);
                if (category.Id != id)
                {
                    return CustomResult("Id is missing!", HttpStatusCode.BadRequest);
                }
                if (data == null)
                {
                    return CustomResult("Data not found to be updated", HttpStatusCode.NotFound);
                }


                bool isUpdated = _categoryManager.Update(category);
                if (isUpdated)
                {
                    return CustomResult("Category has been updated successfully.",category, HttpStatusCode.OK);
                }
                return CustomResult("Something went wrong!", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var data = _categoryManager.GetById(id);
                if (data == null)
                {
                    return CustomResult("Data not found to be deleted", HttpStatusCode.NotFound);
                }
                bool isDeleted = _categoryManager.Delete(data);
                if (isDeleted)
                {
                    return CustomResult("Category has been Deleted successfully.", HttpStatusCode.OK);
                }
                return CustomResult("Something went wrong!", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

    }
}
