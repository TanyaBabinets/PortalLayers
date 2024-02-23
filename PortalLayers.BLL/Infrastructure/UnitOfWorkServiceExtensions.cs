using Microsoft.Extensions.DependencyInjection;
using PortalLayers.DAL.Interfaces;
using PortalLayers.DAL.Repository;


namespace PortalLayers.BLL.Infrastructure
{
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
    }
}




   
