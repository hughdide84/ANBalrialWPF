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
    public partial class ProyTurnIns : Page
    {
        private int idProyecto;
        private int? id;
        public ProyTurnIns(int idProyecto,int? id)
        {
            this.id = id;
            this.idProyecto = idProyecto;
            InitializeComponent();
            if (this.id!=null)
            {
                setTextBoxWithValues((int)this.id);
            }
        }

        private void btn_accept(object sender, RoutedEventArgs e)
        {
            var proy = new ProyTurnoDTO();
            proy.idProyecto = this.idProyecto;
            proy.horaInicio = textBoxInicio.Text;
            proy.horaFin = textBoxFin.Text;
            resetTextBox();
            if (this.id==null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PostAsJsonAsync("api/turnoproyectos", proy).Result;
                }
            }
            else
            {
                proy.id = (int)this.id;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PutAsJsonAsync(String.Format("api/turnoproyectos/{0}", proy.id), proy).Result;
                }
                this.NavigationService.GoBack();
            }
        }
        private void btn_cancel(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
        private void resetTextBox()
        {
            textBoxFin.Text = "";
            textBoxInicio.Text = "";
        }
        private void setTextBoxWithValues(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(App.URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                HttpResponseMessage response = client.GetAsync("api/turnoproyectos/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var x = response.Content.ReadAsAsync<ProyTurnoDTO>().Result;
                    textBoxFin.Text = x.horaFin;
                    textBoxInicio.Text = x.horaInicio;
                   
                }
            }
        }
        
    }
}
