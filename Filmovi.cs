using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MovieLib
{
    class Filmovi
    {
        public static async Task<List<Film>> GetFilmoviAsync()
        {
            List<Film> filmovi = new List<Film>();
            string query = "SELECT * FROM Film ORDER BY Naziv";

            using (SqlConnection sqlConnection = new SqlConnection(Baza.ConnectionString()))
            {
                try
                {
                    // async vezu sa bazom podataka
                    await sqlConnection.OpenAsync();

                    SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
                    DataSet ds = new DataSet();

                    await Task.Run(() => adapter.Fill(ds));

                    foreach (DataRow redak in ds.Tables[0].Rows)
                    {
                        int id = redak["Id"] != DBNull.Value ? Convert.ToInt32(redak["Id"]) : 0;
                        string naziv = redak["Naziv"] != DBNull.Value ? redak["Naziv"].ToString() : string.Empty;
                        int trajanje = redak["Trajanje"] != DBNull.Value ? Convert.ToInt32(redak["Trajanje"]) : 0;
                        int godina = redak["Godina"] != DBNull.Value ? Convert.ToInt32(redak["Godina"]) : 0;
                        string zanr = redak["Zanr"] != DBNull.Value ? redak["Zanr"].ToString() : string.Empty;

                        filmovi.Add(new Film(id, naziv, trajanje, godina, zanr));
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Greška: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Opća Greška: " + ex.Message);
                }
            }

            return filmovi;
        }


        public static async Task<List<Film>> RandomMovie()
        {
            List<Film> filmovi = new List<Film>();

            // Prvi upit za dohvat najvećeg ID-a
            string maxIdQuery = "SELECT MAX(Id) FROM Film";

            // Drugi upit za dohvaćanje filma po nasumičnom ID-u
            string filmByIdQuery = "SELECT * FROM Film WHERE Id = @RandomId";

            using (SqlConnection sqlConnection = new SqlConnection(Baza.ConnectionString()))
            {
                try
                {
                    await sqlConnection.OpenAsync();

                    // Prvo dohvaćanje najvećeg ID-a
                    int maxId = 0;
                    using (SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, sqlConnection))
                    {
                        object result = await maxIdCommand.ExecuteScalarAsync();
                        maxId = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }

                    if (maxId == 0)
                    {
                        // Ako nema filmova u bazi
                        return filmovi;
                    }

                    // Generisanje nasumičnog ID-a između 1 i maxId
                    Random random = new Random();
                    int randomId = random.Next(1, maxId + 1);  // nasumičan broj između 1 i maxId

                    // Dohvaćanje filma sa nasumičnim ID-om
                    using (SqlCommand filmByIdCommand = new SqlCommand(filmByIdQuery, sqlConnection))
                    {
                        filmByIdCommand.Parameters.AddWithValue("@RandomId", randomId);

                        SqlDataAdapter adapter = new SqlDataAdapter(filmByIdCommand);
                        DataSet ds = new DataSet();

                        await Task.Run(() => adapter.Fill(ds));

                        foreach (DataRow redak in ds.Tables[0].Rows)
                        {
                            int id = redak["Id"] != DBNull.Value ? Convert.ToInt32(redak["Id"]) : 0;
                            string naziv = redak["Naziv"] != DBNull.Value ? redak["Naziv"].ToString() : string.Empty;
                            int trajanje = redak["Trajanje"] != DBNull.Value ? Convert.ToInt32(redak["Trajanje"]) : 0;
                            int godina = redak["Godina"] != DBNull.Value ? Convert.ToInt32(redak["Godina"]) : 0;
                            string zanr = redak["Zanr"] != DBNull.Value ? redak["Zanr"].ToString() : string.Empty;

                            filmovi.Add(new Film(id, naziv, trajanje, godina, zanr));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Greška: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Opća Greška: " + ex.Message);
                }
            }

            return filmovi;
        }

    }
}
