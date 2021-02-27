using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using xyz.lsyyy.Verification;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Services;

namespace xyz.lsyyyVerification
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
			services.AddRouting(x =>
			{
				x.LowercaseQueryStrings = true;
				x.LowercaseUrls = true;
			});
			services.AddControllers()
				.AddNewtonsoftJson(option =>
				{
					option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				});
			services.AddDbContext<MyDbContext>(options =>
			{
				options
					.UseLazyLoadingProxies()
					.UseMySql(Configuration.GetConnectionString("DefaultDataBase"));
			});
			services
				.AddSingleton<MemoryActionTagService>()
				.AddScoped<ActionTagService>();
			services.AddLogging();
			services.AddGrpc();
			services.AddSwaggerGen(option =>
			{
				option.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "授权管理 API",
					Description = "API for 授权管理",
					Contact = new OpenApiContact
					{
						Name = "lsyyy",
						Email = "lsyyy24698@163.com"
					}
				});
				string path = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");
				option.IncludeXmlComments(path, true);
			});
		}
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(option =>
				{
					option.SwaggerEndpoint("/swagger/v1/swagger.json","授权管理");
					option.RoutePrefix = string.Empty;
				});
			}
			else
			{
				app.UseExceptionHandler(appBuilder =>
				{
					appBuilder.Run(async context =>
					{
						context.Response.StatusCode = 500;
						await context.Response.WriteAsync("Unexpected Error");
					});
				});
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGrpcService<GrpcUserService>();
				endpoints.MapGrpcService<VerificationService>();
				endpoints.MapGrpcService<GrpcActionService>();
				endpoints.MapControllers();
			});
		}
	}
}
