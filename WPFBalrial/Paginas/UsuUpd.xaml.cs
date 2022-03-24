using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para UsuUpd.xaml
    /// </summary>
    public partial class UsuUpd : Page
    {

        HttpClient client = new HttpClient();

        public UsuUpd(UsuarioDTO usuarioDTO)
        {
            InitializeComponent();

            // Inicializar eventos botones
            btSalir.Click += BtSalir_Click;
            btGuardar.Click += BtGuardar_Click;

            // Rellenar datos de la entidad a actualizar
            this.tbId.Text = usuarioDTO.id.ToString();
            this.tbNombre.Text = usuarioDTO.nombre;
            this.tbApellidos.Text = usuarioDTO.apellidos;
            this.tbLogin.Text = usuarioDTO.login;
            this.tbTelefono.Text = usuarioDTO.telefono;
            this.tbEmail.Text = usuarioDTO.email;
            this.tbCP.Text = usuarioDTO.cp.ToString();
            this.tbHoraInicio.Text = usuarioDTO.horaInicio;
            this.tbHoraFin.Text = usuarioDTO.horaFin;

            // Inicializar cliente
            client.BaseAddress = new Uri("https://www.galsoftpre.es/apibalrial/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
        }

        private void BtGuardar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarUsuario();
        }

        private void BtSalir_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        public void ActualizarUsuario()
        {
            String diasSemana = "";
            if (cbL.IsChecked == true)
            {
                diasSemana = diasSemana + "L";
            }

            if (cbM.IsChecked == true)
            {
                diasSemana = diasSemana + "M";
            }

            if (cbX.IsChecked == true)
            {
                diasSemana = diasSemana + "X";
            }
            if (cbJ.IsChecked == true)
            {
                diasSemana = diasSemana + "J";
            }
            if (cbV.IsChecked == true)
            {
                diasSemana = diasSemana + "V";
            }
            if (cbS.IsChecked == true)
            {
                diasSemana = diasSemana + "S";
            }
            if (cbD.IsChecked == true)
            {
                diasSemana = diasSemana + "D";
            }
            var usuarioDTO = new UsuarioDTO()
            {
                /*id = Int32.Parse(tbId.Text),
                nombre = tbNombre.Text,
                apellidos = tbApellidos.Text,
                login = tbLogin.Text,
                telefono = tbTelefono.Text,
                email = tbEmail.Text,
                cp = Int32.Parse(tbCP.Text),
                dias = diasSemana,
                horaInicio = tbHoraInicio.Text,
                horaFin = tbHoraFin.Text,
                disponibilidad = 1*/
            };

            usuarioDTO.id = Int32.Parse(tbId.Text);
            usuarioDTO.nombre = tbNombre.Text;
            usuarioDTO.apellidos = tbApellidos.Text;
            usuarioDTO.login = tbLogin.Text;
            usuarioDTO.telefono = tbTelefono.Text;

            // IsValidEmail(tbEmail.Text);
            if (!IsValidEmail(tbEmail.Text))
            {
                tbAvisos.Text = "Email incorrecto";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;
                return;
            }
            else
            {
                usuarioDTO.email = tbEmail.Text;
            }

            // IsValidCp(tbCP.Text);
            if (!IsValidCp(tbCP.Text))
            {
                tbAvisos.Text = "Codigo postal incorrecto";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;
                return;
            }
            else
            {
                usuarioDTO.cp = Int32.Parse(tbCP.Text);
            }

            usuarioDTO.dias = diasSemana;
            usuarioDTO.horaInicio = tbHoraInicio.Text;
            usuarioDTO.horaFin = tbHoraFin.Text;
            usuarioDTO.disponibilidad = 1;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8080/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PutAsJsonAsync("api/usuarios/"+ tbId.Text, usuarioDTO).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        tbAvisos.Text = " Actualizado correctamente";
                        tbAvisos.Foreground = Brushes.White;
                        tbAvisos.Background = Brushes.Green;
                    }
                    else
                    {
                        tbAvisos.Text = " Se ha producido un error";
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

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidCp(string cp)
        {
            int _val;

            if (cp.Length == 5)
            {
                bool valor = Int32.TryParse(cp, out _val);
                return valor;
            }
            else
            {
                return false;
            }
        }
    }
}
