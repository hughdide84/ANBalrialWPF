using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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
    /// Lógica de interacción para InfProyecto.xaml
    /// </summary>
    public partial class InfProyecto : Page
    {

        private IEnumerable<PlanificacionDTO> datosGr = new List<PlanificacionDTO>();

        private int idProyecto = 1;
        public InfProyecto()
        {
            InitializeComponent();
            CargarBarras();
        }

        private void CargarBarras()
        {
            var model = new PlotModel();

            var s1 = new BarSeries { Title = "Necesarios", FillColor = OxyColors.IndianRed, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s1.Items.Add(new BarItem { Value = 19 });
            s1.Items.Add(new BarItem { Value = 22 });
            s1.Items.Add(new BarItem { Value = 18 });

            var s2 = new BarSeries { Title = "Asignados", FillColor = OxyColors.Green, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s2.Items.Add(new BarItem { Value = 12 });
            s2.Items.Add(new BarItem { Value = 13 });
            s2.Items.Add(new BarItem { Value = 15 });

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
            categoryAxis.Labels.Add("10:00-12:00");
            categoryAxis.Labels.Add("12:00-10:00");
            categoryAxis.Labels.Add("14:00-16:00");

            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
            model.Series.Add(s2);
            model.Series.Add(s1);
            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);

            this.grBarras.Model = model;
        }

        public void ConsultarDatos()
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/proyectos/"+ idProyecto.ToString()+ "/planificaciones").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        datosGr = response.Content.ReadAsAsync<IEnumerable<PlanificacionDTO>>().Result;

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
