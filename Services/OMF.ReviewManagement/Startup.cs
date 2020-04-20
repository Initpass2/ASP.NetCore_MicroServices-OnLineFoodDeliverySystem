using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMF.Common.Commands;
using OMF.Common.Commands.Review;
using OMF.Common.RabbitMq;
using OMF.ReviewManagement.Domain.Repositories;
using OMF.ReviewManagement.Handler;
using OMF.ReviewManagement.Moderator;
using OMF.ReviewManagement.Services;

namespace OMF.ReviewManagement
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
            services.AddLogging();
            services.AddRabbitMq(Configuration);
            services.AddScoped<ICommandHandler<CreateReview>, CreateReviewHandler>();
            services.AddScoped<ICommandHandler<UpdateReview>, UpdateReviewHandler>();
            services.AddScoped<ICommandHandler<DeleteReview>, DeleteReviewHandler>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewValidation, ReviewValidation>();
            services.AddScoped<IReviewServices, ReviewServices>();
            services.AddScoped<IReviewModeration, TextReviewModeration>();
            services.AddDbContext<OMFReviewDbContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:CustomerReviewDB"]));
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
