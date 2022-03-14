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
            btCancelar.Click += BtCancelar_Click;
        }

        private void BtCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
    }
}
