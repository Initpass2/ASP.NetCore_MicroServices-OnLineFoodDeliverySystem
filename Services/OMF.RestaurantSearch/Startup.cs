using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMF.Common.Events;
using OMF.Common.Events.Restaurant;
using OMF.Common.RabbitMq;
using OMF.RestaurantManagement.Domain.Repositories;
using OMF.RestaurantManagement.Handler;
using OMF.RestaurantManagement.Services;

namespace OMF.RestaurantSearch
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRabbitMq(Configuration);
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IEventHandler<OrderCreated>, OrderCreatedHandler>();
            services.AddScoped<IEventHandler<OrderUpdated>, OrderUpdatedHandler>();
            services.AddScoped<IEventHandler<OrderDeleted>, OrderDeletedHandler>();
            services.AddDbContext<OMFRestaurantManagemetDbContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:OrderManagementDbContext"],
                x=>x.UseNetTopologySuite()),ServiceLifetime.Singleton);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
