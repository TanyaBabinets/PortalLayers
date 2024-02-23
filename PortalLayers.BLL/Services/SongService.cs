
using PortalLayers.BLL.Infrastructure;
using AutoMapper;
using PortalLayers.DAL.Interfaces;
using PortalLayers.Models;
using PortalLayers.BLL.DTO.Interfaces;
using PortalLayers.BLL.DTO;


namespace PortalLayers.BLL.Services
{
    public class SongService : ISongService
    {
        IUnitOfWork Database { get; set; }

        public SongService(IUnitOfWork uow)
        {
            Database = uow;
        }

   

        public async Task CreateSong(SongDTO songDto)
        {
            var song = new Songs
            {
                Id = songDto.Id,
                name = songDto.name,
                singer = songDto.singer,
                runtime = songDto.runtime,
                size = songDto.size,
                file = songDto.file,
                pic = songDto.pic,
                dateTime = DateTime.Now,
                user = await Database.Users.GetById((int)songDto.userId),             
                genre = await Database.Genres.GetById((int)songDto.genreId)               
                
            };
            await Database.Songs.Create(song);
            await Database.Save();
        }
		public async Task UpdateSong(SongDTO songDto)
		{
			var existingSong = await Database.Songs.GetById(songDto.Id);
			if (existingSong != null)
			{
				existingSong.name = songDto.name;
				existingSong.singer = songDto.singer;
				existingSong.runtime = songDto.runtime;
				existingSong.size = songDto.size;
				existingSong.dateTime = DateTime.Now;
				existingSong.user = await Database.Users.GetById((int)songDto.userId);
				existingSong.genre = await Database.Genres.GetById((int)songDto.genreId);

			
				if (songDto.file != null)
				{
					
					existingSong.file = songDto.file;
				}
				if (songDto.pic != null)
				{

					existingSong.pic = songDto.pic;
				}
				Database.Songs.Update(existingSong);
				await Database.Save();
			}
		}
	

		public async Task DeleteSong(int id)
        {
            await Database.Songs.Delete(id);
            await Database.Save();
        }

        public async Task<SongDTO> GetSong(int id)
		{
            var song = await Database.Songs.GetById(id);
            if (song == null)
                throw new ValidationException("Нет такой песни!", "");
			var mapperG = new MapperConfiguration(cfg => cfg.CreateMap<Users, UserDTO>()).CreateMapper();
			int? userId = song.user != null ? song.user.Id : null;

			return new SongDTO
            {
                Id = song.Id,
                name = song.name,
                singer = song.singer,
                runtime = song.runtime,
                size = song.size,
                file = song.file,
                pic = song.pic,
                dateTime = DateTime.Now,             
               userId = song.user?.Id,			
				user = song.user != null ? mapperG.Map<Users, UserDTO>(song.user) : null,
				genreId = song.genreId,
                genre = song.genre?.name

            };
        }
		

		public async Task<IEnumerable<SongDTO>> GetSongs()
        {
			var mapperG = new MapperConfiguration(cfg => cfg.CreateMap<Users, UserDTO>()).CreateMapper();
			var config = new MapperConfiguration(cfg => cfg.CreateMap<Songs, SongDTO>()

            .ForMember("genre", u => u.MapFrom(c => c.genre.name)).ForMember("user", u => u.MapFrom(c => mapperG.Map<Users, UserDTO>(c.user))));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Songs>, IEnumerable<SongDTO>>(await Database.Songs.ToList());
        }     
    }
}

