using Microsoft.EntityFrameworkCore;
using PortalLayers.DAL.Interfaces;
using PortalLayers.Models;

namespace PortalLayers.DAL.Repository
{
    public class SongRepository : IRepository<Songs>
    {
        private SongContext db;

        public SongRepository(SongContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Songs>> ToList()
        {
            return await db.songs.Include(o => o.genre).Include(u => u.user).ToListAsync();
        }

      
		public async Task<Songs> GetById(int id)
		{
			var song = await db.songs
				.Include(o => o.genre)
				.Include(u => u.user)
				.FirstOrDefaultAsync(a => a.Id == id);

			return song;
		}
		public async Task<Songs> GetByName(string name)
        {
            var song = await db.songs.Include(u => u.user).Where(a => a.name == name).ToListAsync();
            Songs? s = song?.FirstOrDefault();
            return s;
        }

        public async Task Create(Songs s)
        {
            await db.songs.AddAsync(s);
        }

        public void Update(Songs s)
        {
            db.Entry(s).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Songs? s = await db.songs.FindAsync(id);
            if (s != null)
                db.songs.Remove(s);
        }
       

    }
}
