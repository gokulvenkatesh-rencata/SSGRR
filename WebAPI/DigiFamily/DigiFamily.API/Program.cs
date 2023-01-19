using DigiFamily.API.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
string? migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
string? connectionString = builder.Configuration.GetConnectionString("TechathonDatabase");
// Add services to the container.
builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ConfigurationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<PersistedGrantDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddConfigurationStore(option =>
    {
        option.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
    })
    .AddOperationalStore(option =>
    {
        option.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

DatabaseInitializer.PopulateIdentityServer(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
