using DotNetEnv;
using finshark_api.Data;
using finshark_api.Interfaces;
using finshark_api.Repository;
using Microsoft.EntityFrameworkCore;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    $"server={Environment.GetEnvironmentVariable("DB_HOST")};" +
    $"port={Environment.GetEnvironmentVariable("DB_PORT")};" +
    $"database={Environment.GetEnvironmentVariable("DB_NAME")};" +
    $"user={Environment.GetEnvironmentVariable("DB_USER")};" +
    $"password={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 26)));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
