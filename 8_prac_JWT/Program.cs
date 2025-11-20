using _8_prac_JWT.DatabaseContext;
using _8_prac_JWT.Interfaces;
using _8_prac_JWT.Service;
using _8_prac_JWT.UniversalMethod;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ContextDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")), ServiceLifetime.Scoped);

// JWT  
builder.Services.AddSingleton<JwtGenerator>();

//HTTP
builder.Services.AddScoped<IProductInterfaces, ProductService>();
builder.Services.AddScoped<IUserInterfaces, UsersService>();

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
