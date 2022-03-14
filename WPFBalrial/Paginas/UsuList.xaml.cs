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
    /// Lógica de interacción para UsuList.xaml
    /// </summary>
    public partial class UsuList : Page
    {
        public UsuList()
        {
            InitializeComponent();
            ObtenerUsuariosAsync();
            this.btInsertar.Click += BtInsertar_Click;
        }

        private void BtInsertar_Click(object sender, RoutedEventArgs e)
        {
            UsuIns usuIns = new UsuIns();
            this.NavigationService.Navigate(usuIns);
        }

        public void ObtenerUsuariosAsync()
        {

            try
            {
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");

                    // Setting content type.  
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // HTTP GET  
                    HttpResponseMessage response = client.GetAsync("api/usuarios").Result;
                    // response = await client.PostAsJsonAsync("api/WebApi/PostRegInfo", requestObj).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var usuarios = response.Content.ReadAsAsync<IEnumerable<UsuarioDTO>>().Result;
                        lvUsuarios.ItemsSource = usuarios;

                    }
                    else
                    {
                        MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                    }
                    /*
                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonConvert.DeserializeObject<RegInfoResponseObj>(result);

                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj.code = 602;
                    }
                    */
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
