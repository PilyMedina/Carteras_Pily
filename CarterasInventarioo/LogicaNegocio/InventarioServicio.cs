using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarterasInventarioo.LogicaNegocio
{
    internal class InventarioServicio
    {

            private string connectionString = "server=localhost;database=CarteraInventoryDB;uid=root;pwd=;";

            public List<Carteras> GetAll()
            {
                List<Carteras> carteras = new List<Carteras>();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "SELECT * FROM Carteras";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carteras.Add(new Carteras
                            {
                                Id = reader.GetInt32("Id"),
                                Marca = reader.GetString("Marca"),
                                Modelo = reader.GetString("Modelo"),
                                Precio = reader.GetDecimal("Precio"),
                                Stock = reader.GetInt32("Stock")
                            });
                        }
                    }
                }
                return carteras;
            }

            public void Add(Carteras cartera)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "INSERT INTO Carteras (Marca, Modelo, Precio, Stock) VALUES (@Marca, @Modelo, @Precio, @Stock)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Marca", cartera.Marca);
                    cmd.Parameters.AddWithValue("@Modelo", cartera.Modelo);
                    cmd.Parameters.AddWithValue("@Precio", cartera.Precio);
                    cmd.Parameters.AddWithValue("@Stock", cartera.Stock);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // ... (Métodos Update y Delete, que son similares) ...

            public void Update(Carteras cartera)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "UPDATE Carteras SET Marca = @Marca, Modelo = @Modelo, Precio = @Precio, Stock = @Stock WHERE Id = @Id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Marca", cartera.Marca);
                    cmd.Parameters.AddWithValue("@Modelo", cartera.Modelo);
                    cmd.Parameters.AddWithValue("@Precio", cartera.Precio);
                    cmd.Parameters.AddWithValue("@Stock", cartera.Stock);
                    cmd.Parameters.AddWithValue("@Id", cartera.Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            public void Delete(int id)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "DELETE FROM Carteras WHERE Id = @Id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
    }
}


