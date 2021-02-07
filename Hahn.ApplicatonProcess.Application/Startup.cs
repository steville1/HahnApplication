using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Data.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.BusinessLogic;
using Hahn.ApplicatonProcess.December2020.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace Hahn.ApplicatonProcess.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddDbContext<HahnApplicationDBContext>(opt => opt.UseInMemoryDatabase("HahnDb"));
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Starts Add Cors

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:6075/")
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod();
                                  });
            });
            //Ends Add Cors

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicatonProcess.Application", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicatonProcess.Application v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Starts Add Core 
            //app.UseCors(MyAllowSpecificOrigins);
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            //Ends Add Cors

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
