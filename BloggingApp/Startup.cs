﻿using BloggingApp.Data;
using BloggingApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloggingApp
{
	public class Startup
    {
		private const string DbConnectionString = "Server=(local);Integrated Security=SSPI;" +
			"Initial Catalog=EFGetStarted.AspNetCore.NewDb";

		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

			// Add application services.
			services.AddTransient<IBlogAppContext>(s => new BlogAppSqlServerContext( DbConnectionString ));
			services.AddTransient<IBlogRepository, BlogRepository>();
			services.AddTransient<IBlogService, BlogService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
