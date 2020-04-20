using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMF.Common.Auth;
using OMF.Common.Commands;
using OMF.Common.Commands.User;
using OMF.Common.RabbitMq;
using OMF.CustomerManagement.Auth;
using OMF.CustomerManagement.Domain.Repositories;
using OMF.CustomerManagement.Domain.Services;
using OMF.CustomerManagement.Handlers;
using OMF.CustomerManagement.Services;

namespace OMF.CustomerManagement
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
            services.AddJwt(Configuration);
            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddScoped<ICommandHandler<UpdateUser>, UpdateUserHandler>();
            services.AddScoped<ICommandHandler<DeleteUser>, DeleteUserHandler>();
            services.AddScoped<IEncrypter, Encrypter>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.Configure<FacebookAuthSettings>(Configuration.GetSection("FacebookAuthSettings"));
           
            services.AddDbContext<OMFIdentityDbContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:IdentityDb"]));           
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
