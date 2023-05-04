using Cat.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  //verifica os endpoints

builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("Test"));  //falando para a api que iremos usar o bd
builder.Services.AddScoped<DataContext, DataContext>();  

var app = builder.Build();

builder.Services.AddCors(
    options => {options.AddPolicy("AllowLocalhost", policy => {
        policy.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500")
        .SetIsOriginAllowed(isOriginAllowed: _ => true)
        .AllowAnyHeader().AllowAnyMethod();
    });
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
