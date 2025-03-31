using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
	public class HomeController : Controller
	{
		private IStoreRepository repository;
		public int PageSize = 4; // На одной странице должны отображаться сведения о 4 товарах
		public HomeController(IStoreRepository repo)
		{
			repository = repo;
		}
		public ViewResult Index(string category,int productPage = 1)
			=> View(new ProductsListViewModel
			{
				Products = repository.Products
					.Where(p => category == null || p.Category == category) // фильтрация объектов по категории
					.OrderBy(p => p.ProductID) // упорядочиваются по первичному ключу
					.Skip((productPage - 1) * PageSize) // пропускаются до начала текущей страницы
					.Take(PageSize), // выбирается кол-во товаров, указанное в PageSize
				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = PageSize,
					TotalItems = category == null ? 
						repository.Products.Count() : 
						repository.Products.Where(e => 
							e.Category == category).Count()
				},
				CurrentCategory = category
			});
	}
}


