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
    /// Lógica de interacción para UbiUpd.xaml
    /// </summary>
    public partial class UbiUpd : Page
    {
        public int idEntidad { get; set; }

        HttpClient client = new HttpClient();

        public UbiUpd(UbicacionDTO ubicacionDTO)
        {
            InitializeComponent();
            this.tbNombre.Text = ubicacionDTO.nombre.ToString();
            this.tbDireccion.Text = ubicacionDTO.direccion.ToString(); ;
            this.tbCp.Text = ubicacionDTO.cp.ToString(); ;
            this.tbPoblacion.Text = ubicacionDTO.poblacion.ToString(); ;
            this.tbZona.Text = ubicacionDTO.zona.ToString(); ;
            this.tbLatitud.Text = ubicacionDTO.latitud.ToString(); ;
            this.tbLongitud.Text = ubicacionDTO.longitud.ToString(); ;
            this.tbVolumen.Text = ubicacionDTO.volumen.ToString();
            this.tbId.Text = ubicacionDTO.id.ToString();
            

            client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

        }

        private void btSalir_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarUbicacion();

        }


        public void ActualizarUbicacion()
        {
            var ubicacionDTO = new UbicacionDTO()
            {
                id = Int32.Parse(tbId.Text),
                nombre = tbNombre.Text,
                direccion = tbDireccion.Text,
                cp = Int32.Parse(tbCp.Text),
                zona = tbZona.Text,
                poblacion = tbPoblacion.Text,
                longitud = Int32.Parse(tbLongitud.Text),
                latitud = Int32.Parse(tbLatitud.Text),
                volumen = Int32.Parse(tbVolumen.Text),
                idEntidad = this.idEntidad
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                    //client.BaseAddress = new Uri("http://localhost:8080/");

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PutAsJsonAsync("api/ubicaciones/" + tbId.Text, ubicacionDTO).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        tbAvisos.Text = " Actualizado correctamente";
                        tbAvisos.Foreground = Brushes.White;
                        tbAvisos.Background = Brushes.Green;
                    }
                    else
                    {
                        tbAvisos.Text = " Se ha producido un error";
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
