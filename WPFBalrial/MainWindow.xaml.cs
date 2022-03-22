using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using WPFBalrial.Paginas;
using WPFBalrial.DTOs;

namespace WPFBalrial
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btUsuarios.Click += BtUsuarios_Click;
            btAcceder.Click += BtAcceder_Click;
            btEntidades.Click += BtEntidades_Click;
            btProyectos.Click += BtProyectos_Click;
            pnlLogin.Visibility = Visibility.Visible;
            pnlMenu.Visibility = Visibility.Hidden;

            IniLogo logoFrame = new IniLogo();
            frmPrincipal.Navigate(logoFrame);
        }

        private void BtProyectos_Click(object sender, RoutedEventArgs e)
        {
            ProList selFrame = new ProList();
            frmPrincipal.Navigate(selFrame);
        }

        private void BtEntidades_Click(object sender, RoutedEventArgs e)
        {
            EntList selFrame = new EntList();
            frmPrincipal.Navigate(selFrame);
        }

        private void BtAcceder_Click(object sender, RoutedEventArgs e)
        {
            
            var loginDTO = new LoginDTO()
            {
                login = tbUsuario.Text,
                password = tbPassword.Text
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PostAsJsonAsync("api/login", loginDTO).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        pnlMenu.Visibility = Visibility.Visible;
                        pnlLogin.Visibility = Visibility.Collapsed;
                        UsuarioDTO usuario = response.Content.ReadAsAsync<UsuarioDTO>().Result;
                        Console.WriteLine(usuario.id);

                        //Pintar botones
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode);
                        MessageBox.Show("Se ha producido un error");
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
        private void PintarBotones()
        {
            //switch para poder printear los botones necesarios
        }

        private void BtUsuarios_Click(object sender, RoutedEventArgs e)
        {
            UsuList selFrame = new UsuList();
            frmPrincipal.Navigate(selFrame);
        }
    }
}
