using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using ProductService.Data;
using ProductService.Repositories;
using ProductService.RepositoryContracts;
using ProductService.ServiceContracts.IProducts;
using ProductService.ServiceContracts.IProductsCategories;
using ProductService.ServiceContracts.IProductsProductTypes;
using ProductService.Services.Products;
using ProductService.Services.ProductsCategories;
using ProductService.Services.ProductsCategories.ProductTypes;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//builder.WebHost.UseUrls("http://*:80", "https://*:443");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
}); ;

builder.Services.AddOcelot();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});


builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IProductTypesRepository, ProductTypesRepository>();
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

builder.Services.AddScoped<IProductTypesAdderService, ProductTypesAdderService>();
builder.Services.AddScoped<IProductTypesGetterService, ProductTypesGetterService>();
builder.Services.AddScoped<IProductTypesDeleterService, ProductTypesDeleterService>();
builder.Services.AddScoped<IProductTypesUpdaterService, ProductTypesUpdaterService>();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map attribute-routed controllers
});

app.UseOcelot().Wait();


app.Run();
