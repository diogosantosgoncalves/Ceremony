using Ceremony.Model;
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

        public void Abre_Menu_Clientes(object sender, RoutedEventArgs e)
        {
            Cadastro_Cliente tela = new Cadastro_Cliente();
            tela.Show();
        }

        public void Abre_Menu_Pacotes(object sender, RoutedEventArgs e)
        {
            Tela_Pacotes tela = new Tela_Pacotes();
            tela.Show();
        }

        public void Abre_Menu_Servicos_Cerimonia(object sender, RoutedEventArgs e)
        {
            Tela_Servicos_Cerimonia tela = new Tela_Servicos_Cerimonia();
            tela.Show();
        }
        public void Abre_Menu_Cerimonial(object sender, RoutedEventArgs e)
        {
            Tela_Cerimonial tela = new Tela_Cerimonial();
            tela.Show();
        }
        public void Abre_Menu_Buffet(object sender, RoutedEventArgs e)
        {
            Tela_Buffet tela = new Tela_Buffet();
            tela.Show();
        }
        public void Abre_Menu_Consulta_Cliente(object sender, RoutedEventArgs e)
        {
            Tela_Consulta_Cliente tela = new Tela_Consulta_Cliente();
            tela.Show();
        }
        public void Abre_Menu_Consulta_Buffet(object sender, RoutedEventArgs e)
        {
            Tela_Consulta_Buffet tela = new Tela_Consulta_Buffet();
            tela.Show();
        }
        public void Abre_Menu_Consulta_Cerimonial(object sender, RoutedEventArgs e)
        {
            Tela_Consulta_Cerimonial tela = new Tela_Consulta_Cerimonial();
            tela.Show();
        }
        public void Abre_Menu_Tipos_Eventos(object sender, RoutedEventArgs e)
        {
            Tela_Tipo_Evento tela = new Tela_Tipo_Evento();
            tela.Show();
        }

        private void Abre_Menu_Consultar_Pacotes(object sender, RoutedEventArgs e)
        {
            Tela_Consulta_Pacote tela = new Tela_Consulta_Pacote();
            tela.Show();
        }

        private void Abre_Menu_Consultar_Servicos(object sender, RoutedEventArgs e)
        {
            Tela_Consulta_Pacote_Servicos tela = new Tela_Consulta_Pacote_Servicos();
            tela.Show();
        }

        private void Abre_Menu_Consultar_Tipos_Eventos(object sender, RoutedEventArgs e)
        {
            Tela_Consulta_Tipo_Evento tela = new Tela_Consulta_Tipo_Evento();
            tela.Show();
        }
    }
}
