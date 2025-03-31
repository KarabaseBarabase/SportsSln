using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Controllers;
using Xunit;
using Moq;
using SportsStore.Models.ViewModels;


namespace SportsStore.Tests
{
	public class HomeControllerTests
	{
		[Fact]
		public void Can_Use_Repository() // Тестирование того, что контроллер корректно обращается в хранилище
		{
			//Организация
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>(); //создание имитированного хранилища
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductID =1, Name = "P1"},
				new Product {ProductID =2, Name = "P2" } }).AsQueryable<Product>());
			HomeController controller = new HomeController(mock.Object); //внедрение в конструктор класса HomeController имитированного хранилища
		    //Действие
			ProductsListViewModel result = controller.Index(null).ViewData.Model as ProductsListViewModel;
			//IEnumerable<Product> result = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Product>; // вызов метода Index для получения ответа, который содержит список товаров 	
			//Утверждение																								 
			Product[] prodArray = result.Products.ToArray();
			// полученные объекты Product понадобится сравнить с объектами, ожидаемыми от тестовых данных в имитированной реализации
			Assert.True(prodArray.Length == 2);
			Assert.Equal("P1", prodArray[0].Name);
			Assert.Equal("P2", prodArray[1].Name);
		}
		[Fact]
		public void Can_Paginate()
		{
			//Организация
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductID =1, Name = "P1"},
				new Product {ProductID =2, Name = "P2" },
				new Product {ProductID =3, Name = "P3" },
				new Product {ProductID =4, Name = "P4" },
				new Product {ProductID =5, Name = "P5" }
			}).AsQueryable<Product>());
			HomeController controller = new HomeController(mock.Object);
			controller.PageSize = 3;
			//Действие
			ProductsListViewModel result = controller.Index(null,2).ViewData.Model as ProductsListViewModel;
			//Утверждение
			Product[] prodArray = result.Products.ToArray();
			Assert.True(prodArray.Length == 2);
			Assert.Equal("P4", prodArray[0].Name);
			Assert.Equal("P5", prodArray[1].Name);
		}
		[Fact]
		public void Can_Send_Pagination_View_Model()
		{
			//Организация
			Mock<IStoreRepository> mock = new Mock<IStoreRepository> ();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductID =1,Name = "P1" },
				new Product {ProductID =2,Name = "P2" },
				new Product {ProductID =3,Name = "P3" },
				new Product {ProductID =4,Name = "P4" },
				new Product {ProductID =5,Name = "P5" } 
			}).AsQueryable<Product>());
			HomeController controller = new HomeController(mock.Object) { PageSize = 3 };
			//Действие
			ProductsListViewModel result = controller.Index(null,2).ViewData.Model as ProductsListViewModel;
			//Утверждение
			PagingInfo pageInfo = result.PagingInfo;
			Assert.Equal(2, pageInfo.CurrentPage);
			Assert.Equal(3, pageInfo.ItemsPerPage);
			Assert.Equal(5, pageInfo.TotalItems);
			Assert.Equal(2, pageInfo.TotalPages);
		}
		[Fact]
		public void Can_Filter_Products()
		{
			//Организация - создание имитированного хранилища
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductID =1,Name = "P1", Category = "Cat1" },
				new Product {ProductID =2,Name = "P2", Category = "Cat2" },
				new Product {ProductID =3,Name = "P3", Category = "Cat1" },
				new Product {ProductID =4,Name = "P4", Category = "Cat2" },
				new Product {ProductID =5,Name = "P5", Category = "Cat3" }
			}).AsQueryable<Product>());
			HomeController controller = new HomeController(mock.Object);
			controller.PageSize = 3;
			//Действие
			Product[] result = (controller.Index("Cat2",1).ViewData.Model as ProductsListViewModel).Products.ToArray();
			//Утверждение
			Assert.Equal(2, result.Length);
			Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
			Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
		}
		[Fact]
		public void Generate_Category_Specific_Product_Count()
		{
			// Организация
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductID =1,Name = "P1", Category = "Cat1" },
				new Product {ProductID =2,Name = "P2", Category = "Cat2" },
				new Product {ProductID =3,Name = "P3", Category = "Cat1" },
				new Product {ProductID =4,Name = "P4", Category = "Cat2" },
				new Product {ProductID =5,Name = "P5", Category = "Cat3" }
			}).AsQueryable<Product>());
			HomeController target = new HomeController(mock.Object);
			target.PageSize = 3;
			Func<ViewResult, ProductsListViewModel> GetModel = result =>
				result?.ViewData?.Model as ProductsListViewModel;
			// Действие
			int? res1 = GetModel(target.Index("Cat1"))?.PagingInfo.TotalItems;
			int? res2 = GetModel(target.Index("Cat2"))?.PagingInfo.TotalItems;
			int? res3 = GetModel(target.Index("Cat3"))?.PagingInfo.TotalItems;
			int? resAll = GetModel(target.Index(null))?.PagingInfo.TotalItems;
			// Утверждение
			Assert.Equal(2,res1);
			Assert.Equal(2, res2);
			Assert.Equal(1, res3);
			Assert.Equal(5, resAll);
		}
	}
}
