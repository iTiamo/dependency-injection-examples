using dependency_injection_examples.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dependency_injection_examples
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dependency Injection Examples", Version = "v1" });
            });

            // Below we register services into the Service Container. We start with the most basic example and gradually increase in difficulty.
            // Anywhere the AddSingleton method is used, the AddTransient or AddScoped methods can also be used. The only difference is in lifetime, from most available to least:

            // Singleton : Only ONE instance can exist for the lifetime of the Application
            // Scoped : One instance is created PER request (a request starts when an endpoint is called and dies when a result is returned)
            // Transient : One instance is created PER resolution (when it is called from the Service Container)

            // 1. Registering a Singleton (with only a single method) into the Service Container
            // This example simply demonstrates how to register Services into the Service Conttainer at the most basic level

            services.AddSingleton<ConsoleService>();

            // 2. Registering a Singleton implementing an interface into the Service Container
            // Imagine we are running on a Windows system and we implement some logic to store a file
            // If we migrate to a Linux environment we could write another StorageService class implementing the IStorageService interface for storing files on Linux

            services.AddSingleton<IStorageService, WindowsStorageService>();

            // 3. Registering a Service that needs another service into the Service Container
            // Imagine we have an EmailService that we use to send emails to customers
            // We have another service called AddressBookService that manages all the email addresses of our customers
            // The EmailService depends on the AddressBookService to send emails, we need to register both services into the Service Container

            services.AddSingleton<IAddressBookService, AddressBookService>();
            services.AddSingleton<IEmailService, EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dependency Injection Examples v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
