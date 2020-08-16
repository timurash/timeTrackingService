using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BLL.AutoMapperProfiles;
using PL.AutoMapperProfiles;

namespace PL
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService> ();
            services.AddTransient<IReportService, ReportService>();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UsersContext>(options => options.UseNpgsql(connectionString));
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserDTOProfile());
                mc.AddProfile(new ReportDTOProfile());
                mc.AddProfile(new UserViewProfile());
                mc.AddProfile(new ReportViewProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAutoMapper(typeof(UserViewProfile), typeof(ReportViewProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}