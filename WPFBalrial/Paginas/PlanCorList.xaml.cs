using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para PlanCorList.xaml
    /// </summary>
    public partial class PlanCorList : Page
    {
        public PlanCorList()
        {
            InitializeComponent();


            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/coordinador/2/planificaciones").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<PlanificacionDTO> planificaciones = response.Content.ReadAsAsync<IEnumerable<PlanificacionDTO>>().Result;
                        dt.ItemsSource = planificaciones;
                    }
                }



            } catch
            {

            }
        }

        private void AsignarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Button obj = ((FrameworkElement)sender).DataContext as Button;

            Trace.WriteLine((dt.SelectedItem as PlanificacionDTO).id);

            this.NavigationService.Navigate(new PlanCorIns((dt.SelectedItem as PlanificacionDTO).id));
        }
    }
}
