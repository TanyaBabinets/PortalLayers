using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PortalLayers.Models;

namespace PortalLayers.BLL.Infrastructure
{
    

    public static class SongContextExtensions
    {
        public static void AddSongContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<SongContext>(options => options.UseSqlServer(connection));
        }
    }
}