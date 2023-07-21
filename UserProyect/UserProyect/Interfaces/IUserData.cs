using UserProyect.Models;

namespace UserProyect.Interfaces;

public interface IUserData
{
    Task<IEnumerable<UserData>> GetAll();

    Task<bool> CreateNewUser(UserDataDTO userData);

    Task<bool> UpdateUser(UserDataDTO userData, Guid id);

    Task<bool> DeleteUser(Guid id);
}
