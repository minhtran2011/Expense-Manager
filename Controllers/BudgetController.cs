using DoAn1.Areas.Identity.Data;
using DoAn1.Models;
using DoAn1.Models.View_Models;
using DoAn1.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace DoAn1.Controllers
{
	[Authorize]
	public class BudgetController : Controller
	{
		private readonly IUnitOfWork _unitofwork;
		private readonly UserManager<AppUser> _userManager;
		public BudgetController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
		{
			_unitofwork = unitOfWork;
			this._userManager = userManager;
		}
		public IActionResult Index()
		{
			List<Budget> objBudgetList = _unitofwork.Budget.GetAll(includeProperties:"Category").ToList();
			List<Budget> objNewBudgetList = new List<Budget>();
			foreach(Budget obj in objBudgetList)
			{
				if(obj.UserId == _userManager.GetUserId(this.User)){
					objNewBudgetList.Add(obj);
				}
			}
			return View(objNewBudgetList);
		}
		public IActionResult CrEdit(int? id)
		{
			BudgetVM BudgetVM = new()
			{
				CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Budget = new Budget()
			};
			if (id == null || id == 0)
			{
				//create
				return View(BudgetVM);
			}
			else
			{
				//update
				BudgetVM.Budget = _unitofwork.Budget.Get(u => u.Id == id);
				return View(BudgetVM);
			}
		}
		[HttpPost]
		public IActionResult CrEdit(BudgetVM BudgetVM)
		{
			if (ModelState.IsValid)
			{
				if (BudgetVM.Budget.Id == 0)
				{
					BudgetVM.Budget.UserId = _userManager.GetUserId(this.User);
					_unitofwork.Budget.Add(BudgetVM.Budget);
				}
				else
				{
					BudgetVM.Budget.UserId = _userManager.GetUserId(this.User);
					_unitofwork.Budget.Update(BudgetVM.Budget);
				}
				_unitofwork.Save();
				return RedirectToAction("Index");
			}
			else
			{
				BudgetVM.CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
				return View(BudgetVM);
			}
		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Budget? BudgetFromDb = _unitofwork.Budget.Get(u => u.Id == id, includeProperties:"Category");
			if (BudgetFromDb == null)
			{
				return NotFound();
			}
			return View(BudgetFromDb);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Budget? obj = _unitofwork.Budget.Get(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			_unitofwork.Budget.Remove(obj);
			_unitofwork.Save();
			return RedirectToAction("Index", "Budget");
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			List<Budget> objBudgetList = _unitofwork.Budget.GetAll(includeProperties: "Category").Where(x => x.UserId == _userManager.GetUserId(this.User)).ToList();
			return Json(new { data = objBudgetList });
		}
	}
}
