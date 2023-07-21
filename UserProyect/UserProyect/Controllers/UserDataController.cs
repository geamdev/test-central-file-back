using Microsoft.AspNetCore.Mvc;
using UserProyect.Interfaces;
using UserProyect.Models;

namespace UserProyect.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserDataController : ControllerBase
{
    private readonly IUserData _userData;

    public UserDataController(IUserData userData)
    {
        _userData = userData;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        var users = await _userData.GetAll();
        return users.Any() ? Ok(users) : NoContent();
    }
    
    //Crear
    [HttpPost]
    [Route("Created")]
    public async Task<IActionResult> CreateUserData(UserDataDTO userData)
    {
        var ListUser = await _userData.GetAll();

        foreach (var item in ListUser)
        {
            if (item.Email == userData.Email)
            {
                return BadRequest("Correo Ya Registrado");
            };
        }

        var add = await _userData.CreateNewUser(userData);
        return add ? Ok() : BadRequest("Error Usuario No Creado");
    }

    //actualizar
    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateUserData(UserDataDTO userData, Guid id)
    {
        var update = await _userData.UpdateUser(userData, id);
        return update ? Ok() : BadRequest("Error Usuario No Actualizado");
    }

    //Eliminar
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteUserData(Guid id)
    {
        var delete = await _userData.DeleteUser(id);
        return delete ? Ok() : BadRequest("Usuario no eliminado");
    }
}

