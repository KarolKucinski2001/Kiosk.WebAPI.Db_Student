using Microsoft.EntityFrameworkCore;
using Kiosk.WebAPI.Persistance;
using Kiosk.WebAPI.Db.Services;
using Kiosk.WebAPI.Db.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// rejestracja kontekstu bazy w kontenerze IoC 
var sqliteConnectionString = "Data Source=Kiosk.WebAPI.db";
builder.Services.AddDbContext<KioskDbContext>(options =>
    options.UseSqlite(sqliteConnectionString));

// rejestracja jednostki pracy w kontenerze IoC 
builder.Services.AddScoped<IKioskUnitOfWork, KioskUnitOfWork>();

// rejestracja repozytorium produktów w kontenerze IoC 
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// rejestracja serwisu produktów- w kontenerze IoC 

builder.Services.AddScoped<IProductService, ProductService>();


// rejestracja exception middleware-a w kontenerze IoC 
builder.Services.AddScoped<ExceptionMiddleware>();



// rejestracja seeder-a w kontenerze IoC 
builder.Services.AddScoped<DataSeeder>();

//... 
var app = builder.Build();
// uruchomienie seedera 
using (var scope = app.Services.CreateScope())
{
    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    dataSeeder.Seed();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// wstawienie exception middleware do potoku obs³ugi ¿¹dania 
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
