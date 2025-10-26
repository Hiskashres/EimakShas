using Microsoft.EntityFrameworkCore;
using EimakShas.Data;
using EimakShas.Services;
using EimakShas.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EimakShasDb;Trusted_Connection=True;"));

// Add services to the container.
builder.Services.AddScoped<LoadShasDataService>();

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var shasService = scope.ServiceProvider.GetRequiredService<LoadShasDataService>();
//    await shasService.InsertShasDataServiceAsync();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


    app.UseHttpsRedirection();

    app.UseCors("AllowAngularApp");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
