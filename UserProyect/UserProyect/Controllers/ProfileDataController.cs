using Microsoft.AspNetCore.Mvc;
using UserProyect.Interfaces;
using UserProyect.Models;

namespace UserProyect.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileDataController : ControllerBase
{
    private readonly IProfileData _profileData;

    public ProfileDataController(IProfileData profileData)
    {
        _profileData = profileData;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProfile()
    {
        var profiles = await _profileData.GetAll();
        return profiles.Any() ? Ok(profiles) : NoContent();
    }

    //Crear
    [HttpPost]
    [Route("Created")]
    public async Task<IActionResult> CreateProfileData(ProfileDataDTO profileData)
    {
        var ListProfile = await _profileData.GetAll();

        foreach (var item in ListProfile)
        {
            if (item.Name == profileData.Name)
            {
                return BadRequest("Perfil Ya Registrado");
            };
        }

        var add = await _profileData.CreateNewProfile(profileData);
        return add ? Ok() : BadRequest("Error Perfil No Creado");
    }

    
    //actualizar
    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateProfileData(ProfileDataDTO profileData, Guid id)
    {
        var update = await _profileData.UpdateProfile(profileData, id);
        return update ? Ok() : BadRequest("Error Perfil No Actualizado");
    }

    //Eliminar 
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteUserData(Guid id)
    {
        var delete = await _profileData.DeleteProfile(id);
        return delete ? Ok() : BadRequest("Perfil no eliminado");
    }
}
