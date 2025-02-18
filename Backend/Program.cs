using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Mapping;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// dodati ovu liniju za swagger
builder.Services.AddSwaggerGen();

//dodavanje konteksta baze

builder.Services.AddDbContext<BackendContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BackendContext"));
});

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });

});

//automapper

builder.Services.AddAutoMapper(typeof(BackendMappingProfile));



var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

// dodati ove dvije linije za swagger
app.UseSwagger();

app.UseSwaggerUI(o => {
    o.EnableTryItOutByDefault();
    o.ConfigObject.AdditionalItems.Add("requestSnippetsEnabled", true);
});

app.MapControllers();

app.UseCors("CorsPolicy");

//za potrebe produkcije

app.UseStaticFiles();
app.UseDefaultFiles();
app.MapFallbackToFile("index.html");

app.Run();