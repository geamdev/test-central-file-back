using UserProyect.Models;

namespace UserProyect.Interfaces;

public interface IProfileData
{
    Task<IEnumerable<ProfileData>> GetAll();

    Task<bool> CreateNewProfile(ProfileDataDTO profiledata);

    Task<bool> UpdateProfile(ProfileDataDTO profiledata, Guid id);

    Task<bool> DeleteProfile(Guid id);
}
