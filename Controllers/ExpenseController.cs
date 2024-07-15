using DoAn1.Areas.Identity.Data;
using DoAn1.Models;
using DoAn1.Models.View_Models;
using DoAn1.Repository;
using DoAn1.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Claims;

namespace DoAn1.Controllers
{
	[Authorize]
	public class ExpenseController : Controller
	{
		private readonly IUnitOfWork _unitofwork;
		private readonly UserManager<AppUser> _userManager;
		public ExpenseController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
		{
			_unitofwork = unitOfWork;
			this._userManager = userManager;
		}
		public IActionResult Index()
		{
			List<Expense> objExpenseList = _unitofwork.Expense.GetAll(includeProperties:"Category").ToList();
			List<Expense> objNewExpenseList = new List<Expense>();
			foreach(Expense obj in objExpenseList)
			{
				if(obj.UserId == _userManager.GetUserId(this.User)){
					objNewExpenseList.Add(obj);
				}
			}
			return View(objNewExpenseList);
		}
		public IActionResult CrEdit(int? id)
		{
			ExpenseVM ExpenseVM = new()
			{
				CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Expense = new Expense()
			};
			if (id == null || id == 0)
			{
				//create
				return View(ExpenseVM);
			}
			else
			{
				//update
				ExpenseVM.Expense = _unitofwork.Expense.Get(u => u.Id == id);
				return View(ExpenseVM);
			}
		}
		[HttpPost]
		public IActionResult CrEdit(ExpenseVM ExpenseVM)
		{
			if (ModelState.IsValid)
			{
				if (ExpenseVM.Expense.Id == 0)
				{
					ExpenseVM.Expense.UserId = _userManager.GetUserId(this.User);
					_unitofwork.Expense.Add(ExpenseVM.Expense);
				}
				else
				{
					ExpenseVM.Expense.UserId = _userManager.GetUserId(this.User);
					_unitofwork.Expense.Update(ExpenseVM.Expense);
				}
				_unitofwork.Save();
				return RedirectToAction("Index");
			}
			else
			{
				ExpenseVM.CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
				return View(ExpenseVM);
			}
		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Expense? ExpenseFromDb = _unitofwork.Expense.Get(u => u.Id == id, includeProperties:"Category");
			if (ExpenseFromDb == null)
			{
				return NotFound();
			}
			return View(ExpenseFromDb);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Expense? obj = _unitofwork.Expense.Get(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			_unitofwork.Expense.Remove(obj);
			_unitofwork.Save();
			return RedirectToAction("Index", "Expense");
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			List<Expense> objExpenseList = _unitofwork.Expense.GetAll(includeProperties: "Category").Where(x=>x.UserId == _userManager.GetUserId(this.User)).ToList();
			return Json(new { data = objExpenseList });
		}
		[HttpGet]
		public IActionResult GetRecent() {
			List<Expense> objRecentExpenseList = _unitofwork.Expense.GetAll(includeProperties: "Category").Where(x => x.UserId == _userManager.GetUserId(this.User)).OrderByDescending(x=>x.Date).Take(7).ToList();
			return Json(new { data = objRecentExpenseList });
		}
	}
}
