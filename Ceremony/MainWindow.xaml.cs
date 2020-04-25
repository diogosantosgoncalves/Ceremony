using Ceremony.View;
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

namespace Ceremony
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Abre_Menu_Clientes(object sender, RoutedEventArgs e)
        {
            Cadastro_Cliente tela = new Cadastro_Cliente();
            tela.Show();
        }

        private void Abre_Menu_Tipos_Servicos(object sender, RoutedEventArgs e)
        {
            Tela_Tipos_Servicos tela = new Tela_Tipos_Servicos();
            tela.Show();
        }

        private void Abre_Menu_Servicos(object sender, RoutedEventArgs e)
        {
            Tela_Servicos tela = new Tela_Servicos();
            tela.Show();
        }
    }
}
