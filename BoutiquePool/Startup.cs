using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unieco.Services.Email;

namespace BoutiquePool
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [System.Obsolete]

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<Models.Configurations.MongoDB.DatabaseSettings>(
                Configuration.GetSection(nameof(Models.Configurations.MongoDB.DatabaseSettings)));

            services.Configure<Models.Configurations.AWS.Credentials>(Configuration.GetSection("AWSCredentials"));
            services.Configure<Models.Configurations.AWS.S3Configuration>(Configuration.GetSection("AWSS3"));

            services.Configure<Models.Configurations.MongoDB.DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<Models.Configurations.MongoDB.DatabaseSettings>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<Models.Configurations.AWS.Credentials>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<Models.Configurations.AWS.S3Configuration>>().Value);

            services.AddControllers()
                .AddNewtonsoftJson(options => options.UseMemberCasing());

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, AuthMessageSender>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
            app.UseDeveloperExceptionPage();

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
