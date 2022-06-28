using ApiEndointTest.Data;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
// for asp.net core 3.1 (globaly)
builder.Services.AddMvc()
 .AddJsonOptions(o => {
     o.JsonSerializerOptions.
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

           });
// Add services to the container.

builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(o => o.AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
