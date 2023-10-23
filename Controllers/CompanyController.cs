using CoreApiResponse;
using InventoryManagementSystemApi.Interfaces.Manager;
using InventoryManagementSystemApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InventoryManagementSystemApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        //InventoryDbContext _dbContext;
        //CategoryManager categoryManager;
        //public CategoryController(InventoryDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //    categoryManager = new CategoryManager(_dbContext);
        //}

        ICompanyManager _companyManager;
        public CompanyController(ICompanyManager companyManager)
        {
            _companyManager = companyManager;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var companies = _companyManager.GetAll().ToList();
                return CustomResult("Data loaded Successfully", companies, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public IActionResult Save(Company company)
        {
            try
            {
                bool isCompanyExist = _companyManager.isCompanyExist(company.CompanyName);
                if (!isCompanyExist)
                {
                    bool isSaved = _companyManager.Add(company);
                    if (isSaved)
                    {
                        return CustomResult("Data Saved Successfully", company, HttpStatusCode.Created);
                    }
                    return CustomResult("Something went wrong!", HttpStatusCode.BadRequest);
                }
                return CustomResult("Company name must be unique.", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        public IActionResult Update(int id, Company company)
        {
            try
            {
                var data = _companyManager.GetById(id);
                if (company.Id != id)
                {
                    return CustomResult("Id is missing!", HttpStatusCode.BadRequest);
                }
                if (data == null)
                {
                    return CustomResult("Data not found to be updated", HttpStatusCode.NotFound);
                }

                bool isUpdated = _companyManager.Update(company);
                if (isUpdated)
                {
                    return CustomResult("Company has been updated successfully.", company, HttpStatusCode.OK);
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
                var data = _companyManager.GetById(id);
                if (data == null)
                {
                    return CustomResult("Data not found to be deleted", HttpStatusCode.NotFound);
                }
                bool isDeleted = _companyManager.Delete(data);
                if (isDeleted)
                {
                    return CustomResult("Company has been Deleted successfully.", HttpStatusCode.OK);
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
