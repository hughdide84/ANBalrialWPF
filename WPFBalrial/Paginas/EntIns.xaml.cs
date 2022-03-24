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
    /// Lógica de interacción para EntIns.xaml
    /// </summary>
    public partial class EntIns : Page
    {
        public EntIns()
        {
            InitializeComponent();
            btGuardar.Click += BtGuardar_Click;
            btSalir.Click += BtSalir_Click;
        }

        private void BtGuardar_Click(object sender, RoutedEventArgs e)
        {
            InsertarEntidad();
        }

        private void BtSalir_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        public void InsertarEntidad()
        {
            var entidadDTO = new EntidadDTO()
            {
                id = 0,
                nombre = tbNombre.Text,
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PostAsJsonAsync("api/entidades", entidadDTO).Result;

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
