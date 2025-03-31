using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using Microsoft.AspNetCore.Identity;

namespace SportsStore
{
	public class Startup
	{

		public Startup(IConfiguration config)
		{
			Configuration = config;
		}

		private IConfiguration Configuration { get; set; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews(); // настраивает совместно используемые объекты, требующиеся в приложениях, которые эксплуатируют MVC frw механизм визуализации Razor

			services.AddDbContext<StoreDbContext>(opts => // Конфигурация EFR
			{
				opts.UseSqlServer(
					Configuration["ConnectionStrings:SportsStoreConnection"]);
			});
			services.AddScoped<IStoreRepository, EFStoreRepository>(); // Cлужба хранилища, в которой каждый HTTP-запрос получает собственный объект хранилища
			services.AddScoped<IOrderRepository, EFOrderRepository>();

			services.AddRazorPages(); // настраивает службы, используемые инфраструктурой RazorPages

			services.AddDistributedMemoryCache(); // настраивает хранилище данных в памяти
			services.AddSession(); // регистрирует службы, используемые для доступа к данным сеанса

			services.AddScoped<Cart>(sp => SessionCart.GetCart(sp)); // Для удовлетворения запросов к экземплярам Cart должен применяться один и тот же объект
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Всегда должен применяться один и тот же объект, чтобы в классе SessionCart получать доступ к текущему сеансу
			
			services.AddServerSideBlazor();

			services.AddDbContext<AppIdentityDbContext>(options => // Конфигурация EFR
			{
				options.UseSqlServer(
					Configuration["ConnectionStrings:IdentityConnection"]);
			});
			services.AddIdentity<IdentityUser, IdentityRole>() // Устанавливаются службы Identity c исп. встроенных классов для представления пользователей и ролей
				.AddEntityFrameworkStores<AppIdentityDbContext>();
		}
		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsProduction())
				app.UseExceptionHandler("/error");
			else
			{
				app.UseDeveloperExceptionPage();  // Добавляется до других middleware, чтобы перехватывать исключения и показывать детали, если в режиме разработки.
				app.UseStatusCodePages(); // Добавляет обработку кодов состояний.
			}

			app.UseStaticFiles(); // Поддержка для обслуживания статического содержимого из папки wwwroot

			app.UseSession(); // Система сеансов автоматически ассоциирует запросы с сеансами, когда они поступают от клиента

			app.UseRouting();// Создает точки маршрутизации на основе входящих запросов и определяет, какой контроллер и действие должны быть вызваны.

			app.UseAuthentication(); // Установка компонентов, которые будут внедрять политику безопасности
			app.UseAuthorization();

			// Здесь происходит сопоставление маршрутов с действиями контроллеров и генерация ответа на основе маршрутизации и авторизации;
			//это делается после настройки всех middleware, связанных с обработкой запросов.
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { Controller = "Home", action = "Index" });
				endpoints.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });
				endpoints.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });
				endpoints.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index", productPage = 1 });
				endpoints.MapDefaultControllerRoute();
				endpoints.MapRazorPages(); // Регистрирует Razor Pages в качестве конечной точки, которую URL может применять для обработки запросов
				endpoints.MapBlazorHub(); // Регистрирует компоненты промежуточного ПО Blazor
				endpoints.MapFallbackToPage("/admin/{*cathcall}", "/Admin/Index"); // Для совершенствования системы маршрутизации
			});

			SeedData.EnsurePopulated(app);
			IdentitySeedData.EnsurePopulated(app);
		}
	}
}
