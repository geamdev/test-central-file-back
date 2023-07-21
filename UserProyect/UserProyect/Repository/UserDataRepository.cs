using Dapper;
using Microsoft.Data.SqlClient;
using UserProyect.Interfaces;
using UserProyect.Models;

namespace UserProyect.Repository;

public class UserDataRepository : IUserData
{

    private readonly IConfiguration _configuration;

    private readonly string connectionString;

    public UserDataRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetConnectionString("SqlServer");
    }


    public async Task<IEnumerable<UserData>> GetAll()
    {
        string sql = @" select UserData.Id, UserData.Name, UserData.Email, UserData.Password, UserData.IdProfile, ProfileData.Name as ProfileName from UserData 
                        inner join ProfileData on ProfileData.id = UserData.IdProfile WHERE UserData.IsActive != 1";

        using (var con = new SqlConnection(connectionString))
            return await con.QueryAsync<UserData>(sql);
    }

    public async Task<bool> CreateNewUser(UserDataDTO userData)
    {
        string sql = @"INSERT INTO UserData (Name, Email, Password, IdProfile) VALUES
            (@Name, @Email, @Password, @IdProfile);";

        var param = new DynamicParameters();
        param.Add("Name", userData.Name);
        param.Add("Email", userData.Email);
        param.Add("Password", userData.Password);
        param.Add("IdProfile", userData.IdProfile);

        using (var con = new SqlConnection(connectionString))
            return await con.ExecuteAsync(sql, param) > 0;
    }

    public async Task<bool> UpdateUser(UserDataDTO userData, Guid id)
    {
        string sql = @"UPDATE UserData SET
                      Name = @Name,
                      Email = @Email,
                      Password = @Password,
                      IdProfile = @IdProfile
                      Where Id = @Id and IsActive != 1";

        var param = new DynamicParameters();
        param.Add("Name", userData.Name);
        param.Add("Email", userData.Email);
        param.Add("Password", userData.Password);
        param.Add("IdProfile", userData.IdProfile);
        param.Add("Id", id);

        using (var con = new SqlConnection(connectionString))
            return await con.ExecuteAsync(sql, param) > 0;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        string sql = @"UPDATE UserData SET
                         IsActive = 1
                         WHERE Id = @Id";
        using (var con = new SqlConnection(connectionString))
            return await con.ExecuteAsync(sql, new { Id = id }) > 0;
    }
}
 


