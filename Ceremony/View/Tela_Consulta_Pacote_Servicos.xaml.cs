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
    /// Interaction logic for Tela_Consulta_Pacote_Servicos.xaml
    /// </summary>
    public partial class Tela_Consulta_Pacote_Servicos : Window
    {
        ServicesDBPacote_Servico servicesDBPacote_Servico = new ServicesDBPacote_Servico();
        public Tela_Consulta_Pacote_Servicos()
        {
            InitializeComponent();
        }
        public void ConsultarPacote_Servicos(object sender, RoutedEventArgs e)
        {
            dg_ConsultaPacote_Servico.ItemsSource = servicesDBPacote_Servico.Listar_Pacote_Servicos(txt_nome.Text.ToString());
        }
        public void bt_Editar(object sender, RoutedEventArgs e)
        {
            Pacote_Servicos pacote_servicos = servicesDBPacote_Servico.Editar(int.Parse(PegarLinhaGrid(0)));
            Tela_Servicos_Cerimonia tela = new Tela_Servicos_Cerimonia(pacote_servicos);
            tela.ShowDialog();
            dg_ConsultaPacote_Servico.ItemsSource = servicesDBPacote_Servico.Listar_Pacote_Servicos(txt_nome.Text.ToString());
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
                // servicesDBPacote_Servico.Excluir(int.Parse(PegarLinhaGrid(0)));
                //MessageBox.Show(dBUsuario.Statusmessagem);
                //dg_ConsultaPacote_Servico.ItemsSource = servicesDBCliente.BuscarUsuario(txt_nomeUsuario.Text.ToString());
            }
        }
        public string PegarLinhaGrid(int linha)
        {
            var selectedItem = dg_ConsultaPacote_Servico.SelectedItem.ToString();
            Type t = dg_ConsultaPacote_Servico.SelectedItem.GetType();
            System.Reflection.PropertyInfo[] props = t.GetProperties();
            string propertyValue = props[linha].GetValue(dg_ConsultaPacote_Servico.SelectedItem, null).ToString();
            return propertyValue;
        }

        private void bt_Confirmar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
