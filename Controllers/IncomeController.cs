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
	public class IncomeController : Controller
	{
		private readonly IUnitOfWork _unitofwork;
		private readonly UserManager<AppUser> _userManager;
		public IncomeController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
		{
			_unitofwork = unitOfWork;
			this._userManager = userManager;
		}
		public IActionResult Index()
		{
			List<Income> objIncomeList = _unitofwork.Income.GetAll().ToList();
			List<Income> objNewIncomeList = new List<Income>();
			foreach(Income obj in objIncomeList)
			{
				if(obj.UserId == _userManager.GetUserId(this.User)){
					objNewIncomeList.Add(obj);
				}
			}
			return View(objNewIncomeList);
		}
		public IActionResult CrEdit(int? id)
		{
			IncomeVM IncomeVM = new()
			{
				Income = new Income()
			};
			if (id == null || id == 0)
			{
				//create
				return View(IncomeVM);
			}
			else
			{
				//update
				IncomeVM.Income = _unitofwork.Income.Get(u => u.Id == id);
				return View(IncomeVM);
			}
		}
		[HttpPost]
		public IActionResult CrEdit(IncomeVM IncomeVM)
		{
			if (ModelState.IsValid)
			{
				if (IncomeVM.Income.Id == 0)
				{
					IncomeVM.Income.UserId = _userManager.GetUserId(this.User);
					_unitofwork.Income.Add(IncomeVM.Income);
				}
				else
				{
					IncomeVM.Income.UserId = _userManager.GetUserId(this.User);
					_unitofwork.Income.Update(IncomeVM.Income);
				}
				_unitofwork.Save();
				return RedirectToAction("Index");
			}
			else
			{
				return View(IncomeVM);
			}
		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Income? IncomeFromDb = _unitofwork.Income.Get(u => u.Id == id);
			if (IncomeFromDb == null)
			{
				return NotFound();
			}
			return View(IncomeFromDb);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Income? obj = _unitofwork.Income.Get(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			_unitofwork.Income.Remove(obj);
			_unitofwork.Save();
			return RedirectToAction("Index", "Income");
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			List<Income> objIncomeList = _unitofwork.Income.GetAll().Where(x => x.UserId == _userManager.GetUserId(this.User)).ToList();
			return Json(new { data = objIncomeList });
		}
	}
}
