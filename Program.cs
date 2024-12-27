using ApiVenta.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//codigo agregado, sirve para la conexion con MySql
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(builder.Configuration.
        GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8,0,32)))  
    );

// Add services to the container.

builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

        });

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

app.UseAuthorization();

app.MapControllers();

app.Run();
