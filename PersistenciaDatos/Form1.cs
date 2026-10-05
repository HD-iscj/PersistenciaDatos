using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersistenciaDatos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string rutaArchivo = "C:\\Users\\C3\\Desktop\\Archivo.txt";//Acá va la ruta del archivo existenete
        public List<string> listaPersonas = new List<string>();

        //leer archivo
        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("El archivo de nombres no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                listBox1.Items.Clear();
                //con metodos para leer el archivo
                var nombres = File.ReadLines(rutaArchivo)
                    .Where(linea => !string.IsNullOrWhiteSpace(linea))
                    .ToArray();
                /*  
                 *  //alternativa a metodos 
                 *  var nombres = new List<string>();
                    using (StreamReader lector = new StreamReader(rutaArchivo))
                    {
                        string linea;
                        while ((linea = lector.ReadLine()) != null)
                        {
                            string nombre = linea.Trim();
                            if (!string.IsNullOrEmpty(nombre))
                            {
                                nombres.Add(nombre);
                            }
                        }
                    }
                 */
                //mostrar en listBox
                listaPersonas.Clear();
                listaPersonas.AddRange(nombres);
                listBox1.Items.AddRange(nombres);
            }
            catch (Exception ex) {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                listaPersonas.Add(textBox1.Text);
            }
            //si existe agrega contenido y si no existe crea el archivo en la ruta
            using (TextWriter archivo = File.AppendText(rutaArchivo))
            {
                archivo.WriteLine(listaPersonas.Last());
                archivo.Close();//es necesario cerrar el archivo o usar using que crea momentaneamente
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
