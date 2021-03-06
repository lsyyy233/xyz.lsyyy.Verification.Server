using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System;
using System.IO;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Services;

namespace xyz.lsyyy.Verification
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
			ConfigurationOptions configurationOptions = new ConfigurationOptions
			{
				EndPoints = {"192.168.202.128:6379"},
				Password = "qwerty123456"
			};
			services.AddScoped<TokenService>();
			services.AddDistributedMemoryCache();
			services.AddStackExchangeRedisCache(option =>
			{
				
				option.ConfigurationOptions = configurationOptions;
			});
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
				endpoints.MapGrpcService<GrpcActionService>();
				endpoints.MapGrpcService<GrpcDepartmentService>();
				endpoints.MapGrpcService<GrpcPositionService>();
				endpoints.MapGrpcService<GrpcUserService>();
				endpoints.MapGrpcService<GrpcVerificationService>();
			});
		}
	}
}
