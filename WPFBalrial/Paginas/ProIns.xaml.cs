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
    /// Lógica de interacción para ProIns.xaml
    /// </summary>
    public partial class ProIns : Page
    {
        private int? id;
        public ProIns(int? idProyecto)
        {
            this.id = idProyecto;
            InitializeComponent();
            if (this.id!=null)
            {
                converTextBoxWithValues((int)this.id);
            }
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProList());
        }
        private void btnAccept(object sender, RoutedEventArgs e)
        {
            ProyectoDTO proyectoDTO = new ProyectoDTO();
            proyectoDTO.nombre = textBlockName.Text;
            proyectoDTO.fechaInicio = textBlockFechaInicio.Text;
            proyectoDTO.fechaFin = textBlockFechaInicio.Text;
            resetTextFields();
            if (this.id==null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PostAsJsonAsync("api/proyectos", proyectoDTO).Result;
                }
            }
            else
            {
                proyectoDTO.id = (int)id;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PutAsJsonAsync(String.Format("api/proyectos/{0}",proyectoDTO.id), proyectoDTO).Result;
                }
                this.NavigationService.GoBack();
            }
            
            
        }
        private void converTextBoxWithValues(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(App.URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                var response = client.GetAsync(String.Format("api/proyectos/{0}", id)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var obj=response.Content.ReadAsAsync<ProyectoDTO>().Result;
                    textBlockName.Text = obj.nombre;
                    textBlockFechaInicio.Text = obj.fechaInicio;
                    textBlockFechaFin.Text = obj.fechaFin;
                }
            }
        }
        private void resetTextFields()
        {
            textBlockName.Text = "";
            textBlockFechaInicio.Text = "";
            textBlockFechaFin.Text = "";
        }
    }
}
