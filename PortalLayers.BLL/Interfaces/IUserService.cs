using PortalLayers.BLL.DTO;


namespace PortalLayers.BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDTO userDto);
        Task UpdateUser(UserDTO userDto);
        Task DeleteUser(int id);
        Task<UserDTO> GetById(int id);
        Task<IEnumerable<UserDTO>> GetUsers();
    }
}
