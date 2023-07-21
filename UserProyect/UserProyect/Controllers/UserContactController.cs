using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using UserProyect.Interfaces;
using UserProyect.Models;

namespace UserProyect.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserContactController : ControllerBase
{
    private readonly IUserContact _userContact;
    private readonly IUserData _userData;

    public UserContactController(IUserContact userContact, IUserData userData)
    {
        _userContact = userContact;
        _userData = userData;
    }


    //Crear
    [HttpPost]
    [Route("Created")]
    public async Task<IActionResult> CreateUserContact(UserContact userContact)
    {
        var add = await _userContact.CreateNewContact(userContact);
        return add ? Ok() : BadRequest("Error Contacto No Asignado");
    }
    
    [HttpGet]
    [Route("ByUser/{id}")]
    public async Task<IActionResult> GetAllContactByUser(Guid id)
    {
        var contactUser = await _userContact.GetAllContactByUser(id);
        return contactUser.Any() ? Ok(contactUser) : NoContent();
    }

    [HttpPut]
    [Route("Login")]
    public async Task<IActionResult> GetVerifyUser(Login login)
    {
        var UserLogin = await _userData.GetAll();

        foreach (var item in UserLogin)
        {
            if (item.Email == login.Email && item.Password == login.Password)
            {
                return Ok(item);
            };
        }
        return BadRequest("Error Usuario No Registrado");
    }
}
