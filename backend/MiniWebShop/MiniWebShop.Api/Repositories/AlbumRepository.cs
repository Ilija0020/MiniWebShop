using Microsoft.Data.Sqlite;
using MiniWebShop.Api.Models;

namespace MiniWebShop.Api.Repositories
{
    public class AlbumRepository
    {
        private readonly string connectionString;

        public AlbumRepository(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:SQLiteConnection"]!;
        }

        public List<Album> GetAll()
        {
            List<Album> albums = new List<Album>();
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT al.Id, al.Title, al.ReleaseYear, al.ArtistId, ar. Name AS ArtistName
                        FROM Albums AS al
                        INNER JOIN Artists AS ar ON al.ArtistId = ar.Id";
                     
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Album album = new Album
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Title = reader["Title"].ToString()!,
                                    ReleaseYear = Convert.ToInt32(reader["ReleaseYear"]),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    ArtistId = Convert.ToInt32(reader["ArtistId"]),
                                    Artist = new Artist
                                    {
                                        Id = Convert.ToInt32(reader["ArtistId"]),
                                        Name = reader["ArtistName"].ToString()!
                                    }
                                };
                                albums.Add(album);
                            }
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greška pri povezivanju sa bazom ili izvršavanju SQL upita: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška jer konekcija nije ili je više puta otvorena: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška: {ex.Message}");
                throw;
            }
            return albums;
        }

        public List<Album> GetPaged(int page, int pageSize)
        {
            List<Album> albums = new List<Album>();
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT al.Id, al.Title, al.ReleaseYear, al.Price, al.ArtistId, ar.Name AS ArtistName 
                        FROM Albums AS al 
                        INNER JOIN Artists AS ar ON al.ArtistId = ar.Id
                        LIMIT @PageSize OFFSET @Offset";
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        int offset = (page - 1) * pageSize;
                        command.Parameters.AddWithValue("@PageSize", pageSize);
                        command.Parameters.AddWithValue("@Offset", offset);

                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Album album = new Album
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Title = reader["Title"].ToString()!,
                                    ReleaseYear = Convert.ToInt32(reader["ReleaseYear"]),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    ArtistId = Convert.ToInt32(reader["ArtistId"]),
                                    Artist = new Artist
                                    {
                                        Id = Convert.ToInt32(reader["ArtistId"]),
                                        Name = reader["ArtistName"].ToString()!
                                    }
                                };
                                albums.Add(album);
                            }
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greška pri povezivanju sa bazom ili izvršavanju SQL upita: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška jer konekcija nije ili je više puta otvorena: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška: {ex.Message}");
                throw;
            }
            return albums;
        }

        public int CountAll()
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Albums";
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greška pri povezivanju sa bazom ili izvršavanju SQL upita: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška jer konekcija nije ili je više puta otvorena: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška: {ex.Message}");
                throw;
            }
        }

        public Album? GetById(int id)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT al.Id, al.Title, al.ReleaseYear, al.Price, al.ArtistId, ar.Name AS ArtistName
                        FROM Albums AS al
                        INNER JOIN Artists AS ar ON al.ArtistId = ar.Id
                        WHERE al.Id = @Id";
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Album
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Title = reader["Title"].ToString()!,
                                    ReleaseYear = Convert.ToInt32(reader["ReleaseYear"]),
                                    Price = Convert.ToDecimal(reader["Price"]), // Čitamo cenu
                                    ArtistId = Convert.ToInt32(reader["ArtistId"]),

                                    Artist = new Artist
                                    {
                                        Id = Convert.ToInt32(reader["ArtistId"]),
                                        Name = reader["ArtistName"].ToString()!
                                    }
                                };
                            }
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greška pri povezivanju sa bazom ili izvršavanju SQL upita: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška jer konekcija nije ili je više puta otvorena: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška: {ex.Message}");
                throw;
            }
            return null;
        }

        public Album Create(Album album)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"
                        INSERT INTO Albums (Title, ReleaseYear, Price, ArtistId) 
                        VALUES (@Title, @ReleaseYear, @Price, @ArtistId);
                        SELECT LAST_INSERT_ROWID();";

                    using (SqliteCommand command = new SqliteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", album.Title);
                        command.Parameters.AddWithValue("@ReleaseYear", album.ReleaseYear);
                        command.Parameters.AddWithValue("@Price", album.Price);
                        command.Parameters.AddWithValue("@ArtistId", album.Artist.Id);

                        album.Id = Convert.ToInt32(command.ExecuteScalar());
                        return album;
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Greška pri povezivanju sa bazom ili izvršavanju SQL upita: {ex.Message}");
                throw;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Greška u formatu podataka: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška jer konekcija nije ili je više puta otvorena: {ex.Message}");
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
