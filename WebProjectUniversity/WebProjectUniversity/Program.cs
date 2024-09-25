using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using WebProjectUniversity.Core.Domain.RepositoryContracts;
using WebProjectUniversity.Core.Service;
using WebProjectUniversity.Core.Service.Products;
using WebProjectUniversity.Core.Service.ProductsCategories;
using WebProjectUniversity.Core.Service.ProductsSubcategories;
using WebProjectUniversity.Core.ServiceContracts.ICategories;
using WebProjectUniversity.Core.ServiceContracts.IProducts;
using WebProjectUniversity.Core.ServiceContracts.IProductsCategories;
using WebProjectUniversity.Core.ServiceContracts.IProductsSubcategories;
using WebProjectUniversity.Infrastructure.AppDbContext;
using WebProjectUniversity.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
	options.SerializerSettings.Converters.Add(new StringEnumConverter());
});


builder.Services.AddDbContext<ApplicationDbContext>(options=>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ISubcategoriesRepository, SubcategoriesRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();


builder.Services.AddScoped<IProductsAdderService, ProductsAdderService>();
builder.Services.AddScoped<IProductsGetterService, ProductsGetterService>();
builder.Services.AddScoped<IProductsDeleterService, ProductsDeleterService>();
builder.Services.AddScoped<IProductsSorterService, ProductsSorterService>();
builder.Services.AddScoped<IProductsUpdaterService, ProductsUpdaterService>();


builder.Services.AddScoped<ICategoriesAdderService, CategoriesAdderService>();
builder.Services.AddScoped<ICategoriesGetterService, CategoriesGetterService>();
builder.Services.AddScoped<ICategoriesDeleterService, CategoriesDeleterService>();
builder.Services.AddScoped<ICategoriesUpdaterService, CategoriesUpdaterService>();

builder.Services.AddScoped<ISubcategoriesAdderService, SubcategoriesAdderService>();
builder.Services.AddScoped<ISubcategoriesGetterService, SubcategoriesGetterService>();
builder.Services.AddScoped<ISubcategoriesDeleterService, SubcategoriesDeleterService>();
builder.Services.AddScoped<ISubcategoriesUpdaterService, SubcategoriesUpdaterService>();




var app = builder.Build();


app.UseRouting();
app.UseStaticFiles();
app.MapControllers();

app.Run();
