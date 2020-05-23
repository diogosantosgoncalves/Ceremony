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
        double total = 0;
        ServicesDBCerimonia servicesDBCerimonia = new ServicesDBCerimonia();
        public Tela_Cerimonial()
        {
            InitializeComponent();
            ServicesDBTipo_Evento servicesDBTipo_Evento = new ServicesDBTipo_Evento();
            cb_evento.ItemsSource = servicesDBTipo_Evento.Listar_Tipo_Evento();
            ServicesDBPacote servicesDBPacote = new ServicesDBPacote();
            cb_pacote.ItemsSource = servicesDBPacote.Listar_Pacote();
        }
        public Tela_Cerimonial(Cerimonia cerimonia)
        {
            InitializeComponent();
            
            ServicesDBTipo_Evento servicesDBTipo_Evento = new ServicesDBTipo_Evento();
            cb_evento.ItemsSource = servicesDBTipo_Evento.Listar_Tipo_Evento();
            ServicesDBPacote servicesDBPacote = new ServicesDBPacote();
            cb_pacote.ItemsSource = servicesDBPacote.Listar_Pacote();
            

            txt_cliente.Text = cerimonia.cerimonia_cliente_id.ToString();
            txt_data.Text = cerimonia.cerimonia_data_evento.ToString();

            txt_cidade_local.Text = cerimonia.cerimonia_cidade_local;
            txt_convidados.Text = cerimonia.cerimonia_total_convidados.ToString();
            txt_horario_cerimonia.Text = cerimonia.cerimonia_horario_cerimonia;
            txt_horario_festa.Text = cerimonia.cerimonia_inicio_festa;
            txt_valor_prestacao_do_servico.Text = cerimonia.cerimonia_valor_total.ToString();

            txt_parcelas.Text = cerimonia.cerimonia_num_parcelas.ToString();
            txt_valor_das_parcelas.Text = cerimonia.cerimonia_valor_parcelas.ToString();
            txt_data_primeiro_vencimento.Text = cerimonia.cerimonia_data_primeiro_vencimento.ToString();
            txt_horario_festa.Text = cerimonia.cerimonia_inicio_festa.ToString();
            txt_observacao.Text = cerimonia.cerimonia_observacao;
            cb_evento.SelectedValue = cerimonia.cerimonia_tipo_evento_id;
            //Convert.ToInt32(cerimonia.cerimonia_pacote_id);
            CarregarComboValor(Convert.ToInt32(cerimonia.cerimonia_pacote_id));
            //cb_pacote.SelectedValue = cerimonia.cerimonia_pacote_id;
            txt_Desconto.Text = cerimonia.cerimonia_desconto.ToString();

        }

        public void bt_salvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cerimonia cerimonia = new Cerimonia();

                if (string.IsNullOrEmpty(txt_cliente.Text))
                {
                    MessageBox.Show("Selecione um Cliente!");
                    return;
                }
                else
                    cerimonia.cerimonia_cliente_id = int.Parse(txt_cliente.Text);
                
                if (string.IsNullOrEmpty(txt_data.Text))
                {
                    MessageBox.Show("Selecione a Data do Evento!");
                    return;
                }
                else 
                    cerimonia.cerimonia_data_evento = DateTime.Parse(txt_data.Text);
                
                if (string.IsNullOrEmpty(cb_evento.SelectedValue.ToString()))
                {
                    MessageBox.Show("Selecione um tipo de Evento!");
                    return;
                }
                else 
                    cerimonia.cerimonia_tipo_evento_id = Convert.ToInt32(cb_evento.SelectedValue);
                
                if (string.IsNullOrEmpty(cb_pacote.SelectedValue.ToString()))
                {
                    MessageBox.Show("Selecione um Pacote!");
                    return;
                }
                else 
                    cerimonia.cerimonia_pacote_id = Convert.ToInt32(cb_pacote.SelectedValue);
                
                cerimonia.cerimonia_cidade_local = txt_cidade_local.Text;

                if(!string.IsNullOrEmpty(txt_convidados.Text))
                    cerimonia.cerimonia_total_convidados = int.Parse(txt_convidados.Text);
                
                cerimonia.cerimonia_horario_cerimonia = txt_horario_cerimonia.Text;
                cerimonia.cerimonia_inicio_festa = txt_horario_festa.Text;

                if (string.IsNullOrEmpty(txt_valor_das_parcelas.Text))
                {
                    MessageBox.Show("Informe um valor da Parcela!");
                    return;
                }
                else 
                    cerimonia.cerimonia_valor_parcelas = Decimal.Parse(txt_valor_das_parcelas.Text);

                if (string.IsNullOrEmpty(txt_Desconto.Text))
                {
                    cerimonia.cerimonia_desconto = 0;
                }
                else
                    cerimonia.cerimonia_desconto = Decimal.Parse(txt_Desconto.Text);

                if (string.IsNullOrEmpty(txt_parcelas.Text))
                {
                    MessageBox.Show("Selecione o número de Parcelas!");
                    return;
                }
                else
                    cerimonia.cerimonia_num_parcelas = int.Parse(txt_parcelas.Text);
                
                if (string.IsNullOrEmpty(txt_data_primeiro_vencimento.Text))
                {
                    MessageBox.Show("Selecione a data do primeiro vencimento!");
                    return;
                }
                else
                    cerimonia.cerimonia_data_primeiro_vencimento = DateTime.Parse(txt_data_primeiro_vencimento.Text);

                cerimonia.cerimonia_observacao = txt_observacao.Text;

                if (string.IsNullOrEmpty(lb_total.Content.ToString()))
                {
                    MessageBox.Show("Valor Total zerado!");
                    return;
                }else
                    cerimonia.cerimonia_valor_total = Decimal.Parse(lb_total.Content.ToString());

                servicesDBCerimonia.Salvar(cerimonia);
                MessageBox.Show(servicesDBCerimonia.Statusmessagem);
            }
            catch(Exception ex)
            {
                MessageBox.Show(servicesDBCerimonia.Statusmessagem);
            }
        }

        public void cb_pacote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int codigo = Convert.ToInt32(cb_pacote.SelectedValue);
            CarregarComboValor(codigo);
        }
        public void CarregarComboValor(int codigo)
        {
            total = 0;
            //codigo = Convert.ToInt32(cb_pacote.SelectedValue);

            ServicesDBPacote_Servico servicesDBPacote_Servico = new ServicesDBPacote_Servico();
            lv_pacote_servico.ItemsSource = servicesDBPacote_Servico.Listar_Pacote_Servicos_Por_Id(codigo);

            foreach (Pacote_Servicos o in lv_pacote_servico.Items)
            {
                total += o.pacote_servico_valor;
            }
            lb_total.Content = total;
            txt_Desconto.Text = "";
            txt_valor_prestacao_do_servico.Text = total.ToString();
        }
        
        private void txt_Desconto_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if ((!string.IsNullOrEmpty(txt_Desconto.Text)) && (!string.IsNullOrEmpty(total.ToString())) )
            {
                lb_total.Content = Convert.ToDouble(total)  - (Convert.ToDouble(txt_Desconto.Text));
            }
            else
            {
                lb_total.Content = total;
            }
        }
    }
}
