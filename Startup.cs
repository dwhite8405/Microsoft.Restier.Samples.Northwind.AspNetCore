using System;
using System.Linq;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Restier.AspNetCore;
using Microsoft.Restier.Core;
using Microsoft.Restier.Samples.Northwind.AspNet.Controllers;

namespace Microsoft.Restier.Samples.Northwind.AspNetCore
{

    /// <summary>
    /// Startup class. Configures the container and the application.
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// The application configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        string LocalhostOrigin = "_localhostOrigin";

        /// <summary>
        /// Configures the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: LocalhostOrigin,
                     policy =>
                     {
                         policy.WithOrigins("http://tygrid.com");
                     });
            });
            services.AddRestier((builder) =>
            {
                // This delegate is executed after OData is added to the container.
                // Add you replacement services here. 
                builder.AddRestierApi<NorthwindApi>(routeServices =>
                {

                    routeServices
                        .AddEFCoreProviderServices<NorthwindContext>((services, options) =>
                        {
                            options.UseSqlite(Configuration.GetConnectionString("NorthwindEntities"));
                        })
                        .AddSingleton(new ODataValidationSettings
                        {
                            //MaxTop = 5,
                            MaxAnyAllExpressionDepth = 3,
                            MaxExpansionDepth = 3,
                        });

                });
            });
            services.AddControllers(options => options.EnableEndpointRouting = false);
        }

        /// <summary>
        /// Configures the application and the HTTP Request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                // This is used when we're being deployed behind a reverse proxy, such as
                // nginx.
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            app.UseCors(LocalhostOrigin);
            app.UseAuthorization();
            app.UseClaimsPrincipals();
            app.UseRestierBatching();

            app.UseMvc(builder =>
            {
                builder.Select().Expand().Filter().OrderBy().MaxTop(100).Count().SetTimeZoneInfo(TimeZoneInfo.Utc);

                builder.MapRestier(builder =>
                {
                    if (env.IsDevelopment())
                    {
                        builder.MapApiRoute<NorthwindApi>("ApiV1", "", true);
                    } else
                    {
                        builder.MapApiRoute<NorthwindApi>("ApiV1", "o/northwind", true);
                    }
                });
            });
        }

    }

}
