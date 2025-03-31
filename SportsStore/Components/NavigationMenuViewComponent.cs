using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStore.Models;

namespace SportsStore.Components
{
	public class NavigationMenuViewComponent : ViewComponent
	{
		private IStoreRepository repository;
		public NavigationMenuViewComponent(IStoreRepository repo)
		{
			repository = repo;
		}
		public IViewComponentResult Invoke()
		{
			ViewBag.SelectedCategory = RouteData?.Values["category"]; // динамически присваивается значение текущей категории, полученное через объект контекста
			return View(repository.Products
				.Select(x => x.Category)
				.Distinct()
				.OrderBy(x => x));
		}
	}
}
