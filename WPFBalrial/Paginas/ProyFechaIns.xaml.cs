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
    public partial class ProyFechaIns : Page
    {
        private int idProyecto;
        private int? id;
        public ProyFechaIns(int idProyecto,int? id)
        {
            this.id = id;
            this.idProyecto = idProyecto;
            InitializeComponent();
            if (id!=null)
            {
                setterValuesTextbox();
            }
        }
        private void setterValuesTextbox()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(App.URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                HttpResponseMessage response = client.GetAsync(String.Format("api/proyfechas/{0}", this.id)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var x = response.Content.ReadAsAsync<ProyFechaDTO>().Result;
                    textBoxFecha.Text = x.fecha;
                   
                }
            }
        }

        
        private void btnAccept(object sender, RoutedEventArgs e)
        {
            var a = new ProyFechaDTO();
            a.idProyecto = this.idProyecto;
            a.fecha = textBoxFecha.Text;
            textBoxFecha.Text = "";
            if (this.id==null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PostAsJsonAsync("api/proyfechas", a).Result;
                }
            }
            else
            {
                a.id = (int)id;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PutAsJsonAsync("api/proyfechas/"+a.id, a).Result;
                }
                this.NavigationService.GoBack();
            }
        }
        private void btn_cancel(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
