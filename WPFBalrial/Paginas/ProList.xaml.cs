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
    /// Lógica de interacción para ProList.xaml
    /// </summary>
    public partial class ProList : Page
    {
        public ProList()
        {
            InitializeComponent();
            listProyects();
        }

        private void listProyects()
        {
            try
            {
                using (var client=new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/proyectos").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<ProyectoDTO> ProyectoDTO = response.Content.ReadAsAsync<IEnumerable<ProyectoDTO>>().Result;
                        listViewProyects.ItemsSource = ProyectoDTO;
                    }
                }
            }catch(Exception e)
            {
                throw e;
            }
        }

        private void btn_Turno(object sender, RoutedEventArgs e)
        {
            var a = listViewProyects.SelectedItem as ProyectoDTO;
            if (a!=null)
            {
                this.NavigationService.Navigate(new ProyTurnList(a.id));
            }
            
            
        }
        private void btn_Fechas(object sender,RoutedEventArgs e)
        {
            var a = listViewProyects.SelectedItem as ProyectoDTO;
            if (a!=null)
            {
                this.NavigationService.Navigate(new ProyFechaList(a.id));
            }
        }
        private void generate(object sender, RoutedEventArgs e)
        {
            // /planificaciones/{ idProyecto}
            var a = listViewProyects.SelectedItem as ProyectoDTO;
            if (a!=null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PostAsJsonAsync("api/planificaciones/{0}", a.id).Result;
                }
            }
        }

        private void addProyecto(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProIns(null));
        }
        private void deleteProyect(object sender, RoutedEventArgs e)
        {
            var a = listViewProyects.SelectedItem as ProyectoDTO;
            if (a!=null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    var response = client.DeleteAsync(String.Format("api/proyectos/{0}",a.id)).Result;
                    listProyects();
             
                    
                    
                }
                
            }
        }
        private void updateProyects(object sender, RoutedEventArgs e)
        {
            listProyects();
        }

        private void editProyects(object sender,RoutedEventArgs e)
        {
            var a = listViewProyects.SelectedItem as ProyectoDTO;
            if (a!=null)
            {
                this.NavigationService.Navigate(new ProIns(a.id));
            }

        }
    }
}
