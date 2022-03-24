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
    /// Lógica de interacción para UsuIns.xaml
    /// </summary>
    public partial class UsuIns : Page
    {
        public UsuIns()
        {
            InitializeComponent();
            btSalir.Click += BtSalir_Click;
            btGuardar.Click += BtGuardar_Click;
        }

        private void BtGuardar_Click(object sender, RoutedEventArgs e)
        {
            InsertarUsuario();
        }

        private void BtSalir_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        public void InsertarUsuario()
        {

            String diasSemana = "";
            if (cbL.IsChecked == true)
            {
                diasSemana = diasSemana + "L";
            }

            if(cbM.IsChecked== true)
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
                /*id = 0,
                nombre = tbNombre.Text,
                apellidos = tbApellidos.Text,
                login = tbLogin.Text,
                telefono = tbTelefono.Text,
                email = tbEmail.Text,
                cp = Int32.Parse(tbCP.Text),
                telegramId = Int32.Parse(tbTelegramId.Text),
                dias = diasSemana,
                horaInicio = tbHoraInicio.Text,
                horaFin = tbHoraFin.Text,
                disponibilidad = 1*/
            };

            usuarioDTO.id = 0;
            usuarioDTO.nombre = tbNombre.Text;
            usuarioDTO.apellidos = tbApellidos.Text;
            usuarioDTO.login = tbLogin.Text;
            usuarioDTO.telefono = tbTelefono.Text;

            // IsValidEmail(tbEmail.Text);
            if (!IsValidEmail(tbEmail.Text))
            {
                ResetearAviso();
                tbAvisos.Text = "Email incorrecto";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;
                tbAvisos.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                usuarioDTO.email = tbEmail.Text;
            }

            // IsValidCp(tbCP.Text);
            if (!IsValidCp(tbCP.Text))
            {
                ResetearAviso();
                tbAvisos.Text = "Codigo postal incorrecto";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;
                tbAvisos.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                usuarioDTO.cp = Int32.Parse(tbCP.Text);
            }

            usuarioDTO.dias = diasSemana;

            if (!IsValidHoraIni(tbHoraInicio.Text))
            {
                ResetearAviso();
                tbAvisos.Text = "Hora de Inicio incorrecta";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;
                tbAvisos.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                usuarioDTO.horaInicio = tbHoraInicio.Text;
            }

            
            if (!IsValidHoraFin(tbHoraFin.Text))
            {
                ResetearAviso();
                tbAvisos.Text = "Hora de Fin incorrecta";
                tbAvisos.Foreground = Brushes.White;
                tbAvisos.Background = Brushes.Crimson;
                tbAvisos.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                usuarioDTO.horaFin = tbHoraFin.Text;
            }

            /*usuarioDTO.horaInicio = tbHoraInicio.Text;
            usuarioDTO.horaFin = tbHoraFin.Text;*/
            usuarioDTO.disponibilidad = 1;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(App.URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = client.PostAsJsonAsync("api/usuarios", usuarioDTO).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        ResetearAviso();
                        tbAvisos.Text = "Registrado correctamente";
                        tbAvisos.Foreground = Brushes.White;
                        tbAvisos.Background = Brushes.Green;
                        tbAvisos.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ResetearAviso();
                        tbAvisos.Text = "Se ha producido un error";
                        tbAvisos.Foreground = Brushes.White;
                        tbAvisos.Background = Brushes.Crimson;
                        tbAvisos.Visibility = Visibility.Visible;
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

        public static bool IsValidHoraIni(string inicio)
        {
            Regex checktime = new Regex(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");
            return checktime.IsMatch(inicio);
        }
        public static bool IsValidHoraFin(string fin)
        {
            Regex checktime = new Regex(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");
            return checktime.IsMatch(fin);
        }

        private void ResetearAviso()
        {
            tbAvisos.Text = "";
            tbAvisos.Foreground = Brushes.White;
            tbAvisos.Background = Brushes.White;
            tbAvisos.Visibility = Visibility.Collapsed;
        }
    }
}
