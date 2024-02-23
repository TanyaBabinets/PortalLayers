
using PortalLayers.BLL.Infrastructure;
using PortalLayers.BLL.Interfaces;
using AutoMapper;
using PortalLayers.DAL.Interfaces;
using System.Numerics;
using PortalLayers.Models;
using PortalLayers.BLL.DTO;

namespace PortalLayers.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }



        public async Task CreateUser(UserDTO userDto)
        {
            var user = new Users
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Login = userDto.Login,
               Email = userDto.Email,
                Password = userDto.Password,
                Salt=userDto.Salt,
                IsActivated= userDto.IsActivated   
                         
            };
            await Database.Users.Create(user);
            await Database.Save();
        }
        public async Task UpdateUser(UserDTO userDto)
        {
            var user = new Users
            {
               
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Login = userDto.Login,
                Email = userDto.Email,
                Password = userDto.Password,
                Salt = userDto.Salt,
                IsActivated = userDto.IsActivated

            };
            Database.Users.Update(user);
            await Database.Save();
        }

        public async Task DeleteUser(int id)
        {
            await Database.Users.Delete(id);
            await Database.Save();
        }

        public async Task<UserDTO> GetById(int id)
        {
            var user = await Database.Users.GetById(id);
            if (user == null)
                throw new ValidationException("Нет такого пользователя!", "");
            return new UserDTO
            {
              
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                IsActivated = user.IsActivated

            };
        }

       
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Users, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Users>, IEnumerable<UserDTO>>(await Database.Users.ToList());
        }

     
    }
}


