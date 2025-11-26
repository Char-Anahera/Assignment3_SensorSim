using Microsoft.Data.Sqlite;
using SensorSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorSim.Configuration
{
    public class DataRepo
    {
        public void StoreData(Data data)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();

            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText =
                @"
                INSERT INTO SensorData (SensorName, Temperature, RecordedAt)
                VALUES ($name, $temp, $time);";

            insertCmd.Parameters.AddWithValue("$name", data.SensorName);
            insertCmd.Parameters.AddWithValue("$temp", data.Temperature);
            insertCmd.Parameters.AddWithValue("$time", data.DateTime.ToString("o"));

            insertCmd.ExecuteNonQuery();
        }

        public List<Data> GetData()
        {
            var results = new List<Data>();

            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT SensorName, Temperature, RecordedAt FROM SensorData;";

            using var reader = selectCmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new Data
                {
                    SensorName = reader.GetString(0),
                    Temperature = reader.GetDouble(1),
                    DateTime = DateTime.Parse(reader.GetString(2))
                });
            }

            return results;
        }
    }
}
