using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFBalrial.DTOs;

namespace WPFBalrial.Paginas
{
    /// <summary>
    /// Lógica de interacción para UbiIns.xaml
    /// </summary>
    public partial class UbiIns : Page
    {
        public int idEntidad { get; set; }
        public UbiIns()
        {
            InitializeComponent();

            btGuardar.Click += BtGuardar_Click;
            btSalir.Click += BtSalir_Click;
        }

        private void BtGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (tbNombre.Text == "" || tbDireccion.Text == "" || tbCp.Text == "" || tbPoblacion.Text == "" || tbZona.Text == "" || tbLongitud.Text == "" || tbLatitud.Text == "" || tbVolumen.Text == "")
            {
                tbAvisos.Text = "Algún campo está vacío";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;

                return;
            }

            InsertarUbicacion();
        }

        private void BtSalir_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        public void InsertarUbicacion()
        {


            var ubicacionDTO = new UbicacionDTO()
            {

                id = 0,
                nombre = tbNombre.Text,
                direccion = tbDireccion.Text,
                cp = Int32.Parse(tbCp.Text),
                poblacion = tbPoblacion.Text,
                zona = tbZona.Text,
                longitud = Double.Parse(tbLongitud.Text),
                latitud = Double.Parse(tbLatitud.Text),
                volumen = Int32.Parse(tbVolumen.Text),
                idEntidad = this.idEntidad
            };

  
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                   // client.BaseAddress = new Uri("http://localhost:8080/");

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PostAsJsonAsync("api/ubicaciones", ubicacionDTO).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        tbAvisos.Text = "Registrado correctamente";
                        tbAvisos.Foreground = Brushes.White;
                        tbAvisos.Background = Brushes.Green;
                    }
                    else
                    {
                        tbAvisos.Text = "Se ha producido un error";
                        tbAvisos.Foreground = Brushes.White;
                        tbAvisos.Background = Brushes.Crimson;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
