using Ceremony.Dal;
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
    /// Interaction logic for Tela_Consulta_Pacote.xaml
    /// </summary>
    public partial class Tela_Consulta_Pacote : Window
    {
        ServicesDBPacote servicesDBPacote = new ServicesDBPacote();
        public Tela_Consulta_Pacote()
        {
            InitializeComponent();
        }
        public void ConsultarPacote(object sender, RoutedEventArgs e)
        {
            dg_ConsultaPacote.ItemsSource = servicesDBPacote.Listar_Pacote(txt_nome.Text.ToString());
        }
        public void bt_EditarUsuario(object sender, RoutedEventArgs e)
        {
            //Cliente cliente = servicesDBPacote.Editar(int.Parse(PegarLinhaGrid(0)));
            //Cadastro_Cliente tela = new Cadastro_Cliente(cliente);
            //tela.ShowDialog();
            //dg_ConsultaPacote.ItemsSource = servicesDBPacote.BuscarCliente(txt_nome.Text.ToString());

            //TelaCadastrarUsuario tela1 = new TelaCadastrarUsuario(usu.usu_nome, usu.usu_senha, usu.usu_id, usu.usu_inativo);
            //tela1.ShowDialog();
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
                //servicesDBPacote.Excluir(int.Parse(PegarLinhaGrid(0)));
                //MessageBox.Show(dBUsuario.Statusmessagem);
                //dg_ConsultaPacote.ItemsSource = servicesDBCliente.BuscarUsuario(txt_nomeUsuario.Text.ToString());
            }
        }
        public string PegarLinhaGrid(int linha)
        {
            var selectedItem = dg_ConsultaPacote.SelectedItem.ToString();
            Type t = dg_ConsultaPacote.SelectedItem.GetType();
            System.Reflection.PropertyInfo[] props = t.GetProperties();
            string propertyValue = props[linha].GetValue(dg_ConsultaPacote.SelectedItem, null).ToString();
            return propertyValue;
        }
    }
}
