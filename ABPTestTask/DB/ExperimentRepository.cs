using System.Data;
using System.Data.SqlClient;
using ABPTestTask.Models;
using Dapper;

namespace ABPTestTask.DB;

public class ExperimentRepository
{
    private readonly string ConnectionString;

    public ExperimentRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public IDbConnection Connection
    {
        get => new SqlConnection(ConnectionString);
    }

    public async Task<ColorExperiment> InsertColorExperiment(ColorExperiment colorExp)
    {
        try
        {
            using var con = Connection;
            colorExp.Id = await con.QueryFirstAsync<int>(
                $"insert into {nameof(ColorExperiment)} ({nameof(ColorExperiment.DeviceId)}, {nameof(ColorExperiment.Color)})" +
                $" values (@{nameof(ColorExperiment.DeviceId)}, @{nameof(ColorExperiment.Color)})" +
                $"SELECT @@IDENTITY", new {colorExp.Color,colorExp.DeviceId});

            return colorExp;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<PriceExperiment> InsertPriceExperiment(PriceExperiment priceExp)
    {
        try
        {
            using var con = Connection;
            priceExp.Id = await con.QueryFirstAsync<int>(
                $"insert into {nameof(PriceExperiment)} ({nameof(PriceExperiment.DeviceId)}, {nameof(PriceExperiment.Price)})" +
                $" values (@{nameof(PriceExperiment.DeviceId)}, @{nameof(PriceExperiment.Price)})" +
                $"SELECT @@IDENTITY", new {priceExp.DeviceId,priceExp.Price});

            return priceExp;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

   

    public async Task<T?> GetByDeviceId<T>(int deviceId) where T : ForeignKey 
    {
        try
        {
            var nameType = typeof(T).Name;
            using var con = Connection;
            return await con.QueryFirstOrDefaultAsync<T>(
                $"select * from {nameType} where {nameof(ForeignKey.DeviceId)}={deviceId}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}