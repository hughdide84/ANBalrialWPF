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
    /// Lógica de interacción para EntList.xaml
    /// </summary>
    public partial class EntList : Page
    {
        public EntList()
        {
            InitializeComponent();

            // Cargar datos al iniciar pantalla
            this.Loaded += EntList_Loaded;

            // Inicializar eventos botones
            this.btInsertar.Click += BtInsertar_Click;
            this.btEliminar.Click += BtEliminar_Click;
            this.btActualizar.Click += BtActualizar_Click;
        }

        private void EntList_Loaded(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            ListarEntidades();
        }

        private void ResetearAviso()
        {
            tbAvisos.Text = "";
            tbAvisos.Foreground = Brushes.White;
            tbAvisos.Background = Brushes.White;
            tbAvisos.Visibility = Visibility.Collapsed;
        }

        private void BtInsertar_Click(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            EntIns selFrame = new EntIns();
            this.NavigationService.Navigate(selFrame);
        }

        private void BtEliminar_Click(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            var eleSeleccionados = this.lvEntidades.SelectedItems;

            var idSel = 0;
            foreach (EntidadDTO entidadDTO in eleSeleccionados)
            {
                idSel = entidadDTO.id;

            }

            if (idSel == 0)
            {
                tbAvisos.Text = "Debe seleccionar un usuario";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;
            }
            else
            {
                EliminarEntidad(idSel);
                ListarEntidades();
            }

        }

        private void BtActualizar_Click(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            var eleSeleccionados = this.lvEntidades.SelectedItems;

            EntidadDTO entidadDTO = new EntidadDTO();
            var idSel = 0;
            foreach (EntidadDTO entidadDTOSel in eleSeleccionados)
            {
                entidadDTO = entidadDTOSel;
                idSel = entidadDTOSel.id;
            }

            if (idSel == 0)
            {
                tbAvisos.Text = "Debe seleccionar un usuario";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;
            }
            else
            {
                EntUpd selFrame = new EntUpd(entidadDTO);
                this.NavigationService.Navigate(selFrame, idSel);
            }

        }

        public void ListarEntidades()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/entidades").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<EntidadDTO> entidades = response.Content.ReadAsAsync<IEnumerable<EntidadDTO>>().Result;
                        lvEntidades.ItemsSource = entidades;
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

        public void EliminarEntidad(int idEntidad)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.DeleteAsync("api/entidades/" + idEntidad).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        tbAvisos.Text = "Eliminado correctamente";
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
