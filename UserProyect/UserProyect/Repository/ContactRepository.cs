using Dapper;
using Microsoft.Data.SqlClient;
using UserProyect.Interfaces;
using UserProyect.Models;

namespace UserProyect.Repository;

public class ContactRepository : IContact
{
    private readonly IConfiguration _configuration;

    private readonly string connectionString;

    public ContactRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetConnectionString("SqlServer");
    }

    public async Task<IEnumerable<Contact>> GetAll()
    {
        string sql = @"SELECT * fROM Contact WHERE IsActive != 1";

        using (var con = new SqlConnection(connectionString))
            return await con.QueryAsync<Contact>(sql);
    }


    public async Task<bool> CreateNewContact(ContactDTO contact)
    {
        string sql = @"INSERT INTO Contact (Name, Email, Phone, Biography) VALUES
            (@Name, @Email, @Phone, @Biography);";

        var param = new DynamicParameters();
        param.Add("Name", contact.Name);
        param.Add("Email", contact.Email);
        param.Add("Phone", contact.Phone);
        param.Add("Biography", contact.Biography);

        using (var con = new SqlConnection(connectionString))
            return await con.ExecuteAsync(sql, param) > 0;
    }

    public async Task<bool> UpdateContact(ContactDTO contact, Guid id)
    {
        string sql = @"UPDATE Contact SET
                      Name = @Name,
                      Email = @Email,
                      Phone = @Phone,
                      Biography = @Biography
                      Where Id = @Id and IsActive != 1";

        var param = new DynamicParameters();
        param.Add("Name", contact.Name);
        param.Add("Email", contact.Email);
        param.Add("Phone", contact.Phone);
        param.Add("Biography", contact.Biography);
        param.Add("Id", id);

        using (var con = new SqlConnection(connectionString))
            return await con.ExecuteAsync(sql, param) > 0;
    }

    public async Task<bool> DeleteContact(Guid id)
    {
        string sql = @"UPDATE Contact SET
                         IsActive = 1
                         WHERE Id = @Id";
        using (var con = new SqlConnection(connectionString))
            return await con.ExecuteAsync(sql, new { Id = id }) > 0;
    }

}
