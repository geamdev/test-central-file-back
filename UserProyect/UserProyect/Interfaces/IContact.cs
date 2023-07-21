using UserProyect.Models;

namespace UserProyect.Interfaces;

public interface IContact
{
    Task<IEnumerable<Contact>> GetAll();

    Task<bool> CreateNewContact (ContactDTO contact);

    Task<bool> UpdateContact (ContactDTO contact, Guid id);
    
    Task<bool> DeleteContact (Guid id);
}
