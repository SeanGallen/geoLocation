using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using getVehicleLocationAPI.Model;
using getVehicleLocationAPI.Data;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace getVehicleLocationAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{

			services.AddMvc();

			Secrets connectionStr = new Secrets();
         
			var connection = connectionStr.Connection;
			services.AddDbContext<LocationContext>(options => options.UseInMemoryDatabase()); 

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "LatLong to Address API",
					Description = "An API that uses Google GeoLocation API to convert LatLong values to street addresses.",
					Version = "v1",
					TermsOfService = "None",
					Contact = new Contact { Name = "Riccardo Gabrielle", Email = "riccardo.gabrielli@accenture.com"}
                });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "getVehicleLocationAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseMvc();
		}
	}
}
