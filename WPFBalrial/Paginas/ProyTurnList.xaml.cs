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
    public partial class ProyTurnList : Page
    {
        private int id;
        public ProyTurnList(int id)
        {
            this.id = id;
            InitializeComponent();

            listTurn(id);
            
            
            
        }

        private void listTurn(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync(String.Format("api/proyectos/{0}/turnos", id)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<ProyTurnoDTO> ProyectoDTO = response.Content.ReadAsAsync<IEnumerable<ProyTurnoDTO>>().Result;
                        //listViewProyects.ItemsSource = ProyectoDTO;
                        listViewProyects.ItemsSource = ProyectoDTO;
                    }
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void btn_edit(object sender, RoutedEventArgs e)
        {
            var a = listViewProyects.SelectedItem as ProyTurnoDTO;
            if (a!=null)
            {
                this.NavigationService.Navigate(new ProyTurnIns(a.idProyecto, a.id));
            }
            
        }
        private void btn_add(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProyTurnIns(this.id,null));
        }
        private void btn_remove(object sender, RoutedEventArgs e)
        {
            var a = listViewProyects.SelectedItem as ProyTurnoDTO;
            if (a != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    var response = client.DeleteAsync(String.Format("api/turnoproyectos/{0}", a.id)).Result;
                    
                    listTurn(this.id);
                }
                

            }
        }
        private void btn_update(object sender, RoutedEventArgs e)
        {
            listTurn(this.id);
        }
    }
}
