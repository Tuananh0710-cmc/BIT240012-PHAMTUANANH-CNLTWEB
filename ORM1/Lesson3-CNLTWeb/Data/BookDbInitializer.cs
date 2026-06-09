using Microsoft.Data.SqlClient;

namespace Lesson3_CNLTWeb.Data
{
    public static class BookDbInitializer
    {
        public static void Initialize(IConfiguration configuration)
        {
            var bookConnectionString = configuration.GetConnectionString("BookManagement")
                ?? throw new InvalidOperationException("Connection string 'BookManagement' not found.");

            var masterConnectionString = bookConnectionString
                .Replace("Database=BookManagement", "Database=master", StringComparison.OrdinalIgnoreCase);

            CreateDatabaseIfNotExists(masterConnectionString);
            CreateBookTableIfNotExists(bookConnectionString);
            EnsureAuthorColumnExists(bookConnectionString);
        }

        private static void CreateDatabaseIfNotExists(string masterConnectionString)
        {
            const string sql = """
                IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'BookManagement')
                BEGIN
                    CREATE DATABASE BookManagement;
                END
                """;

            using var connection = new SqlConnection(masterConnectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        private static void CreateBookTableIfNotExists(string bookConnectionString)
        {
            const string sql = """
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Book')
                BEGIN
                    CREATE TABLE Book (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name NVARCHAR(200) NOT NULL,
                        Author NVARCHAR(200) NOT NULL DEFAULT N'',
                        Price DECIMAL(18,2) NOT NULL
                    );
                END
                """;

            using var connection = new SqlConnection(bookConnectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        private static void EnsureAuthorColumnExists(string bookConnectionString)
        {
            const string sql = """
                IF NOT EXISTS (
                    SELECT * FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Book') AND name = N'Author'
                )
                BEGIN
                    ALTER TABLE Book ADD Author NVARCHAR(200) NOT NULL DEFAULT N'';
                END
                """;

            using var connection = new SqlConnection(bookConnectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}
