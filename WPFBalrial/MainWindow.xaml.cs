using System;
using System.Collections.Generic;
using System.Linq;
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

            // Para pruebas cambiar aqui
            InfProyecto selFrame = new InfProyecto();
            frmPrincipal.Navigate(selFrame);
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
            pnlLogin.Visibility = Visibility.Collapsed;
            pnlMenu.Visibility = Visibility.Visible;
        }

        private void BtUsuarios_Click(object sender, RoutedEventArgs e)
        {
            UsuList selFrame = new UsuList();
            frmPrincipal.Navigate(selFrame);
        }
    }
}
