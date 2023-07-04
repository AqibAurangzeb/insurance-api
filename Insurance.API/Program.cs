using Insurance.DataAccess;
using Insurance.Domain.Abstractions;
using Insurance.Service;
using Microsoft.EntityFrameworkCore;

namespace Insurance.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IClaimsService, ClaimsService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();

            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IClaimsRepository, ClaimsRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


            builder.Services.AddDbContext<InsuranceDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}