﻿using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        //private readonly ICategoryRepository _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
       
        }

        public IActionResult Index()
        {
            // IEnumerable<Category> categoryList = _context.GetAll();
           // IEnumerable<CoverType> coverTypeList = _unitOfWork.CoverType.GetAll();
            return View();
        }
 
        //Get
        public IActionResult Upsert(int? id)
        {
            Company company = new();

            if (id == null || id == 0)
            {
               // ViewBag.CategoryList = CategoryList;
               // ViewData["CoverTypeList"] = CoverTypeList;
                //  Create Product
                return View(company);
            }
            else
            {
                //Update Product
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }

           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void sendData(Company obj, IFormFile? file)
        {
            var data = obj;
            Console.WriteLine(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj,IFormFile? file)
        {
            

            if (ModelState.IsValid)
            {
              
                
                if (obj.Id==0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company created successfully..!";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Product Updated successfully..!";
                }
               
                _unitOfWork.Save();
               
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //Get
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
          
        //    var coverTypefromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
        //    if (coverTypefromDbFirst == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(coverTypefromDbFirst);
        //}


        
         
        #region  API CALLS
        [HttpGet]

        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new { data = companyList });
        }

        [HttpDelete]
        
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

           
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = " delete successfull..!" });
            // return RedirectToAction("Index");



        }

        #endregion

    }
}
