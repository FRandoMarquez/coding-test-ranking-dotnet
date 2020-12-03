using AutoMapper;
using Azure;
using coding_test_ranking.infrastructure.persistence;
using coding_test_ranking.infrastructure.TextAnalytics;
using coding_test_ranking.Repositories;
using coding_test_ranking.Repositories.Mapping;
using coding_test_ranking.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace coding_test_ranking
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<IAdsService, AdsService>();
            services.AddTransient<AdsMapper>();
            services.AddTransient<IAdScoreEvaluationService, AdScoreEvaluationService>();
            services.AddTransient<ISentimentAnalysisService, IdealistaSentimentAnalysisService>();
            //services.AddTransient<ISentimentAnalysisService, AzureSentimentAnalysisService>();
            services.AddAutoMapper(typeof(AdsMapper));
            services.AddScoped<IAdsRepository, AdsRepository>();
            services.AddSingleton<IPersistence, InMemoryPersistence>();
            services.AddSwaggerGen();

            services.Configure<AzureTextAnalyticsSettings>(options =>
            {
                options.Key
                   = new AzureKeyCredential(Configuration.GetSection("AzureTextAnalytics").GetValue<string>("TextAnalyticsKey"));
                options.EndPoint
                    = new Uri(Configuration.GetSection("AzureTextAnalytics").GetValue<string>("EndPoint"));
            });
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
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
