﻿using System.Text.Json.Serialization;
using Materias.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Materias;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
        
        services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler
            = ReferenceHandler.IgnoreCycles);// para solucionar el error de entra en bucle el sql porque hay una relacion de muchos a muchos
        
        //Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });

        //Add CORS
        services.AddCors(options =>
        {
            options.AddPolicy("CorsRule", rule =>
            {
                rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
            });
        });// para permitir que se conecte el backend con el forntend
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        
        app.UseCors("CorsRule");
        
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}