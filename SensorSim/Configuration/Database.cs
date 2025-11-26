using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorSim.Models;

namespace SensorSim.Configuration
{
    public static class Database
    {
        private static readonly string connectionString = "Data Source=sensordata.db";

        public static void Initialize()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS SensorData(
                        Id INTEGER PRIMARY KEY AUTOINCRREMENT,
                        SensorName TEXT,
                        Temperature REAL,
                        RecordedAt TEXT);";

            tableCmd.ExecuteNonQuery();
        }

        public static string ConnectionString => connectionString;
    }
}
