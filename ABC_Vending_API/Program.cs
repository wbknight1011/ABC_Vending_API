using ABC_Vending_API.Data;
using ABC_Vending_API.Contexts;
using ABC_Vending_API.Repos;
using ABC_Vending_API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddScoped<IWarehouesRepository, WarehouseRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddDbContext<OrganizationContext>(o =>
	{
		o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
	});
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "AllowLocalHosts",
					  policy =>
					  {
						  policy.WithOrigins("http://localhost:4200", "https://localhost:4200", "https://localhost:7195")
						  .AllowAnyHeader()
						  .AllowAnyMethod();
					  });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
};

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowLocalHosts");

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	try
	{
		var context = services.GetRequiredService<OrganizationContext>();
		DbInitializer.Initialize(context);
	}
	catch (Exception ex)
	{
		var logger = services.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An error occurred creating DB.");
	}
}

app.Run();