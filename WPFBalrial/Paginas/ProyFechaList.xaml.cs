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
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class ProyFechaList : Page
    {
        private int id;
        public ProyFechaList(int id)
        {
            this.id = id;
            InitializeComponent();
            listProyFechas();

        }

        public void listProyFechas()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(App.URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                HttpResponseMessage response = client.GetAsync(String.Format("api/proyectos/{0}/fechas", id)).Result;
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<ProyFechaDTO> ProyectoDTO = response.Content.ReadAsAsync<IEnumerable<ProyFechaDTO>>().Result;
                    listViewProyects.ItemsSource=ProyectoDTO;
                }
            }
        }

        private void addBtn(object sender, RoutedEventArgs e)
        {
            
            this.NavigationService.Navigate(new ProyFechaIns(this.id,null));
            
        }
        private void editBtn(object sender,RoutedEventArgs e)
        {
            var a = listViewProyects.SelectedItem as ProyFechaDTO;
            if (a!=null)
            {
                this.NavigationService.Navigate(new ProyFechaIns(a.idProyecto, a.id));
            }
        }
        private void deleteBtn(object sender, RoutedEventArgs e)
        {
            var a = listViewProyects.SelectedItem as ProyFechaDTO;
            if (a != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    var response = client.DeleteAsync(String.Format("api/proyfechas/{0}", a.id)).Result;
                    listProyFechas();



                }

            }
        }
        private void updateBtn(object sender, RoutedEventArgs e)
        {
            listProyFechas();
        }
    }
}
