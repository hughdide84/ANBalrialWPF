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
    /// Lógica de interacción para UsuList.xaml
    /// </summary>
    public partial class UsuList : Page
    {
        public UsuList()
        {
            InitializeComponent();
            ListarUsuarios();
            this.btInsertar.Click += BtInsertar_Click;
            this.btEliminar.Click += BtEliminar_Click;
        }

        private void BtEliminar_Click(object sender, RoutedEventArgs e)
        {
            var eleSeleccionados = this.lvUsuarios.SelectedItems;

            foreach (UsuarioDTO usuarioDTO in eleSeleccionados)
            {
                EliminarUsuario(usuarioDTO.id);
            }
            ListarUsuarios();
        }

        private void BtInsertar_Click(object sender, RoutedEventArgs e)
        {
            UsuIns usuIns = new UsuIns();
            this.NavigationService.Navigate(usuIns);
        }

        public void ListarUsuarios()
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/usuarios").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var usuarios = response.Content.ReadAsAsync<IEnumerable<UsuarioDTO>>().Result;
                        lvUsuarios.ItemsSource = usuarios;

                    }
                    else
                    {
                        MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarUsuario(int idUsuario)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.DeleteAsync("api/usuarios/"+idUsuario).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
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
