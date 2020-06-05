using Ceremony.Dal;
using Ceremony.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Ceremony.View
{
    /// <summary>
    /// Interaction logic for Tela_Cerimonial.xaml
    /// </summary>
    public partial class Tela_Cerimonial : Window
    {
        double total = 0;
        int codigo_cliente;
        int codigo_evento = 0;
        int codigo_pacote = 0;
        ServicesDBCerimonia servicesDBCerimonia = new ServicesDBCerimonia();
        ServicesDBCliente servicesDBCliente = new ServicesDBCliente();
        ServicesDBCerimonia_Produto servicesDBCerimonia_Produto = new ServicesDBCerimonia_Produto();
        public Tela_Cerimonial()
        {
            InitializeComponent();
            ServicesDBTipo_Evento servicesDBTipo_Evento = new ServicesDBTipo_Evento();
            cb_evento.ItemsSource = servicesDBTipo_Evento.Listar_Tipo_Evento("");
            ServicesDBPacote servicesDBPacote = new ServicesDBPacote();
            cb_pacote.ItemsSource = servicesDBPacote.Listar_Pacote("");
        }
        public Tela_Cerimonial(Cerimonia cerimonia)
        {
            InitializeComponent();
            
            ServicesDBTipo_Evento servicesDBTipo_Evento = new ServicesDBTipo_Evento();
            cb_evento.ItemsSource = servicesDBTipo_Evento.Listar_Tipo_Evento("");
            ServicesDBPacote servicesDBPacote = new ServicesDBPacote();
            cb_pacote.ItemsSource = servicesDBPacote.Listar_Pacote("");

            codigo_evento = cerimonia.cerimonia_tipo_evento_id;
            codigo_pacote = cerimonia.cerimonia_pacote_id;
            cb_evento.SelectionChanged += new SelectionChangedEventHandler(comboBox1SelectionChanged);
            cb_pacote.SelectionChanged += new SelectionChangedEventHandler(cb_pacote_SelectionChanged);

            //cb_evento.SelectedIndex = 1;
            Cliente cli = servicesDBCliente.Editar(cerimonia.cerimonia_cliente_id);
            txt_cliente.Text = cli.cli_nome;
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
            //SelectInfo = InfoCombo[0];
            //cb_evento.SelectedIndex = cb_evento.Items.IndexOf(cerimonia.cerimonia_tipo_evento_id);
            Tipo_Evento obj = new Tipo_Evento();
            obj.tipo_evento_id = 1;
            //cb_evento.SelectedValue = obj.tipo_evento_id;
            //cb_evento.SelectedIndex = obj.tipo_evento_id;
            //cb_evento.SelectedValue = cerimonia.cerimonia_tipo_evento_id;
            //Convert.ToInt32(cerimonia.cerimonia_pacote_id);



            //CarregarComboValor(Convert.ToInt32(cerimonia.cerimonia_pacote_id));

            //cb_pacote.SelectedValue = codigo_pacote;
            //cb_pacote.SelectedValue = cerimonia.cerimonia_pacote_id;
            txt_Desconto.Text = cerimonia.cerimonia_desconto.ToString();

        }
        void comboBox1SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_evento.SelectedValue = codigo_evento;
            var currentSelectedIndex = cb_evento.SelectedValue;

            //cb_pacote.SelectedValue = codigo_pacote;
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
                    cerimonia.cerimonia_cliente_id = codigo_cliente;
                
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

                if(servicesDBCerimonia.Cerimonia_Ultimo_Registro() != 0)
                {
                    int codigo_cerimonia = servicesDBCerimonia.Cerimonia_Ultimo_Registro();
                    foreach (var item in lv_pacote_servico.Items.OfType<Pacote_Servicos>())
                    {
                        Cerimonia_Produto cerimonia_Produto = new Cerimonia_Produto();
                        cerimonia_Produto.cerimonia_produto_servicos_id = item.pacote_servico_id;
                        cerimonia_Produto.cerimonia_produto_valor = item.pacote_servico_valor;
                        cerimonia_Produto.cerimonia__id = codigo_cerimonia;
                        servicesDBCerimonia_Produto.Salvar(cerimonia_Produto);
                    }
                }

                MessageBox.Show(servicesDBCerimonia.Statusmessagem);
            }
            catch(Exception exxx)
            {
                MessageBox.Show(servicesDBCerimonia.Statusmessagem);
            }
        }

        public void cb_pacote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
            int codigo = Convert.ToInt32(cb_pacote.SelectedValue);
            CarregarComboValor(codigo);
            //cb_pacote.SelectedValue = codigo_pacote;
           
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

        private void bt_pesquisar_cliente_Click(object sender, RoutedEventArgs e)
        {
            Tela_Consulta_Cliente tela = new Tela_Consulta_Cliente();
            if(tela.ShowDialog() == true)
            {
                if (tela.dg_ConsultaCliente.SelectedItem == null)
                {
                    MessageBox.Show("Selecione um cliente!");
                    return;
                }
                //var a = (tela.dg_ConsultaCliente.SelectedCells[1].Column.GetCellContent(tela.dg_ConsultaCliente.SelectedCells[1].Item) as TextBlock).Text;
                //MessageBox.Show(a);
                var cellInfo = tela.dg_ConsultaCliente.SelectedCells[1];
                var nome = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
                txt_cliente.Text = nome;

                var cellInfo2 = tela.dg_ConsultaCliente.SelectedCells[0];
                var codigo = (cellInfo2.Column.GetCellContent(cellInfo2.Item) as TextBlock).Text;
                codigo_cliente = int.Parse(codigo);

            }
            else
            {
                MessageBox.Show("Selecione um Cliente!");
            }
        }

        private void bt_Adicionar_Produto_Click(object sender, RoutedEventArgs e)
        {
            Tela_Consulta_Pacote_Servicos tela = new Tela_Consulta_Pacote_Servicos();
            if (tela.ShowDialog() == true)
            {
                if (tela.dg_ConsultaPacote_Servico.SelectedItem == null)
                {
                    MessageBox.Show("Selecione um Serviço!");
                    return;
                }
                //var a = (tela.dg_ConsultaCliente.SelectedCells[1].Column.GetCellContent(tela.dg_ConsultaCliente.SelectedCells[1].Item) as TextBlock).Text;
                //MessageBox.Show(a);
                Pacote_Servicos pacote_Servicos = new Pacote_Servicos();

                var cellInfo = tela.dg_ConsultaPacote_Servico.SelectedCells[1];
                var nome = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
                pacote_Servicos.pacote_servico_nome = nome;

                var cellInfo2 = tela.dg_ConsultaPacote_Servico.SelectedCells[0];
                var codigo = (cellInfo2.Column.GetCellContent(cellInfo2.Item) as TextBlock).Text;
                pacote_Servicos.pacote_servico_id = int.Parse(codigo);

                var cellInfo3 = tela.dg_ConsultaPacote_Servico.SelectedCells[2];
                var valor = (cellInfo3.Column.GetCellContent(cellInfo3.Item) as TextBlock).Text;
                pacote_Servicos.pacote_servico_valor = Double.Parse(valor);

                lv_pacote_servico.Items.Add(pacote_Servicos);

                total = 0;
                foreach (Pacote_Servicos o in lv_pacote_servico.Items)
                {
                    total += o.pacote_servico_valor;
                }
                if (!string.IsNullOrEmpty(txt_Desconto.Text))
                    total = total - Convert.ToDouble(txt_Desconto.Text);
                lb_total.Content = total;
                //txt_Desconto.Text = "";
                txt_valor_prestacao_do_servico.Text = total.ToString();
            }
            else
            {
                MessageBox.Show("Selecione um Cliente!");
            }
        }
    }
    
}
