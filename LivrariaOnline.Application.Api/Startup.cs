using AutoMapper;
using LivrariaOnline.Application.Api.AutoMapper;
using LivrariaOnline.CrossCutting.IoC;
using LivrariaOnline.Domain.Entities;
using LivrariaOnline.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace LivrariaOnline.Application.Api
{
    public class Startup
    {
        private readonly ILogger _logger;


        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Add CORS  
            services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            #endregion        

            services.AddAutoMapper();
            // Registering Mappings automatically only works if the 
            // Automapper Profile classes are in ASP.NET project
            AutoMapperConfig.RegisterMappings();

            // .NET Native DI Abstraction
            RegisterServices(services);            
            services.AddDbContext<BibliotecaOnlineContext>(opt => opt.UseInMemoryDatabase(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                _logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseCors("Cors");
            app.UseAuthentication();
            app.UseMvc();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<BibliotecaOnlineContext>();
                dbContext.Set<Book>().Add(new Book("978-8573023510", @"Luis Fernando Verissimo", @"Comédias para Se Ler na Escola", 10.00m, new DateTime(2019, 01, 01), @"ImagesBooks/ComediasParaSeLerNaEscola.jpg"));
                dbContext.Set<Book>().Add(new Book("978-8501112514", @"Colleen Hoover", @"É Assim Que Acaba", 28.99m, new DateTime(2018, 01, 18), @"ImagesBooks/EAssimQueAcaba.jpg"));
                dbContext.Set<Book>().Add(new Book("978-8573255119", @"John Bunyan", @"O Peregrino", 26.93m, new DateTime(2019, 01, 01), @"ImagesBooks/Operegrino.jpg"));
                dbContext.Set<Book>().Add(new Book("978-8543106113", @"Mitch Albom", @"As cinco pessoas que você encontra no céu", 27.96m, new DateTime(2018, 04, 09), @"ImagesBooks/AsCincoPessoasQueVoceEncontraNoCeu.jpg"));
                dbContext.Set<Book>().Add(new Book("978-8537809662", @"L. Frank Baum", @"O Mágico de Oz", 19.90m, new DateTime(2013, 02, 14), @"ImagesBooks/OMagicoDeOz.jpg"));
                dbContext.Set<Book>().Add(new Book("978-8575422397", @"T. Harv Eker", @"Os segredos da mente milionária", 19.99m, new DateTime(1992, 08, 01), @"ImagesBooks/OsSegredosDaMenteMilionaria.jpg"));
                dbContext.Set<Book>().Add(new Book("978-8550801483", @"Robert Kiyosaki", @"Pai rico, pai pobre 20 anos", 54.90m, new DateTime(2017, 08, 16), @"ImagesBooks/PaiRicoPaiPobre.jpg"));
                dbContext.Set<Book>().Add(new Book("978-8504018028", @"Dale Carnegie", @"Como Fazer Amigos e Influenciar Pessoas", 38.94m, new DateTime(2012, 05, 18), @"ImagesBooks/ComoFazerAmigos_e_InfluenciarPessoas.jpg"));
                dbContext.Set<Book>().Add(new Book("978-8576766308", @"Napoleon Hill", @"Quem Pensa Enriquece", 30.90m, new DateTime(2015, 06, 29), @"ImagesBooks/QuemPensaEnriquece.jpg"));

                dbContext.SaveChangesAsync();
            }
        }
    }
}
