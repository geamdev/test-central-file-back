using Microsoft.AspNetCore.Mvc;
using UserProyect.Interfaces;
using UserProyect.Models;

namespace UserProyect.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContact _contact;

    public ContactController(IContact contact)
    {
        _contact = contact;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContact()
    {
        var contact = await _contact.GetAll();
        return contact.Any() ? Ok(contact) : NoContent();
    }

    //Crear
    [HttpPost]
    [Route("Created")]
    public async Task<IActionResult> CreateContact(ContactDTO contact)
    {
        var ListContact = await _contact.GetAll();

        foreach (var item in ListContact)
        {
            if (item.Name == contact.Name)
            {
                return BadRequest("Contacto Ya Registrado");
            };
        }

        var add = await _contact.CreateNewContact(contact);
        return add ? Ok() : BadRequest("Error Contacto No Creado");
    }


    //actualizar
    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateContactData(ContactDTO contact, Guid id)
    {
        var update = await _contact.UpdateContact(contact, id);
        return update ? Ok() : BadRequest("Error Contacto No Actualizado");
    }

    //Eliminar 
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteContactData(Guid id)
    {
        var delete = await _contact.DeleteContact(id);
        return delete ? Ok() : BadRequest("Contacto no eliminado");
    }
}
