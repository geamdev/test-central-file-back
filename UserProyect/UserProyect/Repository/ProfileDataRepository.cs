using Dapper;
using Microsoft.Data.SqlClient;
using UserProyect.Interfaces;
using UserProyect.Models;

namespace UserProyect.Repository;

public class ProfileDataRepository: IProfileData
{
    private readonly IConfiguration _configuration;

    private readonly string connectionString;

    public ProfileDataRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetConnectionString("SqlServer");
    }


    public async Task<IEnumerable<ProfileData>> GetAll()
    {
        string sql = @"SELECT * fROM ProfileData WHERE IsActive != 1";

        using (var con = new SqlConnection(connectionString))
            return await con.QueryAsync<ProfileData>(sql);
    }

    public async Task<bool> CreateNewProfile(ProfileDataDTO profileData)
    {
        string sql = @"INSERT INTO ProfileData (Name) VALUES
            (@Name);";

        var param = new DynamicParameters();
        param.Add("Name", profileData.Name);

        using (var con = new SqlConnection(connectionString))
            return await con.ExecuteAsync(sql, param) > 0;
    }

    public async Task<bool> UpdateProfile(ProfileDataDTO profileData, Guid id)
    {
        string sql = @"UPDATE ProfileData SET
                      Name = @Name
                      Where Id = @Id and IsActive != 1";

        var param = new DynamicParameters();
        param.Add("Name", profileData.Name);
        param.Add("Id", id);

        using (var con = new SqlConnection(connectionString))
            return await con.ExecuteAsync(sql, param) > 0;
    }

    public async Task<bool> DeleteProfile(Guid id)
    {
        string sql = @"UPDATE ProfileData SET
                         IsActive = 1
                         WHERE Id = @Id";
        using (var con = new SqlConnection(connectionString))
            return await con.ExecuteAsync(sql, new { Id = id }) > 0;
    }
}
