
using PortalLayers.DAL.Interfaces;
using PortalLayers.Models;


namespace PortalLayers.DAL.Repository
{



    public class EFUnitOfWork : IUnitOfWork
    {
        private SongContext db;
        private SongRepository songRepository;
        private UserRepository userRepository;
        private GenreRepository genreRepository;

        public EFUnitOfWork(SongContext context)
        {
            db = context;
        }

        public IRepository<Users> users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Users> Users
        {
            get { 
                if (userRepository == null)                
                    userRepository = new UserRepository(db);               
                return userRepository;
            }
        }
public IRepository<Songs> Songs
        {
            get
            {
                if (songRepository == null)
                    songRepository = new SongRepository(db);
                return songRepository;
            }
        }
        public IRepository<Genres> Genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

    }
}