using LivrariaOnline.Domain.Interfaces.Repositories;
using LivrariaOnline.Domain.Interfaces.Services;
using LivrariaOnline.Domain.Services;
using LivrariaOnline.Infra.Data.Context;
using LivrariaOnline.Infra.Data.Repositories;
using LivrariaOnline.Infra.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LivrariaOnline.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Services dependency
            services.AddScoped<IBookService, BookService>();            

            // Infra-Data dependency
            services.AddScoped<IBookRepository, BookRepository>();            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Instance Context
            services.AddSingleton<DbContext, BibliotecaOnlineContext>();
            services.AddSingleton<BibliotecaOnlineContext>();
        }
    }
}
