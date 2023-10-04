using ABPTestTask.DB;
using ABPTestTask.DB.Migrations;
using FluentMigrator.Runner;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton(new DeviceRepository(connectionString));
builder.Services.AddSingleton(new ExperimentRepository(connectionString));

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(config => config.AddSqlServer()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(InitMigration).Assembly)
        .For.All()).AddLogging(config => config.AddFluentMigratorConsole());

builder.Services.AddControllersWithViews();
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
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// using var scope = app.Services.CreateScope();
// var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
// migrationService.MigrateUp();

app.Run();