using BLL;
using BLL.Contracts;
using DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var d = builder.Configuration.GetConnectionString("DefoultConnection");
// Add services to the container.
builder.Services.AddDbContext<ProductDbContext>(option =>
                    option
                        .UseSqlServer(builder.Configuration
                            .GetConnectionString("DefoultConnection")));
builder.Services.AddControllers();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
