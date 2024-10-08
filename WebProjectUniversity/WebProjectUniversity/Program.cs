using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Ocelot.Values;
using System.Text.Json.Serialization;

using WebProjectUniversity.UI.Clients;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
	options.SerializerSettings.Converters.Add(new StringEnumConverter());
});


builder.Services.AddHttpClient<ProductServiceClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:8081/"); // Update this URL to your ProductService API base address
});



var app = builder.Build();


app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map attribute-routed controllers
});



app.Run();