using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarterasInventarioo.LogicaNegocio; 

namespace CarterasInventarioo
{
    public partial class Form1 : Form
    {
        
        private InventarioServicio _inventarioServicio;

        public Form1()
        {
            InitializeComponent();
            _inventarioServicio = new InventarioServicio(); 
        }

       
        private void CargarCarterasEnGrid()
        {
            try
            {
                List<Carteras> carteras = _inventarioServicio.ObtenerTodos();
                dataGridView1.DataSource = carteras;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // El servicio ya se inicializó en el constructor, ahora solo cargamos los datos
            CargarCarterasEnGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Código sin modificar, se mantiene vacío
        }

        // Evento de clic para el botón "Crear"
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                Carteras nuevaCartera = new Carteras
                {
                    Marca = txtMarca.Text, // Asume que tienes un control TextBox llamado txtMarca
                    Modelo = txtModelo.Text, // Asume que tienes un control TextBox llamado txtModelo
                    Precio = decimal.Parse(txtPrecio.Text), // Asume que tienes un control TextBox llamado txtPrecio
                    Stock = int.Parse(txtStock.Text) // Asume que tienes un control TextBox llamado txtStock
                };

                _inventarioServicio.Crear(nuevaCartera);
                MessageBox.Show("Cartera creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCarterasEnGrid(); // Recarga el DataGridView para mostrar el nuevo registro
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la cartera: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento de clic para el botón "Listar" (Asume que hay un botón llamado btnListar)
        private void btnListar_Click(object sender, EventArgs e)
        {
            CargarCarterasEnGrid(); // Este método ya lista todas las carteras
        }

        // Evento de clic para el botón "Obtener" (Asume que hay un botón llamado btnObtener y un txtId)
        private void btnObtener_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = int.Parse(txtId.Text);
                Carteras cartera = _inventarioServicio.ObtenerPorId(id);

                if (cartera != null)
                {
                    // Muestra los datos de la cartera en los campos de texto
                    txtMarca.Text = cartera.Marca;
                    txtModelo.Text = cartera.Modelo;
                    txtPrecio.Text = cartera.Precio.ToString();
                    txtStock.Text = cartera.Stock.ToString();
                }
                else
                {
                    MessageBox.Show("Cartera no encontrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la cartera: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento de clic para el botón "Actualizar" (Asume que hay un botón llamado btnActualizar y un txtId)
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = int.Parse(txtId.Text);
                Carteras carteraActualizada = new Carteras
                {
                    Id = id,
                    Marca = txtMarca.Text,
                    Modelo = txtModelo.Text,
                    Precio = decimal.Parse(txtPrecio.Text),
                    Stock = int.Parse(txtStock.Text)
                };

                _inventarioServicio.Actualizar(carteraActualizada);
                MessageBox.Show("Cartera actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCarterasEnGrid(); // Recarga el DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la cartera: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento de clic para el botón "Eliminar" (Asume que hay un botón llamado btnEliminar y un txtId)
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = int.Parse(txtId.Text);
                _inventarioServicio.Eliminar(id);
                MessageBox.Show("Cartera eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCarterasEnGrid(); // Recarga el DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la cartera: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
