using BLL;
using BLL.Contracts;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Product_APi.Authorization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication("Bearer")  
//                                     .AddJwtBearer();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
           
            ValidateIssuer = true,
           
            ValidIssuer = AuthOptions.ISSUER,
            
            ValidateAudience = true,
           
            ValidAudience = AuthOptions.AUDIENCE,
            
            ValidateLifetime = true,
            
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
           
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddDbContext<ProductDbContext>(option =>
                    option.UseSqlServer(
                        builder.Configuration
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
