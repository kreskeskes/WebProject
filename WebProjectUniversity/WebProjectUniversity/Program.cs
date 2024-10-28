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
    client.BaseAddress = new Uri("http://host.docker.internal:5000/"); // Change to your correct API URL
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignore SSL errors (for development only!)
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map attribute-routed controllers
});


app.Run();