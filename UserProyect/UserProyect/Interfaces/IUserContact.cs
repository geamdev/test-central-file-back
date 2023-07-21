using UserProyect.Models;

namespace UserProyect.Interfaces;

public interface IUserContact
{
    Task<bool> CreateNewContact(UserContact userContact);

    Task<IEnumerable<UserContactDTO>> GetAllContactByUser(Guid id);
}
