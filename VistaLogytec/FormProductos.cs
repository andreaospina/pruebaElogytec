using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using VistaLogytec.Models;

namespace VistaLogytec
{
    public partial class FormProductos : DevExpress.XtraEditors.XtraForm
    {
        private string urlProducto = "https://localhost:7175/Productos/";
        private string urlMarca = "https://localhost:7175/Marcas/";
        public FormProductos()
        {
            InitializeComponent();
        }

        private async void FormProductos_Load(object sender, EventArgs e)
        {
            llenarGrid();
            LLenarMarca();

        }

        private async void gvEditar_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var row = GvProductos.GetFocusedRow();
            var content = new StringContent(JsonConvert.SerializeObject(row), Encoding.UTF8, "application/json");
            var resultPost = await client.PostAsync($"{urlProducto}Edit", content);
            if (resultPost.StatusCode == System.Net.HttpStatusCode.OK)
            {
                XtraMessageBox.Show($"Producto editado con exito ", "informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show($"Error, Algo salió mal, intentelo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        private async void gvEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var eliminar = XtraMessageBox.Show($"Alerta, Desea eliminar el registro", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (eliminar == DialogResult.No)
                return;

            var row = GvProductos.GetFocusedRow() as Productos;
            HttpClient client = new HttpClient();
            var result = await client.DeleteAsync($"{urlProducto}Delete?Id={row.Id}");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                GvProductos.DeleteSelectedRows();
            }
        }

        private async void llenarGrid()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync($"{urlProducto}GetAll");
            var info = JsonConvert.DeserializeObject<List<Productos>>(result);
            gridProductos.DataSource = info;
        }

        private async void LLenarMarca()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync($"{urlMarca }GetAll");
            var info = JsonConvert.DeserializeObject<List<Marcas>>(result);
            sltMarca.Properties.DataSource = info;
            sLeMarca.DataSource = info;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var Mensaje = "";

            if ( Producto.EditValue is null)
               Mensaje += "el campo producto es requerido \n";

            if(Codigo.EditValue is null)
                Mensaje += "el campo codigo es requerido \n";

            if(Precio.EditValue is null)
                Mensaje += "el campo precio es requerido \n";

            if(Costo.EditValue is null)
                Mensaje += "el campo costo es requerido \n";

            if(sltMarca.EditValue is null)
                Mensaje += "el campo Marca es requerido \n";

            if (!string.IsNullOrEmpty(Mensaje))
            {
                XtraMessageBox.Show($"{ Mensaje }", "informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var productos = GvProductos.DataSource as List<Productos>;
                
            if (productos.Any(x => x.Descripcion == Producto.EditValue))
            {
                XtraMessageBox.Show($"El Producto ya existe ", "informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var producto = new Productos()
            {
                Descripcion = Producto.Text,
                Ean = Codigo.Text,
                Precio = float.Parse(Precio.Text),
                Costo = float.Parse(Costo.Text),
                IdMarca = Convert.ToInt32(sltMarca.EditValue)
            };

            HttpClient client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
            var resultPost = await client.PostAsync($"{urlProducto}Create", content);
            if (resultPost.StatusCode == System.Net.HttpStatusCode.OK)
            {
                XtraMessageBox.Show($"Producto Guardado con exito ", "informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                llenarGrid();

                Producto.EditValue = null;
                Codigo.EditValue = null;
                Precio.EditValue = null;
                Costo.EditValue = null;
                sltMarca.EditValue = null;
            }
            else
            {
                XtraMessageBox.Show($"Error, Algo salió mal, intentelo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}