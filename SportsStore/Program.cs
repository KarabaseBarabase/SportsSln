//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SportsStore.Models;

//var builder = WebApplication.CreateBuilder(args);

//// Настройка контекста данных
//builder.Services.AddControllersWithViews(); // настраивает совместно используемые объекты, требующиеся в приложениях, которые эксплуатируют MVC frw механизм визуализации Razor

//builder.Services.AddDbContext<StoreDbContext>(options => // Конфигурация EFR
//	options.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreConnection")));
//builder.Services.AddScoped<IStoreRepository, EFStoreRepository>(); // Cлужба хранилища, в которой каждый HTTP-запрос получает собственный объект хранилища
//builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();	

//builder.Services.AddRazorPages(); // настраивает службы, используемые инфраструктурой RazorPages

//builder.Services.AddDistributedMemoryCache(); // настраивает хранилище данных в памяти
//builder.Services.AddSession(); // регистрирует службы, используемые для доступа к данным сеанса

//builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp)); // Для удовлетворения запросов к экземплярам Cart должен применяться один и тот же объект
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Всегда должен применяться один и тот же объект, чтобы в классе SessionCart получать доступ к текущему сеансу

//builder.Services.AddServerSideBlazor(); 
//var app = builder.Build();


//// Конфигурируйте HTTP конвейер запросов.
//if (!app.Environment.IsDevelopment()) // Для обработки ошибок и настройки безопасности, выполняются первыми, если приложение не в режиме разработки.
//{
//	app.UseExceptionHandler("/Home/Error");
//	app.UseHsts();
//}

//app.UseDeveloperExceptionPage();  // Добавляется до других middleware, чтобы перехватывать исключения и показывать детали, если в режиме разработки.

////app.UseHttpsRedirection(); // Перенаправление HTTP на HTTPS

//app.UseStatusCodePages(); // Добавляет обработку кодов состояний.

//app.UseStaticFiles(); // Поддержка для обслуживания статического содержимого из папки wwwroot

//app.UseSession(); // Система сеансов автоматически ассоциирует запросы с сеансами, когда они поступают от клиента

//app.UseRouting();// Создает точки маршрутизации на основе входящих запросов и определяет, какой контроллер и действие должны быть вызваны.

////app.UseAuthorization();// Выполняет проверку авторизации, чтобы определить, имеет ли пользователь право на выполнение запрошенного действия. 
////Он должен быть вызван после app.UseRouting(), но до обработки маршрутов, чтобы гарантировать, что только авторизованные пользователи могут получить доступ к определённым действиям контроллеров.

//// Здесь происходит сопоставление маршрутов с действиями контроллеров и генерация ответа на основе маршрутизации и авторизации;
////это делается после настройки всех middleware, связанных с обработкой запросов.
//app.UseEndpoints(endpoints =>
//{
//	endpoints.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { Controller = "Home", action = "Index" });
//	endpoints.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });
//	endpoints.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });
//	endpoints.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index", productPage = 1 });
//	endpoints.MapDefaultControllerRoute();
//	endpoints.MapRazorPages(); // Регистрирует Razor Pages в качестве конечной точки, которую URL может применять для обработки запросов
//	endpoints.MapBlazorHub(); // Регистрирует компоненты промежуточного ПО Blazor
//	endpoints.MapFallbackToPage("/admin/{*cathcall}", "/Admin/Index"); // Для совершенствования системы маршрутизации
//});
//SeedData.EnsurePopulated(app); // Вызывается после запуска конвейера, чтобы убедиться, что база данных заполнена начальными данными
//app.Run(); // запускает приложение и начинает прослушивание входящих HTTP-запросов.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SportsStore
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}

