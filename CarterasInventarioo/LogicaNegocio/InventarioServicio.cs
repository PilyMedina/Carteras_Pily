//
// Clase InventarioServicio.cs: Lógica de negocio para las operaciones CRUD
//
// Este archivo contiene la clase que se encarga de las operaciones
// de crear, listar, obtener, actualizar y eliminar carteras en la base de datos.
//

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CarterasInventarioo.LogicaNegocio
{
    public class InventarioServicio
    {
        // Cadena de conexión a la base de datos
        // Importante: Reemplaza los valores con los de tu configuración local de MySQL
        private readonly string connectionString = "Server=localhost;Database=CarterasDB;Uid=root;Pwd=tu_contraseña_aqui;";

        //
        // Método para crear una nueva cartera
        //
        public void Crear(Carteras cartera)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Carteras (Marca, Modelo, Precio, Stock) VALUES (@Marca, @Modelo, @Precio, @Stock)";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Marca", cartera.Marca);
                command.Parameters.AddWithValue("@Modelo", cartera.Modelo);
                command.Parameters.AddWithValue("@Precio", cartera.Precio);
                command.Parameters.AddWithValue("@Stock", cartera.Stock);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //
        // Método para obtener todas las carteras
        //
        public List<Carteras> ObtenerTodos()
        {
            List<Carteras> carteras = new List<Carteras>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT Id, Marca, Modelo, Precio, Stock FROM Carteras";
                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

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
                return carteras;
            }
        }

        //
        // Método para obtener una cartera por su Id
        //
        public Carteras ObtenerPorId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT Id, Marca, Modelo, Precio, Stock FROM Carteras WHERE Id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Carteras
                    {
                        Id = reader.GetInt32("Id"),
                        Marca = reader.GetString("Marca"),
                        Modelo = reader.GetString("Modelo"),
                        Precio = reader.GetDecimal("Precio"),
                        Stock = reader.GetInt32("Stock")
                    };
                }
                return null; // Devuelve null si no se encuentra la cartera
            }
        }

        //
        // Método para actualizar una cartera existente
        //
        public void Actualizar(Carteras cartera)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE Carteras SET Marca = @Marca, Modelo = @Modelo, Precio = @Precio, Stock = @Stock WHERE Id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Marca", cartera.Marca);
                command.Parameters.AddWithValue("@Modelo", cartera.Modelo);
                command.Parameters.AddWithValue("@Precio", cartera.Precio);
                command.Parameters.AddWithValue("@Stock", cartera.Stock);
                command.Parameters.AddWithValue("@Id", cartera.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //
        // Método para eliminar una cartera por su Id
        //
        public void Eliminar(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Carteras WHERE Id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
