using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace AnimalManager.Services
{
    public class DatabaseService
    {
        private const string ConnectionString = "Data Source=animal_manager.db";

        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string tableCreationQuery = @"
                    CREATE TABLE IF NOT EXISTS Animal (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL,
                        Idade INTEGER NOT NULL,
                        Especie TEXT NOT NULL,
                        DataAdocao TEXT
                    )";
                using (var command = new SQLiteCommand(tableCreationQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static SQLiteConnection GetConnection() => new SQLiteConnection(ConnectionString);
    }
}
