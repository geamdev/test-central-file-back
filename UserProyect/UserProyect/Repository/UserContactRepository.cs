using Dapper;
using Microsoft.Data.SqlClient;
using UserProyect.Interfaces;
using UserProyect.Models;

namespace UserProyect.Repository;

public class UserContactRepository : IUserContact
{
    private readonly IConfiguration _configuration;

    private readonly string connectionString;

    public UserContactRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetConnectionString("SqlServer");
    }

    public async Task<bool> CreateNewContact(UserContact userContact)
    {
        string sql = @"INSERT INTO UserContact(IdUser, IdContact) VALUES 
        (@IdUser, @IdContact);";

        var param = new DynamicParameters();
        param.Add("IdUser", userContact.IdUser);
        param.Add("IdContact", userContact.IdContact);

        using (var con = new SqlConnection(connectionString))
            return await con.ExecuteAsync(sql, param) > 0;
    }

    public async Task<IEnumerable<UserContactDTO>> GetAllContactByUser(Guid id)
    {
        string sql = @"select UserContact.IdContact, Contact.Name, Contact.Email, Contact.Phone, Contact.Biography from UserContact
                    inner join Contact on Contact.Id = UserContact.IdContact
                    Where UserContact.IdUser = @Id and Contact.IsActive != '1'";

        using (var con = new SqlConnection(connectionString))
            return await con.QueryAsync<UserContactDTO>(sql, new { Id = id });
    }
}
