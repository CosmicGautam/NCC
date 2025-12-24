using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApp4ByJessica.Models;

namespace WebApp4ByJessica.Data
{
    public class BookRepository
    {
        private readonly string _connString;

        public BookRepository(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection")!;
        }

        // READ ALL safely
        public IEnumerable<Book> GetAll()
        {
            var list = new List<Book>();
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand("SELECT Id, Title, Author, Isbn, Price, PublishedDate FROM dbo.Bookss ORDER BY Id DESC", conn);
            conn.Open();
            using var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {

                list.Add(new Book
                {
                    Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                    Title = rdr.IsDBNull(rdr.GetOrdinal("Title")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("Title")),
                    Author = rdr.IsDBNull(rdr.GetOrdinal("Author")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("Author")),
                    Isbn = rdr.IsDBNull(rdr.GetOrdinal("Isbn")) ? null : rdr.GetString(rdr.GetOrdinal("Isbn")),
                    Price = rdr.IsDBNull(rdr.GetOrdinal("Price")) ? (decimal?)null : rdr.GetDecimal(rdr.GetOrdinal("Price")),
                    PublishedDate = rdr.IsDBNull(rdr.GetOrdinal("PublishedDate")) ? (DateTime?)null : rdr.GetDateTime(rdr.GetOrdinal("PublishedDate"))
                });
            }

            return list;
        }

        // READ ONE safely
        public Book? GetById(int id)
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand("SELECT Id, Title, Author, Isbn, Price, PublishedDate FROM dbo.Bookss WHERE Id=@Id", conn);
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
            conn.Open();

            using var rdr = cmd.ExecuteReader();
            if (!rdr.Read()) return null;

            return new Book
            {
                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                Title = rdr.IsDBNull(rdr.GetOrdinal("Title")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("Title")),
                Author = rdr.IsDBNull(rdr.GetOrdinal("Author")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("Author")),
                Isbn = rdr.IsDBNull(rdr.GetOrdinal("Isbn")) ? null : rdr.GetString(rdr.GetOrdinal("Isbn")),
                Price = rdr.IsDBNull(rdr.GetOrdinal("Price")) ? (decimal?)null : rdr.GetDecimal(rdr.GetOrdinal("Price")),
                PublishedDate = rdr.IsDBNull(rdr.GetOrdinal("PublishedDate")) ? (DateTime?)null : rdr.GetDateTime(rdr.GetOrdinal("PublishedDate"))
            };
        }

        // CREATE safely
        public int Create(Book book)
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(@"
                INSERT INTO dbo.Bookss (Title, Author, Isbn, Price, PublishedDate)
                OUTPUT INSERTED.Id
                VALUES (@Title, @Author, @Isbn, @Price, @PublishedDate);", conn);

            cmd.Parameters.AddWithValue("@Title", book.Title ?? throw
                new ArgumentNullException(nameof(book.Title)));
            cmd.Parameters.AddWithValue("@Author", book.Author ?? 
                throw new ArgumentNullException(nameof(book.Author)));
            cmd.Parameters.AddWithValue("@Isbn", (object?)book.Isbn 
                ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", (object?)book.Price ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PublishedDate", (object?)
                book.PublishedDate ?? DBNull.Value);

            conn.Open();
            var result = cmd.ExecuteScalar();
            return Convert.ToInt32(result); // returns auto-generated Id
        }

        // UPDATE safely
        public void Update(Book book)
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(@"
                UPDATE dbo.Bookss
                SET Title=@Title, Author=@Author, Isbn=@Isbn, Price=@Price, PublishedDate=@PublishedDate
                WHERE Id=@Id;", conn);

            cmd.Parameters.AddWithValue("@Id", book.Id); // just to locate the row
            cmd.Parameters.AddWithValue("@Title", book.Title ?? throw new ArgumentNullException(nameof(book.Title)));
            cmd.Parameters.AddWithValue("@Author", book.Author ?? throw new ArgumentNullException(nameof(book.Author)));
            cmd.Parameters.AddWithValue("@Isbn", (object?)book.Isbn ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", (object?)book.Price ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PublishedDate", (object?)book.PublishedDate ?? DBNull.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        // DELETE safely
        public void Delete(int id)
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand("DELETE FROM dbo.Bookss WHERE Id=@Id", conn);
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
