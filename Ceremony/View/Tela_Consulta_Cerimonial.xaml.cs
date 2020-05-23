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
    /// Interaction logic for Tela_Consulta_Cerimonial.xaml
    /// </summary>
    public partial class Tela_Consulta_Cerimonial : Window
    {
        ServicesDBCerimonia servicesDBCerimonia = new Dal.ServicesDBCerimonia();
        public Tela_Consulta_Cerimonial()
        {
            InitializeComponent();
            txt_cliente.Focus();
        }

        private void bt_pesquisar_Click(object sender, RoutedEventArgs e)
        {
            servicesDBCerimonia.Buscar_Cerimonia_Por_Nome(txt_cliente.Text);
        }
        public void ConsultarCerimonia(object sender, RoutedEventArgs e)
        {
            dg_ConsultaCerimonia.ItemsSource = servicesDBCerimonia.Buscar_Cerimonia_Por_Nome(txt_cliente.Text);
        }
        public void bt_EditarCerimonia(object sender, RoutedEventArgs e)
        {
            Cerimonia cerimonia = servicesDBCerimonia.Editar(int.Parse(PegarLinhaGrid(0)));
            Tela_Cerimonial tela = new Tela_Cerimonial(cerimonia);
            tela.ShowDialog();
            //dg_ConsultaCliente.ItemsSource = servicesDBCerimonia.BuscarCliente(txt_nome.Text.ToString());

            //TelaCadastrarUsuario tela1 = new TelaCadastrarUsuario(usu.usu_nome, usu.usu_senha, usu.usu_id, usu.usu_inativo);
            //tela1.ShowDialog();
        }
        public void bt_ImprimirCerimonia(object sender, RoutedEventArgs e)
        {
            //Cerimonia cerimonia = servicesDBCerimonia.Editar(int.Parse(PegarLinhaGrid(0)));
            //Tela_Cerimonial tela = new Tela_Cerimonial(cerimonia);
            //tela.ShowDialog();
            RelatorioCerimonia tela = new RelatorioCerimonia();
            tela.ShowDialog();
            //dg_ConsultaCliente.ItemsSource = servicesDBCerimonia.BuscarCliente(txt_nome.Text.ToString());

            //TelaCadastrarUsuario tela1 = new TelaCadastrarUsuario(usu.usu_nome, usu.usu_senha, usu.usu_id, usu.usu_inativo);
            //tela1.ShowDialog();
        }
        public void bt_TelaPermissaoUsuario(object sender, RoutedEventArgs e)
        {
            int codigo = int.Parse(PegarLinhaGrid(0));
            //TelaPermissaoUsuario tela1 = new TelaPermissaoUsuario(codigo, PegarLinhaGrid(1));
            //tela1.ShowDialog();
        }
        public void bt_ExcluiCerimonia(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Deseja realmente excluir esse Usuário?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                //servicesDBCerimonia.Excluir(int.Parse(PegarLinhaGrid(0)));
                //MessageBox.Show(dBUsuario.Statusmessagem);
                //dg_ConsultaCliente.ItemsSource = servicesDBCliente.BuscarUsuario(txt_nomeUsuario.Text.ToString());
            }
        }
        public string PegarLinhaGrid(int linha)
        {
            var selectedItem = dg_ConsultaCerimonia.SelectedItem.ToString();
            Type t = dg_ConsultaCerimonia.SelectedItem.GetType();
            System.Reflection.PropertyInfo[] props = t.GetProperties();
            string propertyValue = props[linha].GetValue(dg_ConsultaCerimonia.SelectedItem, null).ToString();
            return propertyValue;
        }
    }
}
