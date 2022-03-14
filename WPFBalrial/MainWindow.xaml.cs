﻿using System;
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
        }

        private void BtUsuarios_Click(object sender, RoutedEventArgs e)
        {
            UsuList pagina = new UsuList();
            frmPrincipal.Navigate(pagina);
        }
    }
}
