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
        int idPlan;

        public PlanCorIns(int idPlan)
        {
            this.idPlan = idPlan;

            InitializeComponent();

        }


        public void SetAlgo(int idPlan)
        {
            this.idPlan = idPlan;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/planificaciones/" + idPlan + "/usuariosAsignados").Result;

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
            Button obj = ((FrameworkElement)sender).DataContext as Button;

            int id = (dtAsignados.SelectedItem as PlanificacionDTO).id;
            Trace.WriteLine(id);


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                HttpResponseMessage response = client.DeleteAsync("api/planificacionUsuario/" + id).Result;
            }

            SetAlgo(id);
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Button obj = ((FrameworkElement)sender).DataContext as Button;

            int id = (dtDisponibles.SelectedItem as PlanificacionDTO).id;
            Trace.WriteLine(id);


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                HttpResponseMessage response = client.PostAsync("api/planificacionUsuario/" + id, null).Result;
            }

            SetAlgo(id);
        }

    }
}
