﻿using AutoMapper;
using BloggingApp.Data;
using BloggingApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BloggingApp
{
	public class Startup
	{
		private const string DbConnectionString = "Server=(local);Integrated Security=SSPI;" +
			"Initial Catalog=BlogDB";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAutoMapper();
			services.AddMvc(setupAction => setupAction.ReturnHttpNotAcceptable = true);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Title = "Blogging API",
					Version = "v1",
					Description = "A proof-of-concept implementation of the unit of work and repository patterns without an ORM",
					TermsOfService = "None",
					Contact = new Contact { Name = "Jeremy Beck", Email = "", Url = "" },
					License = new License { Name = "Use under LICX", Url = "https://example.com/license" }
				});
			});

			// Add application services.
			services.AddScoped<IBlogAppContext>(s => new BlogAppSqlServerContext(DbConnectionString));
			services.AddTransient<IBlogRepository, BlogRepository>();
			services.AddTransient<IBlogService, BlogService>();
			services.AddTransient<IPostRepository, PostRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if ( env.IsDevelopment() )
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler(appBuilder =>
			   {
				   appBuilder.Run(async context =>
				   {
					   context.Response.StatusCode = 500;
					   await context.Response.WriteAsync("An unexpected fault occurred.");
				   });
			   });
			}

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blogging API V1");
			});

			app.UseMvc();
		}
	}
}
