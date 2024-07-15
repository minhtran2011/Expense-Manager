using DoAn1.Areas.Identity.Data;
using DoAn1.Data;
using DoAn1.Models;
using DoAn1.Models.View_Models;
using DoAn1.Repository;
using DoAn1.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using DbContext = DoAn1.Data.DbContext;

namespace DoAn1.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		//private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitofwork;
		private readonly UserManager<AppUser> _userManager;
		public HomeController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
		{
			_unitofwork = unitOfWork;
			this._userManager = userManager;
		}

		public IActionResult Index()
		{
			int Month = DateTime.Now.Month;
			int Year = DateTime.Now.Year;
			List<Expense> SelectedExpense = _unitofwork.Expense.GetAll().Where(u => u.Date.Month == Month).ToList();
			List<Income> SeletcedIncome = _unitofwork.Income.GetAll().Where(u => u.Date.Month == Month).ToList();
			List<Budget> SelectedBudget = _unitofwork.Budget.GetAll().Where(u => u.Date.Month == Month).ToList();

			List<Income> NewSelectedIncome = new List<Income>();

			foreach (Income obj in SeletcedIncome)
			{
				if (obj.UserId == _userManager.GetUserId(this.User))
				{
					NewSelectedIncome.Add(obj);
				}
			}

			List<Budget> NewSelectedBudget = new List<Budget>();

			foreach (Budget obj in SelectedBudget)
			{
				if (obj.UserId == _userManager.GetUserId(this.User))
				{
					NewSelectedBudget.Add(obj);
				}
			}

			List<Expense> NewSelectedExpense = new List<Expense>();

			foreach (Expense obj in SelectedExpense)
			{
				if (obj.UserId == _userManager.GetUserId(this.User))
				{
					NewSelectedExpense.Add(obj);
				}
			}

			CultureInfo culture = CultureInfo.CreateSpecificCulture("vi-VN");
			culture.NumberFormat.CurrencyNegativePattern = 1;

			int ShowIncome = NewSelectedIncome.Sum(x => x.Amount);
			ViewBag.ShowIncome = string.Format(culture, "{0:C0}", ShowIncome);

			int ShowBudget = NewSelectedBudget.Sum(x => x.Amount);
			ViewBag.ShowBudget = string.Format(culture, "{0:C0}", ShowBudget);

			int ShowExpense = NewSelectedExpense.Sum(x => x.Amount);
			ViewBag.ShowExpense = string.Format(culture, "{0:C0}", ShowExpense);

			int ShowBalance = ShowIncome - ShowExpense;
			ViewBag.ShowBalance = string.Format(culture, "{0:C0}", ShowBalance);

			var pieData = _unitofwork.Expense.GetAll(includeProperties: "Category")
				.Where(i => i.Date.Month == Month && i.UserId == _userManager.GetUserId(User))
				.GroupBy(j => j.CategoryId)
				.Select(k => new
				{
					name = k.First().Category.NameWithIcon,
					amount = k.Sum(x => x.Amount)
				}).ToList();

			List<DataPoint> dataPoints = new List<DataPoint>();

			foreach (var item in pieData)
			{
				dataPoints.Add(new DataPoint(item.name, item.amount));
			}

			ViewBag.PieDataPoints = JsonConvert.SerializeObject(dataPoints);

			var splineData1 = _unitofwork.Income.GetAll()
				.Where(i => i.Date.Year == Year && i.UserId == _userManager.GetUserId(User))
				.GroupBy(j => j.Date.Month)
				.Select(k => new
				{
					dateTime = k.First().Date.Month.ToString(),
					amount = k.Sum(x => x.Amount)
				}).OrderBy(c => c.dateTime).ToList();

			List<DataPoint> splineDataPoint1 = new List<DataPoint>();

			foreach (var item in splineData1)
			{
				splineDataPoint1.Add(new DataPoint(item.dateTime, item.amount));
			}

			ViewBag.SplineDataPoints1 = JsonConvert.SerializeObject(splineDataPoint1);

			var splineData2 = _unitofwork.Expense.GetAll()
				.Where(i => i.Date.Year == Year && i.UserId == _userManager.GetUserId(User))
				.GroupBy(j => j.Date.Month)
				.Select(k => new
				{
					dateTime = k.First().Date.Month.ToString(),
					amount = k.Sum(x => x.Amount)
				}).OrderBy(c => c.dateTime).ToList();

			List<DataPoint> splineDataPoint2 = new List<DataPoint>();

			foreach (var item in splineData2)
			{
				splineDataPoint2.Add(new DataPoint(item.dateTime, item.amount));
			}

			ViewBag.SplineDataPoints2 = JsonConvert.SerializeObject(splineDataPoint2);

			return View();
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
