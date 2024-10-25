using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Origine Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Ajouter le service DbContext avec la chaîne de connexion à MariaDB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(11, 5, 2)))); // Spécifie la version de MariaDB

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services and repositories
//builder.Services.AddSingleton<IProductRepository, JsonProductRepository>();
builder.Services.AddScoped<IProductRepositoryDb, DbProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper(typeof(ProductProfile));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAngularApp");
}

//app.UseHttpsRedirection();
//app.UseAuthorization();

app.MapControllers();

app.Run();