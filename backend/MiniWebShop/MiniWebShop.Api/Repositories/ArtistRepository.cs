using Microsoft.Data.Sqlite;
using MiniWebShop.Api.Models;

namespace MiniWebShop.Api.Repositories
{
    public class ArtistRepository
    {
        private readonly string connectionString;

        public ArtistRepository(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:SQLiteConnection"];
        }

        public List<Artist> GetAll()
        {
            List<Artist> artists = new List<Artist>();
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name FROM Artists";
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                artists.Add(new Artist
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString()!
                                });
                            }
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greska sa bazom: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška u radu sa konekcijom: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška: {ex.Message}");
                throw;
            }
            return artists;
        }

        public Artist? GetById(int id)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name FROM Artists Where Id = @Id";
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Artist
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString()!
                                };
                            }
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greska sa bazom: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška u radu sa konekcijom: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška: {ex.Message}");
                throw;
            }
            return null;
        }

        public List<Artist> GetPaged(int page, int pageSize)
        {
            List<Artist> artists = new List<Artist>();
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name FROM Artists LIMIT @PageSize OFFSET @Offset";
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PageSize", pageSize);
                        command.Parameters.AddWithValue("@Offset", (page - 1) * pageSize);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                artists.Add(new Artist
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString()!
                                });
                            }
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greska sa bazom: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška u radu sa konekcijom: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška: {ex.Message}");
                throw;
            }
            return artists;
        }
        public int CountAll()
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Artists";
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greska sa bazom: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška u radu sa konekcijom: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška: {ex.Message}");
                throw;
            }
        }
        public Artist Create(Artist artist)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Artists (Name) VALUES (@Name); SELECT last_insert_rowid();";
                    using (SqliteCommand command = new SqliteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", artist.Name);
                        artist.Id = Convert.ToInt32(command.ExecuteScalar());
                        return artist;
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greska sa bazom: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška u radu sa konekcijom: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška: {ex.Message}");
                throw;
            }
        }
        public Artist Update(Artist artist)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Artists SET Name = @Name WHERE Id = @Id";
                    using (SqliteCommand command = new SqliteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", artist.Name);
                        command.Parameters.AddWithValue("@Id", artist.Id);
                        int affectedRows = command.ExecuteNonQuery();
                        return affectedRows > 0 ? artist : null;
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greska sa bazom: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška u radu sa konekcijom: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška: {ex.Message}");
                throw;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Artists WHERE Id = @Id";
                    using (SqliteCommand command = new SqliteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        int affectedRows = command.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greska sa bazom: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška u radu sa konekcijom: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška: {ex.Message}");
                throw;
            }
        }
    }
}
