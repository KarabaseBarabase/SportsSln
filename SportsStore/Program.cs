//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SportsStore.Models;

//var builder = WebApplication.CreateBuilder(args);

//// ��������� ��������� ������
//builder.Services.AddControllersWithViews(); // ����������� ��������� ������������ �������, ����������� � �����������, ������� ������������� MVC frw �������� ������������ Razor

//builder.Services.AddDbContext<StoreDbContext>(options => // ������������ EFR
//	options.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreConnection")));
//builder.Services.AddScoped<IStoreRepository, EFStoreRepository>(); // C����� ���������, � ������� ������ HTTP-������ �������� ����������� ������ ���������
//builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();	

//builder.Services.AddRazorPages(); // ����������� ������, ������������ ��������������� RazorPages

//builder.Services.AddDistributedMemoryCache(); // ����������� ��������� ������ � ������
//builder.Services.AddSession(); // ������������ ������, ������������ ��� ������� � ������ ������

//builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp)); // ��� �������������� �������� � ����������� Cart ������ ����������� ���� � ��� �� ������
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // ������ ������ ����������� ���� � ��� �� ������, ����� � ������ SessionCart �������� ������ � �������� ������

//builder.Services.AddServerSideBlazor(); 
//var app = builder.Build();


//// �������������� HTTP �������� ��������.
//if (!app.Environment.IsDevelopment()) // ��� ��������� ������ � ��������� ������������, ����������� �������, ���� ���������� �� � ������ ����������.
//{
//	app.UseExceptionHandler("/Home/Error");
//	app.UseHsts();
//}

//app.UseDeveloperExceptionPage();  // ����������� �� ������ middleware, ����� ������������� ���������� � ���������� ������, ���� � ������ ����������.

////app.UseHttpsRedirection(); // ��������������� HTTP �� HTTPS

//app.UseStatusCodePages(); // ��������� ��������� ����� ���������.

//app.UseStaticFiles(); // ��������� ��� ������������ ������������ ����������� �� ����� wwwroot

//app.UseSession(); // ������� ������� ������������� ����������� ������� � ��������, ����� ��� ��������� �� �������

//app.UseRouting();// ������� ����� ������������� �� ������ �������� �������� � ����������, ����� ���������� � �������� ������ ���� �������.

////app.UseAuthorization();// ��������� �������� �����������, ����� ����������, ����� �� ������������ ����� �� ���������� ������������ ��������. 
////�� ������ ���� ������ ����� app.UseRouting(), �� �� ��������� ���������, ����� �������������, ��� ������ �������������� ������������ ����� �������� ������ � ����������� ��������� ������������.

//// ����� ���������� ������������� ��������� � ���������� ������������ � ��������� ������ �� ������ ������������� � �����������;
////��� �������� ����� ��������� ���� middleware, ��������� � ���������� ��������.
//app.UseEndpoints(endpoints =>
//{
//	endpoints.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { Controller = "Home", action = "Index" });
//	endpoints.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });
//	endpoints.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });
//	endpoints.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index", productPage = 1 });
//	endpoints.MapDefaultControllerRoute();
//	endpoints.MapRazorPages(); // ������������ Razor Pages � �������� �������� �����, ������� URL ����� ��������� ��� ��������� ��������
//	endpoints.MapBlazorHub(); // ������������ ���������� �������������� �� Blazor
//	endpoints.MapFallbackToPage("/admin/{*cathcall}", "/Admin/Index"); // ��� ����������������� ������� �������������
//});
//SeedData.EnsurePopulated(app); // ���������� ����� ������� ���������, ����� ���������, ��� ���� ������ ��������� ���������� �������
//app.Run(); // ��������� ���������� � �������� ������������� �������� HTTP-��������.


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

