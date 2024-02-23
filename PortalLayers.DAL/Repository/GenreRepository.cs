    using Microsoft.EntityFrameworkCore;
    using PortalLayers.DAL.Interfaces;
    using PortalLayers.Models;
   

    namespace PortalLayers.DAL.Repository
    {
        public class GenreRepository : IRepository<Genres>
        {
            private SongContext db;

            public GenreRepository(SongContext context)
            {
                this.db = context;
            }

            public async Task<IEnumerable<Genres>> ToList()
            {
                return await db.genres.ToListAsync();
            }

            public async Task<Genres> GetById(int id)
            {
            return await db.genres.FindAsync(id);
             
            //var genre = await db.genres.ToListAsync();
            //    Genres? g = genre?.FirstOrDefault();
            //    return g;
            }

            public async Task<Genres> GetByName(string name)
            {
                var genre = await db.genres.Where(a => a.name == name).ToListAsync();
                Genres? g = genre?.FirstOrDefault();
                return g;
            }

            public async Task Create(Genres g)
            {
                await db.genres.AddAsync(g);
            }

            public void Update(Genres g)
            {
                db.Entry(g).State = EntityState.Modified;
            }

            public async Task Delete(int id)
            {
                Genres? g = await db.genres.FindAsync(id);
                if (g != null)
                    db.genres.Remove(g);
            }
         

        }
    }


