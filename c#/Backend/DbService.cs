using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Backend
{
    public class DbService : IDbService
    {
        private readonly IDbManager dbManager;
        public async Task<List<Country>> GetPopulationAsync()
        {
            IDbManager db = new SqliteDbManager();
            using DbConnection conn = db.GetConnection();
            if (conn == null)
            {
                Console.WriteLine("Failed to get connection");
            }
            var command = conn.CreateCommand();
            command.CommandText =
                @"SELECT co.CountryName,sum(ci.Population) as TotalPopulation
                FROM City ci
                INNER JOIN State s ON s.StateId=ci.StateId
	            INNER JOIN Country co ON co.CountryId = s.CountryId
	            GROUP BY co.CountryName";
            var countriesWithTotalPopulation = new List<Country>();
            using (var countriesWithTotalPopulationQuery = await command.ExecuteReaderAsync())
            {
                while (await countriesWithTotalPopulationQuery.ReadAsync())
                {
                    string countryName = countriesWithTotalPopulationQuery.GetString(0);
                    long totalPopulation = countriesWithTotalPopulationQuery.GetInt64(1);
                    countriesWithTotalPopulation.Add(new Country(countryName, totalPopulation));
                }
            }
            ;
            return countriesWithTotalPopulation;
        }
    }
}
