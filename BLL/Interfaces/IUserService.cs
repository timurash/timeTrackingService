using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        ServiceResultDTO CreateUser(UserDTO userDTO);
        ServiceResultDTO UpdateUser(UserDTO userDTO);
        ServiceResultDTO DeleteUser(int? userID);
        IEnumerable<UserDTO> GetUsers();
        void Dispose();
    }
}
