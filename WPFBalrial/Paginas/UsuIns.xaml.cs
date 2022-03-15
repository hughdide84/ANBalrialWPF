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
    /// Lógica de interacción para UsuIns.xaml
    /// </summary>
    public partial class UsuIns : Page
    {
        public UsuIns()
        {
            InitializeComponent();
            btSalir.Click += BtSalir_Click;
            btGuardar.Click += BtGuardar_Click;
        }

        private void BtGuardar_Click(object sender, RoutedEventArgs e)
        {
            InsertarUsuario();
        }

        private void BtSalir_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        public void InsertarUsuario()
        {

            var usuarioDTO = new UsuarioDTO()
            {
                id = 0,
                nombre = tbNombre.Text,
                apellidos = tbApellidos.Text,
                login = "xxx",
                telefono = "xxx",
                email = "xxx",
                cp = 1,
                telegramId = 1,
                dias = "LM",
                horaInicio = "10:00",
                horaFin = "20:00",
                disponibilidad = 1
            };
                
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/"); 
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PostAsJsonAsync("api/usuarios", usuarioDTO).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        tbAvisos.Text = "Registrado correctamente";
                        tbAvisos.Foreground = Brushes.Green;
                    }
                    else
                    {
                        tbAvisos.Text = "Se ha producido un error";
                        tbAvisos.Foreground = Brushes.Red;
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
