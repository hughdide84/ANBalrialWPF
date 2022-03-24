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
    /// Lógica de interacción para PlanCorIns.xaml
    /// </summary>
    public partial class PlanCorIns : Page
    {
        public PlanCorIns(int idPlan)
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
                    HttpResponseMessage response = client.GetAsync("api/planificaciones/"+ idPlan + "/usuariosAsignados").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<UsuarioDTO> planificaciones = response.Content.ReadAsAsync<IEnumerable<UsuarioDTO>>().Result;

                        Trace.WriteLine(planificaciones.ToArray()[0]);
                        Trace.WriteLine(idPlan);
                        dtAsignados.ItemsSource = planificaciones;
                    }
                }


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/planificaciones/" + idPlan + "/usuariosDisponibles").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<UsuarioDTO> planificaciones = response.Content.ReadAsAsync<IEnumerable<UsuarioDTO>>().Result;

                        Trace.WriteLine(planificaciones.ToArray()[0]);
                        Trace.WriteLine(idPlan);
                        dtAsignados.ItemsSource = planificaciones;
                    }
                }
            }
            catch
            {

            }
        }

        private void EliminarButto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
