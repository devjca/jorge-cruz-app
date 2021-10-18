using Microsoft.Extensions.DependencyInjection;
using AspenCapitalProject.Models;
using DataAccess.Contracts;
using DataAccess;
using BsusinessLogic.Contracts;
using BsusinessLogic;

namespace AspenCapitalProject.InversionOfControl
{
    public static class IoC
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddTransient<IProjectionDataBL, ProjectionDataBL>();

            return services;
        }
    }
}
