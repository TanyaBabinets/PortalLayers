
using PortalLayers.DAL.Interfaces;
using PortalLayers.BLL.Infrastructure;
using PortalLayers.BLL.Interfaces;
using AutoMapper;
using PortalLayers.DAL.Interfaces;
using PortalLayers.BLL.DTO;
using PortalLayers.Models;
using PortalLayers.BLL.DTO.Interfaces;

namespace PortalLayers.BLL.Services
{
    public class GenreService : IGenreService
    {
        IUnitOfWork Database { get; set; }

        public GenreService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateGenre(GenreDTO genreDto)
        {
            var genre = new Genres
            {
                Id = genreDto.Id,
               name = genreDto.name,
               
            };
            await Database.Genres.Create(genre);
            await Database.Save();
        }

        public async Task UpdateGenre(GenreDTO genreDto)
        {
            var genre = new Genres
            {
                Id = genreDto.Id,
                name = genreDto.name,
                
            };
            Database.Genres.Update(genre);
            await Database.Save();
        }

        public async Task DeleteGenre(int id)
        {
            await Database.Genres.Delete(id);
            await Database.Save();
        }

        public async Task<GenreDTO> GetGenre(int id)
        {
            var genre = await Database.Genres.GetById(id);
            if (genre == null)
                throw new ValidationException("Нет такого жанра!", "");
            return new GenreDTO
            {
                Id = genre.Id,
                name = genre.name,
               
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.
        public async Task<IEnumerable<GenreDTO>> GetGenres()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Genres, GenreDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Genres>, IEnumerable<GenreDTO>>(await Database.Genres.ToList());
        }

       

       

       
    }
}
