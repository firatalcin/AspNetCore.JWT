using AutoMapper;
using JWTApp.Back.Core.Application.Interfaces;
using JWTApp.Back.Core.Application.Mappings;
using JWTApp.Back.Persistance.Context;
using JWTApp.Back.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JWTApp.Back
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("local"));
            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssemblyContaining(typeof(Program));
            });

            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfiles(new List<Profile>()
                {
                    new ProductProfile()
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}