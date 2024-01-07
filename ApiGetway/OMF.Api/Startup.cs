using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMF.Api.DTO.Repositories;
using OMF.Api.Handler;
using OMF.Api.Handler.Order;
using OMF.Api.Handler.Restaurant;
using OMF.Api.Handler.Review;
using OMF.Api.Handler.User;
using OMF.Api.Services;
using OMF.Common.Auth;
using OMF.Common.Events;
using OMF.Common.Events.Order;
using OMF.Common.Events.Restaurant;
using OMF.Common.Events.Review;
using OMF.Common.Events.User;
using OMF.Common.RabbitMq;

namespace OMF.Api
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
            services.AddRabbitMq(Configuration);
            services.AddOptions();
            services.Configure<UrlsConfig>(Configuration.GetSection("urls"));
            services.AddScoped<IEventHandler<UserCreated>, UserCreatedHandler>();
            services.AddScoped<IEventHandler<UserUpdated>, UserUpdatedHandler>();
            services.AddScoped<IEventHandler<UserDeleted>, UserDeletedHandler>();
            services.AddScoped<IEventHandler<ReviewCreated>, ReviewCreatedHandler>();
            services.AddScoped<IEventHandler<ReviewUpdated>, ReviewUpdatedHandler>();
            services.AddScoped<IEventHandler<ReviewDeleted>, ReviewDeletedHandler>();
            services.AddScoped<IEventHandler<ReviewRejected>, ReviewRejectedHandler>();
            services.AddScoped<IEventHandler<RestaurantCreatedOrUpdated>, RestaurantCreatedHandler>();
            services.AddScoped<IEventHandler<OrderModified>, OrderUpdatedHandler>();
            services.AddScoped<IEventHandler<OrderCanceled>, OrderDeletedHandler>();
            services.AddScoped<IEventHandler<OrderAdded>, OrderCreatedHandler>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>(); 
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddHttpClient<IRestaurantApiClient, RestaurantApiClient>();
            services.AddHttpClient<IOrderManagementApiClient, OrderManagementApiClient>();
            services.AddJwt(Configuration);
            services.AddDbContext<OMFStorageContext>(opts =>
            opts.UseSqlServer(Configuration["ConnectionString:OMFDbContext"],
            x => x.UseNetTopologySuite()));
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
