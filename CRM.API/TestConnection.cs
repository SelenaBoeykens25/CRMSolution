using Microsoft.Data.SqlClient;
using System;

namespace CRM.API
{
    public class TestConnection
    {
        public static async Task<bool> TestDatabaseConnectionAsync(string connectionString)
        {
            try
            {
                Console.WriteLine("Testing database connection...");
                Console.WriteLine($"Attempting to connect...");
                
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                
                Console.WriteLine("✓ Connection successful!");
                Console.WriteLine($"Server version: {connection.ServerVersion}");
                Console.WriteLine($"Database: {connection.Database}");
                
                // Test a simple query
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT @@VERSION";
                var version = await command.ExecuteScalarAsync();
                Console.WriteLine($"SQL Server Version: {version}");
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Connection failed: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                return false;
            }
        }
    }
}
