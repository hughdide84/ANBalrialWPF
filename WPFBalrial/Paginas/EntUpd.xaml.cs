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
    /// Lógica de interacción para EntUpd.xaml  
    /// </summary>
    public partial class EntUpd : Page
    {

        HttpClient client = new HttpClient();

        public EntUpd(EntidadDTO entidadDTO)
        {
            InitializeComponent();

            this.Loaded += EntUpd_Loaded;

            // Inicializar eventos botones
            btSalir.Click += BtSalir_Click;
            btGuardar.Click += BtGuardar_Click;

            this.btInsertarUbi.Click += BtInsertarUbi_Click;
            this.btEliminarUbi.Click += BtEliminarUbi_Click;
            this.btActualizarUbi.Click += BtActualizarUbi_Click;

            // Rellenar datos de la entidad a actualizar
            this.tbId.Text = entidadDTO.id.ToString();
            this.tbNombre.Text = entidadDTO.nombre;

            // Inicializar cliente
            client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
        }

        private void ResetearAviso()
        {
            tbAvisos.Text = "";
            tbAvisos.Foreground = Brushes.White;
            tbAvisos.Background = Brushes.White;
            tbAvisos.Visibility = Visibility.Collapsed;
        }

        private void BtInsertarUbi_Click(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            UbiIns selFrame = new UbiIns();
            this.NavigationService.Navigate(selFrame);
        }

        private void BtEliminarUbi_Click(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            var eleSeleccionados = this.lvUbicaciones.SelectedItems;

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
                EliminarUbicacion(idSel);
                ListarUbicaciones();
            }
        }

        private void BtActualizarUbi_Click(object sender, RoutedEventArgs e)
        {
            ResetearAviso();
            var eleSeleccionados = this.lvUbicaciones.SelectedItems;

            UbicacionDTO ubicacionDTO = new UbicacionDTO();
            var idSel = 0;
            foreach (UbicacionDTO ubicacionDTOSel in eleSeleccionados)
            {
                ubicacionDTO = ubicacionDTOSel;
                idSel = ubicacionDTOSel.id;
            }

            if (idSel == 0)
            {
                tbAvisos.Text = "Debe seleccionar un usuario";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;
            }
            else
            {
                UbiUpd selFrame = new UbiUpd(ubicacionDTO);
                this.NavigationService.Navigate(selFrame, idSel);
            }
        }

        private void EntUpd_Loaded(object sender, RoutedEventArgs e)
        {
            ListarUbicaciones();
        }

        private void BtGuardar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarEntidad();
        }

        private void BtSalir_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        public void ActualizarEntidad()
        {
            var entidadDTO = new UsuarioDTO()
            {
                id = Int32.Parse(tbId.Text),
                nombre = tbNombre.Text,
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PutAsJsonAsync("api/entidades/" + tbId.Text, entidadDTO).Result;

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

        public void ListarUbicaciones()
        {
            var ubicacionDTO = new UbicacionDTO()
            {
                idEntidad = Int32.Parse(tbId.Text)
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/entidades/"+ ubicacionDTO.idEntidad + "/ubicaciones").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<UbicacionDTO> entidades = response.Content.ReadAsAsync<IEnumerable<UbicacionDTO>>().Result;
                        lvUbicaciones.ItemsSource = entidades;
                    }
                    else
                    {
                        tbAvisos.Text = "Se ha producido un error al mostrar las ubicaciones";
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

        public void EliminarUbicacion(int idUbicacion)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.DeleteAsync("api/entidades/" + idUbicacion).Result;

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