using Microsoft.EntityFrameworkCore;
using NastyaKupcovakt_42_21.Database;
using System.Reflection.PortableExecutable;
//using NastyaKupcovakt_42_21.ServiceExtensions;
using NLog;
using NLog.Web;

using static NastyaKupcovakt_42_21.ServiceExtensions.ServiceExtensions;
using Microsoft.AspNetCore.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //var builder = WebApplication.CreateBuilder(args);

    // �������� ������������
    var configuration = builder.Configuration;

    // ����������� DbContext
    builder.Services.AddDbContext<StudentDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddServices();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

   // app.UseMiddleware<>();ExceptionHandlerMiddleware
   // app.UseMiddleware<ExceptionHandlerMiddleware>(); //4 ���� �����������

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
}
finally
{
    LogManager.Shutdown();
}