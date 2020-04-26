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
    /// Interaction logic for Tela_Cerimonial.xaml
    /// </summary>
    public partial class Tela_Cerimonial : Window
    {
        public Tela_Cerimonial()
        {
            InitializeComponent();
            ServicesDBTipo_Evento servicesDBTipo_Evento = new ServicesDBTipo_Evento();
            cb_evento.ItemsSource = servicesDBTipo_Evento.Listar_Tipo_Evento();
        }

        private void bt_salvar_Click(object sender, RoutedEventArgs e)
        {
            Cerimonia cerimonia = new Cerimonia();
            cerimonia.cerimonia_data_evento = DateTime.Parse(txt_data.Text);
            //
            //cerimonia.cerimonia_cidade_local = txt_cidade_local.Text;
            //cerimonia.cerimonia_total_convidados = txt_convidados.Text;
            //cerimonia.cerimonia_horario_cerimonia = txt_horario_cerimonia.Text;
            //cerimonia.cerimonia_inicio_festa = txt_horario_festa.Text;
            //cerimonia.cerimonia_valor_parcelas = txt_valor_das_parcelas.Text;
            cerimonia.cerimonia_data_primeiro_vencimento = DateTime.Parse(txt_data_primeiro_vencimento.Text);
            //cerimonia.cerimonia_num_parcelas = txt_parcelas.Text;
            //cerimonia.cerimonia_valor_total = txt_valor_prestacao_do_servico.Text;
            
        }
    }
}
