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
        //defines the database file
        private static readonly string connectionString = "Data Source=sensordata.db";

        // creates table if it doesn't exist
        public static void Initialize()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS SensorData(
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        SensorName TEXT,
                        Temperature REAL,
                        RecordedAt TEXT);";

            tableCmd.ExecuteNonQuery();
        }

        // sets the connection string to be called on by other classes
        public static string ConnectionString => connectionString;
    }
}
