
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;  
    using global::PortalLayers.DAL.Interfaces;
    using global::PortalLayers.Models;

    namespace PortalLayers.DAL.Repository
    {
        public class UserRepository : IRepository<Users>
        {
            private SongContext db;

            public UserRepository(SongContext context)
            {
                this.db = context;
            }

            public async Task<IEnumerable<Users>> ToList()
            {
                return await db.users.ToListAsync();
            }
     
            public async Task<Users> GetById(int id)
            {
            return await db.users.FindAsync(id);
           
         
        }

            public async Task<Users> GetByName(string name)
            {
                var user = await db.users.Where(a => a.Login == name).ToListAsync();
                Users? u = user?.FirstOrDefault();
                return u;
            }

            public async Task Create(Users u)
            {
                await db.users.AddAsync(u);
            }

           
            public void Update(Users u)
            {
                var existingUser = db.users.Find(u.Id);
                if (existingUser != null)
                {
                    db.Entry(existingUser).CurrentValues.SetValues(u);
                    db.SaveChanges();
                }
            }
          //  db.Entry(u).State = EntityState.Modified;
           // }

            public async Task Delete(int id)
            {
               Users? u = await db.users.FindAsync(id);
                if (u != null)
                    db.users.Remove(u);
            }

       
        }
    }


