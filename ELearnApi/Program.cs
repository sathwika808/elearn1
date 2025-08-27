using ELearnApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200") // Angular's default port
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddControllers();

//dependency injection
var ConnectionString = builder.Configuration.GetConnectionString("ELearnConnectionString");

builder.Services.AddDbContext<ELearnDbContext>(options => options.UseSqlServer(ConnectionString));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 



var app = builder.Build();

app.UseCors("AllowAngular");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
