using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
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

        private int idProyecto = 1;
        public InfProyecto()
        {
            InitializeComponent();
            
            
        }

     

        private void CargarTurnos()
        {
           var model = new PlotModel();

            model.Legends.Add(new Legend()
            {
                LegendTitle = " ",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal
            });

            
            

            var s1 = new BarSeries { Title = "Necesarios", FillColor = OxyColors.IndianRed, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            var s2 = new BarSeries { Title = "Asignados", FillColor = OxyColors.LightGreen, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };



            

            // Llamada a endpoint para obtener los necesarios
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/informes/proyecto/"+ idProyecto.ToString() + "/solicitadosTurno").Result;

                    if (response.IsSuccessStatusCode)
                    {                   
                        IEnumerable<InformeDTO> resultados = response.Content.ReadAsAsync<IEnumerable<InformeDTO>>().Result;

                        foreach(InformeDTO informeDTO in resultados)
                        {
                            categoryAxis.Labels.Add(informeDTO.dato);
                            s1.Items.Add(new BarItem { Value = informeDTO.valor });
                        }
                        s1.LabelPlacement = LabelPlacement.Inside;
                        s1.LabelFormatString = "{0}";
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

            // Llamada a endpoint para obtener los asignados
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/informes/proyecto/" + idProyecto.ToString() + "/ocupacionTurno").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<InformeDTO> resultados = response.Content.ReadAsAsync<IEnumerable<InformeDTO>>().Result;

                        foreach (InformeDTO informeDTO in resultados)
                        {
                            s2.Items.Add(new BarItem { Value = informeDTO.valor });
                        }
                        s2.LabelPlacement = LabelPlacement.Inside;
                        s2.LabelFormatString = "{0}";
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





            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
            model.Series.Add(s2);
            model.Series.Add(s1);


            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);

            this.grBarras.Model = model;

           

        }



        private void CargarFechas()
        {
            var model = new PlotModel();

      

            model.Legends.Add(new Legend()
            {
                LegendTitle = " ",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal
            });

            var s1 = new BarSeries { Title = "Necesarios", FillColor = OxyColors.IndianRed, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            var s2 = new BarSeries { Title = "Asignados", FillColor = OxyColors.LightGreen, StrokeColor = OxyColors.Black, StrokeThickness = 1 };

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };

            // Llamada a endpoint para obtener las necesarios
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/informes/proyecto/" + idProyecto.ToString() + "/solicitadasFecha").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<InformeDTO> resultados = response.Content.ReadAsAsync<IEnumerable<InformeDTO>>().Result;

                        foreach (InformeDTO informeDTO in resultados)
                        {
                            categoryAxis.Labels.Add(informeDTO.dato);
                            s1.Items.Add(new BarItem { Value = informeDTO.valor });
                        }
                        s1.LabelPlacement = LabelPlacement.Inside;
                        s1.LabelFormatString = "{0}";
                    }
                    else
                    {
                        tbAvisos.Text = "Se ha producido un error";
                        tbAvisos.Foreground = Brushes.White;
                        tbAvisos.Background = Brushes.Crimson;
                    }
                }
            }catch (Exception ex)
            {
                throw ex;
            }
            // Llamada a endpoint para obtener las asignados
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/informes/proyecto/" + idProyecto.ToString() + "/ocupacionFecha").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<InformeDTO> resultados = response.Content.ReadAsAsync<IEnumerable<InformeDTO>>().Result;

                        foreach (InformeDTO informeDTO in resultados)
                        {

                            s2.Items.Add(new BarItem { Value = informeDTO.valor });
                            s2.LabelPlacement = LabelPlacement.Inside;
                            s2.LabelFormatString = "{0}";
                        }
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
            
            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
            model.Series.Add(s2);
            model.Series.Add(s1);

            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);

            this.grBarras.Model = model;
        }


        private void CargarUbicaciones()
        {
            var model = new PlotModel();



            model.Legends.Add(new Legend()
            {
                LegendTitle = " ",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal
            });

            var s1 = new BarSeries { Title = "Necesarios", FillColor = OxyColors.IndianRed, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            var s2 = new BarSeries { Title = "Asignados", FillColor = OxyColors.LightGreen, StrokeColor = OxyColors.Black, StrokeThickness = 1 };

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };

            // Llamada a endpoint para obtener las necesarios
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/informes/proyecto/" + idProyecto.ToString() + "/solicitadasUbicaciones").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<InformeDTO> resultados = response.Content.ReadAsAsync<IEnumerable<InformeDTO>>().Result;

                        foreach (InformeDTO informeDTO in resultados)
                        {
                            categoryAxis.Labels.Add(informeDTO.dato);
                            s1.Items.Add(new BarItem { Value = informeDTO.valor });
                        }
                        s1.LabelPlacement = LabelPlacement.Inside;
                        s1.LabelFormatString = "{0}";
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
            // Llamada a endpoint para obtener las asignados
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/informes/proyecto/" + idProyecto.ToString() + "/ocupacionUbicacion").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<InformeDTO> resultados = response.Content.ReadAsAsync<IEnumerable<InformeDTO>>().Result;

                        foreach (InformeDTO informeDTO in resultados)
                        {

                            s2.Items.Add(new BarItem { Value = informeDTO.valor });
                        }
                        s2.LabelPlacement = LabelPlacement.Inside;
                        s2.LabelFormatString = "{0}";
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


            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
            model.Series.Add(s2);
            model.Series.Add(s1);

            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);

            this.grBarras.Model = model;
        }

        private void CargarUbicacionesPorcentaje()
        {
            var model = new PlotModel();



            model.Legends.Add(new Legend()
            {
                LegendTitle = " ",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal
            });

            var s1 = new BarSeries { Title = "Porcentaje Cumplimiento", FillColor = OxyColors.LightGreen, StrokeColor = OxyColors.Black, StrokeThickness = 1 , };
            

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };

            // Llamada a endpoint para obtener las porcentaje ubicaciones
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.GetAsync("api/informes/proyecto/" + idProyecto.ToString() + "/porcentajesUbicaciones").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<InformeDTO> resultados = response.Content.ReadAsAsync<IEnumerable<InformeDTO>>().Result;

                        foreach (InformeDTO informeDTO in resultados)
                        {
                            categoryAxis.Labels.Add(informeDTO.dato);
                            s1.Items.Add(new BarItem { Value = informeDTO.valor});
                        }
                        s1.LabelPlacement = LabelPlacement.Inside;
                        s1.LabelFormatString = "{0}%";
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








            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 , Maximum = 100};
            model.Series.Add(s1);

            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);

            this.grBarras.Model = model;
        }

        private void btInfTurnos_Click(object sender, RoutedEventArgs e)
        {
             CargarTurnos();
        }

        private void btInfFechas_Click(object sender, RoutedEventArgs e)
        {
             CargarFechas();
        }

        private void btInfUbicaciones_Click(object sender, RoutedEventArgs e)
        {
            CargarUbicaciones();
        }

        private void btInfUbicacionesPor_Click(object sender, RoutedEventArgs e)
        {
            CargarUbicacionesPorcentaje();
        }
    }
}



