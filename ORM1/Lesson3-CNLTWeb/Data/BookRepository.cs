using Lesson3_CNLTWeb.Models;
using Microsoft.Data.SqlClient;

namespace Lesson3_CNLTWeb.Data
{
    public class BookRepository
    {
        private readonly string _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BookManagement")
                ?? throw new InvalidOperationException("Connection string 'BookManagement' not found.");
        }

        public List<Book> GetAll()
        {
            const string sql = "SELECT Id, Name, Author, Price FROM Book ORDER BY Id";

            var books = new List<Book>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                books.Add(MapBook(reader));
            }

            return books;
        }

        public Book? GetById(int id)
        {
            const string sql = "SELECT Id, Name, Author, Price FROM Book WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            return reader.Read() ? MapBook(reader) : null;
        }

        public void Add(Book book)
        {
            const string sql = """
                INSERT INTO Book (Name, Price)
                OUTPUT INSERTED.Id
                VALUES (@Name, @Price)
                """;

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", book.Name);
            command.Parameters.AddWithValue("@Author", book.Author);
            command.Parameters.AddWithValue("@Price", book.Price);

            book.Id = (int)command.ExecuteScalar()!;
        }

        public bool Update(Book book)
        {
            const string sql = """
                UPDATE Book
                SET Name = @Name, Author = @Author, Price = @Price
                WHERE Id = @Id
                """;

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", book.Id);
            command.Parameters.AddWithValue("@Name", book.Name);
            command.Parameters.AddWithValue("@Author", book.Author);
            command.Parameters.AddWithValue("@Price", book.Price);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM Book WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            return command.ExecuteNonQuery() > 0;
        }

        private static Book MapBook(SqlDataReader reader)
        {
            return new Book
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Author = reader.GetString(reader.GetOrdinal("Author")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price"))
            };
        }
    }
}
