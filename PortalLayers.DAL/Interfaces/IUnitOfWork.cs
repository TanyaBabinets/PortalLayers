
using PortalLayers.Models;

namespace PortalLayers.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Users> Users { get; }
        IRepository<Songs> Songs { get; }
        IRepository<Genres> Genres { get; }
        Task Save();
    }
}


