using PortalLayers.BLL.DTO;

namespace PortalLayers.BLL.DTO.Interfaces
{
    public interface IGenreService
    {
        Task CreateGenre(GenreDTO genreDto);
        Task UpdateGenre(GenreDTO genreDto);
        Task DeleteGenre(int id);
        Task<GenreDTO> GetGenre(int id);
        Task<IEnumerable<GenreDTO>> GetGenres();
    }
}
