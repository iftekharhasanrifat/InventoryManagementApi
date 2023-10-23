using InventoryManagementSystemApi.Context;
using InventoryManagementSystemApi.Interfaces.Manager;
using InventoryManagementSystemApi.Manager;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
// Add services to the container.
builder.Services.AddDbContext<InventoryDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryDB")));

builder.Services.AddTransient<ICategoryManager, CategoryManager>();
builder.Services.AddTransient<ICompanyManager, CompanyManager>();
builder.Services.AddTransient<IItemManager, ItemManager>();
builder.Services.AddTransient<IStockOutManager, StockOutManager>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
