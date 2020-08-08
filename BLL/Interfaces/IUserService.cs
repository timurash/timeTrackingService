using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO userDTO);
        void UpdateUser(UserDTO userDTO);
        void DeleteUser(int? userID);
        IEnumerable<UserDTO> GetUsers();
        void Dispose();
    }
}
