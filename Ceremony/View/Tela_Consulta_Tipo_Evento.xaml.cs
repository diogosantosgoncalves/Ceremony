using Ceremony.Dal;
using Ceremony.Model;
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
using System.Windows.Shapes;

namespace Ceremony.View
{
    /// <summary>
    /// Interaction logic for Tela_Consulta_Tipo_Evento.xaml
    /// </summary>
    public partial class Tela_Consulta_Tipo_Evento : Window
    {
        ServicesDBTipo_Evento servicesDBTipo_Evento = new ServicesDBTipo_Evento();
        public Tela_Consulta_Tipo_Evento()
        {
            InitializeComponent();
        }
        public void ConsultarCliente(object sender, RoutedEventArgs e)
        {
            dg_ConsultaTipo_Evento.ItemsSource = servicesDBTipo_Evento.Listar_Tipo_Evento(txt_nome.Text.ToString());
        }
        public void bt_Editar(object sender, RoutedEventArgs e)
        {
            Tipo_Evento tipo_Evento = servicesDBTipo_Evento.Editar(int.Parse(PegarLinhaGrid(0)));
            Tela_Tipo_Evento tela = new Tela_Tipo_Evento(tipo_Evento);
            tela.ShowDialog();
            dg_ConsultaTipo_Evento.ItemsSource = servicesDBTipo_Evento.Listar_Tipo_Evento(txt_nome.Text.ToString());

        }
        public void bt_TelaPermissaoUsuario(object sender, RoutedEventArgs e)
        {
            int codigo = int.Parse(PegarLinhaGrid(0));
            //TelaPermissaoUsuario tela1 = new TelaPermissaoUsuario(codigo, PegarLinhaGrid(1));
            //tela1.ShowDialog();
        }
        public void bt_ExcluiUsuario(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Deseja realmente excluir esse Usuário?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                // servicesDBTipo_Evento.Excluir(int.Parse(PegarLinhaGrid(0)));
                //MessageBox.Show(dBUsuario.Statusmessagem);
                //dg_ConsultaTipo_Evento.ItemsSource = servicesDBCliente.BuscarUsuario(txt_nomeUsuario.Text.ToString());
            }
        }
        public string PegarLinhaGrid(int linha)
        {
            var selectedItem = dg_ConsultaTipo_Evento.SelectedItem.ToString();
            Type t = dg_ConsultaTipo_Evento.SelectedItem.GetType();
            System.Reflection.PropertyInfo[] props = t.GetProperties();
            string propertyValue = props[linha].GetValue(dg_ConsultaTipo_Evento.SelectedItem, null).ToString();
            return propertyValue;
        }
    }
}
