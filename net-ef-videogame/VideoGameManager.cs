using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace net_ef_videogame
{
    internal static class VideoGameManager
    {
        private const string STRINGA_DI_CONNESSIONE = "Data Source=localhost;Initial Catalog=db-videogames-query;Integrated Security=True;Pooling=False;Encrypt=False;TrustServerCertificate=False";

        internal static void AddNewGame(VideoGame newVideoGame)
        {
            //throw new NotImplementedException();

            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            try
            {
                connessioneSql.Open();
                string query = @"INSERT INTO videogames (name, overview, release_date, created_at, updated_at, software_house_id)
VALUES (@name, @overview, @release_date, @created_at, @updated_at, @sh_id)";

                using SqlCommand cmd = new SqlCommand(query, connessioneSql);
                InsertInternal(cmd, newVideoGame);
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }
        }

        private static int InsertInternal(SqlCommand cmd, VideoGame newVideoGame)
        {
            cmd.Parameters.Add(new SqlParameter("@name", newVideoGame.Name));
            cmd.Parameters.Add(new SqlParameter("@overview", newVideoGame.Overview));
            cmd.Parameters.Add(new SqlParameter("@release_date", newVideoGame.ReleaseDate));
            cmd.Parameters.Add(new SqlParameter("@created_at", newVideoGame.CreatedAt));
            cmd.Parameters.Add(new SqlParameter("@updated_at", newVideoGame.UpdatedAt));
            cmd.Parameters.Add(new SqlParameter("@sh_id", newVideoGame.SoftwareHouseID));

            int affectedRows = cmd.ExecuteNonQuery();
            return affectedRows;
        }

        internal static VideoGame GetVideoGameById(long id)
        {
            VideoGame videoGame = null;

            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            try
            {
                connessioneSql.Open();
                string query = @"SELECT * FROM videogames WHERE id = @id";

                using SqlCommand cmd = new SqlCommand(query, connessioneSql);
                cmd.Parameters.AddWithValue("@id", id);

                using SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                    throw new Exception($"VideoGame con id - {id} non e stata trovata.");

                int indiceID = reader.GetOrdinal("id");
                int indiceName = reader.GetOrdinal("name");
                int indiceOverview = reader.GetOrdinal("overview");
                int indiceReleaseDate = reader.GetOrdinal("release_date");
                int indiceCreatedAt = reader.GetOrdinal("created_at");
                int indiceUpdatedAt = reader.GetOrdinal("updated_at");
                int indiceSoftwareHouseID = reader.GetOrdinal("software_house_id");

                long idVideogame = reader.GetInt64(indiceID);
                string name = reader.GetString(indiceName);
                string overview = reader.GetString(indiceOverview);
                DateTime releaseDate = reader.GetDateTime(indiceReleaseDate);
                DateTime createdAt = reader.GetDateTime(indiceCreatedAt);
                DateTime updatedAt = reader.GetDateTime(indiceUpdatedAt);
                long softwareHouseID = reader.GetInt64(indiceSoftwareHouseID);

                videoGame = new VideoGame(idVideogame, name, overview, releaseDate, createdAt, updatedAt, softwareHouseID);
            }
            catch (Exception ex) 
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            return videoGame;
        }

        internal static List<VideoGame> GetVideoGamesByName(string nameToSearch)
        {
            List<VideoGame> videoGames = new List<VideoGame>();

            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            try
            {
                connessioneSql.Open();
                string query = @"SELECT * FROM videogames WHERE name LIKE '%' + @name + '%'";

                using SqlCommand cmd = new SqlCommand(query, connessioneSql);
                cmd.Parameters.AddWithValue("@name", nameToSearch);

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int indiceID = reader.GetOrdinal("id");
                    int indiceName = reader.GetOrdinal("name");
                    int indiceOverview = reader.GetOrdinal("overview");
                    int indiceReleaseDate = reader.GetOrdinal("release_date");
                    int indiceCreatedAt = reader.GetOrdinal("created_at");
                    int indiceUpdatedAt = reader.GetOrdinal("updated_at");
                    int indiceSoftwareHouseID = reader.GetOrdinal("software_house_id");

                    long idVideogame = reader.GetInt64(indiceID);
                    string name = reader.GetString(indiceName);
                    string overview = reader.GetString(indiceOverview);
                    DateTime releaseDate = reader.GetDateTime(indiceReleaseDate);
                    DateTime createdAt = reader.GetDateTime(indiceCreatedAt);
                    DateTime updatedAt = reader.GetDateTime(indiceUpdatedAt);
                    long softwareHouseID = reader.GetInt64(indiceSoftwareHouseID);

                    VideoGame videoGame = new VideoGame(idVideogame, name, overview, releaseDate, createdAt, updatedAt, softwareHouseID);
               
                    videoGames.Add(videoGame);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }
            return videoGames;
        }

        internal static void DeleteVideoGame(VideoGame videoGame)
        {
            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            try
            {
                connessioneSql.Open();
                string query = @"DELETE FROM videogames WHERE id = @id";

                using SqlCommand cmd = new SqlCommand(query, connessioneSql);
                cmd.Parameters.AddWithValue("@id", videoGame.Id);

                int affectedRows = cmd.ExecuteNonQuery();

                if(affectedRows > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Eliminato con successo!");
                    Console.WriteLine($"Dati del VideoGame eliminato: ");
                    Console.WriteLine();
                    Console.WriteLine(videoGame.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
