using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
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

            // Cargar datos al iniciar pantalla
            this.Loaded += UsuList_Loaded;

            // Inicializar eventos botones
            this.btInsertar.Click += BtInsertar_Click;
            this.btEliminar.Click += BtEliminar_Click;
            this.btActualizar.Click += BtActualizar_Click;
            this.btBuscar.Click += BtBuscar_Click;
        }

        private void BtBuscar_Click(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            ListarUsuarios();
        }

        private void UsuList_Loaded(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            ListarUsuarios();
        }

        private void ResetearAviso ()
        {
            tbAvisos.Text = "";
            tbAvisos.Foreground = Brushes.White;
            tbAvisos.Background = Brushes.White;
            tbAvisos.Visibility = Visibility.Collapsed;
        }

        private void BtActualizar_Click(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            var eleSeleccionados = this.lvUsuarios.SelectedItems;

            UsuarioDTO usuarioDTO = new UsuarioDTO();
            var idSel = 0;
            foreach (UsuarioDTO usuarioDTOSel in eleSeleccionados)
            {
                usuarioDTO = usuarioDTOSel;
                idSel = usuarioDTOSel.id;
            }

            if (idSel == 0)
            {
                tbAvisos.Text = "Debe seleccionar un usuario";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;
            }
            else
            {
                UsuUpd selFrame = new UsuUpd(usuarioDTO);
                this.NavigationService.Navigate(selFrame, idSel);
            }
        }

        private void BtEliminar_Click(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            var eleSeleccionados = this.lvUsuarios.SelectedItems;

            var idSel = 0;
            foreach (UsuarioDTO usuarioDTO in eleSeleccionados)
            {
                idSel = usuarioDTO.id;
                
            }

            if (idSel == 0)
            {
                tbAvisos.Text = "Debe seleccionar un usuario";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;
            }
            else
            {
                EliminarUsuario(idSel);
                ListarUsuarios();
            }
        }

        private void BtInsertar_Click(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            UsuIns selFrame = new UsuIns();
            this.NavigationService.Navigate(selFrame);
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
                        IEnumerable<UsuarioDTO> usuarios = response.Content.ReadAsAsync<IEnumerable<UsuarioDTO>>().Result;

                        if (tbNombre.Text != "") { 
                            usuarios = usuarios.Where(o => o.nombre.ToUpper().Contains(tbNombre.Text.ToUpper()));
                        }

                        if (tbApellidos.Text != "")
                        {
                            usuarios = usuarios.Where(o => o.apellidos.ToUpper().Contains(tbApellidos.Text.ToUpper()));
                        }

                        lvUsuarios.ItemsSource = usuarios;
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

                    if (response.IsSuccessStatusCode)
                    {
                        tbAvisos.Text = "Eliminado correctamente";
                        tbAvisos.Foreground = Brushes.White;
                        tbAvisos.Background = Brushes.Green;
                    } else
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
