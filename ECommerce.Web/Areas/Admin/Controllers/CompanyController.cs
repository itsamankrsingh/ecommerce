using ECommerce.DataAccess.Interface;
using ECommerce.Identity.Common;
using ECommerce.Models;
using ECommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = IdentityRoles.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork mUnitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
                mUnitOfWork=unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CompanyViewModel companyViewModel = new()
            {
                Company = new Company(),
            };

            if (id == null || id == 0)
            {
                //Create
                return View(companyViewModel);
            }
            else
            {
                //Update
                companyViewModel.Company = mUnitOfWork.Company.Get(u => u.Id == id);
                return View(companyViewModel);
            }
        }

        [HttpPost]
        public IActionResult Upsert(CompanyViewModel companyVM)
        {
            if (ModelState.IsValid)
            {       
                if (companyVM.Company.Id == 0)
                {
                    mUnitOfWork.Company.Add(companyVM.Company);
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    mUnitOfWork.Company.Update(companyVM.Company);
                    TempData["success"] = "Company updated successfully";
                }
                mUnitOfWork.Save();
                return RedirectToAction("Index", "Company");
            }
            else
            {
                return View(companyVM);
            }
        }


        #region  API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = mUnitOfWork.Company.GetAll();
            return Json(new { data = companies });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = mUnitOfWork.Company.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            mUnitOfWork.Company.Remove(companyToBeDeleted);
            mUnitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
