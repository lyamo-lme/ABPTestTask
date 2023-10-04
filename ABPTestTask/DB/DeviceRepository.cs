using System;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ABPTestTask.Models;

namespace ABPTestTask.DB;

public class DeviceRepository
{
    private readonly string ConnectionString;

    public DeviceRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public SqlConnection Connection
    {
        get => new SqlConnection(ConnectionString);
    }

    public async Task<Device?> GetByDeviceToken(string deviceId)
    {
        try
        {
            using (var con = Connection)
            {
                var device = await con.QueryFirstOrDefaultAsync<Device>(
                    @$"Select * from {nameof(Device)} where {nameof(Device.DeviceToken)} =@{nameof(deviceId)}",
                    new { deviceId });
                return device;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }


    public async Task<Device> Insert(Device device)
    {
        try
        {
            using (var con = Connection)
            {
                device.Id = await con.QueryFirstAsync<int>(
                    $"insert into {nameof(Device)} ({nameof(Device.DeviceToken)})" +
                    $" values (@{nameof(Device.DeviceToken)})" +
                    $"SELECT @@IDENTITY", new {device.DeviceToken});
   
                return device;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}