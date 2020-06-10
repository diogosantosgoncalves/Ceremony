using Ceremony.Dal;
using Ceremony.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
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
        List<Cerimonia_Produto> lista_original_cerimonia_produtos = new List<Cerimonia_Produto>();
        List<Cerimonia_Produto> cerimonia_Produtos = new List<Cerimonia_Produto>();
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
            //]lv_pacote_servico = null;
            ServicesDBTipo_Evento servicesDBTipo_Evento = new ServicesDBTipo_Evento();
            cb_evento.ItemsSource = servicesDBTipo_Evento.Listar_Tipo_Evento("");
            ServicesDBPacote servicesDBPacote = new ServicesDBPacote();
            cb_pacote.ItemsSource = servicesDBPacote.Listar_Pacote();

            codigo_evento = cerimonia.cerimonia_tipo_evento_id;
            codigo_pacote = cerimonia.cerimonia_pacote_id;
            cb_evento.SelectionChanged += new SelectionChangedEventHandler(comboBox1SelectionChanged);
            cb_pacote.SelectionChanged += new SelectionChangedEventHandler(cb_pacote_SelectionChanged);

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

            cb_evento.SelectedValue = cerimonia.cerimonia_tipo_evento_id;
            cb_pacote.SelectedValue = cerimonia.cerimonia_pacote_id;

            txt_Desconto.Text = cerimonia.cerimonia_desconto.ToString();
            lb_total.Content = double.Parse(cerimonia.cerimonia_valor_total.ToString());
            //lv_pacote_servico.Items.Clear();
            //var lista_ceri = new ObservableCollection<Cerimonia_Produto>(servicesDBCerimonia_Produto.Buscar_Cerimonia_Produto_Por_Codigo(cerimonia.cerimonia_id));
            //lv_pacote_servico.ItemsSource = servicesDBCerimonia_Produto.Buscar_Cerimonia_Produto_Por_Codigo(cerimonia.cerimonia_id);
            //lv_pacote_servico.ItemsSource = lista_ceri;
            lv_pacote_servico.ItemsSource = servicesDBCerimonia_Produto.Buscar_Cerimonia_Produto_Por_Codigo(cerimonia.cerimonia_id);
            lista_original_cerimonia_produtos = servicesDBCerimonia_Produto.Buscar_Cerimonia_Produto_Por_Codigo(cerimonia.cerimonia_id);
            cerimonia_Produtos = servicesDBCerimonia_Produto.Buscar_Cerimonia_Produto_Por_Codigo(cerimonia.cerimonia_id);
            bt_salvar.Content = "Alterar";
            
        }
        void comboBox1SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_evento.SelectedValue = codigo_evento;
            var currentSelectedIndex = cb_evento.SelectedValue;
        }
        void cb_pacote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_pacote.SelectedValue = codigo_pacote;
            var currentSelectedIndex2 = cb_pacote.SelectedValue;
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

                if (!string.IsNullOrEmpty(txt_convidados.Text))
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
                }
                else
                    cerimonia.cerimonia_valor_total = Decimal.Parse(lb_total.Content.ToString());

                if(bt_salvar.Content.ToString() == "Salvar")
                {
                    servicesDBCerimonia.Salvar(cerimonia);

                    if (servicesDBCerimonia.Cerimonia_Ultimo_Registro() != 0)
                    {
                        int codigo_cerimonia = servicesDBCerimonia.Cerimonia_Ultimo_Registro();
                        foreach (var item in lv_pacote_servico.Items.OfType<Cerimonia_Produto>())
                        {
                            Cerimonia_Produto cerimonia_Produto = new Cerimonia_Produto();
                            cerimonia_Produto.cerimonia_produto_servicos_id = item.pacote_servicos.pacote_servico_id;
                            cerimonia_Produto.cerimonia_produto_valor = item.pacote_servicos.pacote_servico_valor;
                            cerimonia_Produto.cerimonia__id = codigo_cerimonia;
                            servicesDBCerimonia_Produto.Salvar(cerimonia_Produto);
                        }
                    }
                }
                else
                {
                    servicesDBCerimonia.Alterar(cerimonia);

                    var difList = cerimonia_Produtos.Where(a => !lista_original_cerimonia_produtos.Any(a1 => a1.pacote_servicos.pacote_servico_id == a.pacote_servicos.pacote_servico_id)).Union(lista_original_cerimonia_produtos.Where(a => !cerimonia_Produtos.Any(a1 => a1.pacote_servicos.pacote_servico_id == a.pacote_servicos.pacote_servico_id)));

                    if (servicesDBCerimonia.Cerimonia_Ultimo_Registro() != 0)
                    {
                        int codigo_cerimonia = servicesDBCerimonia.Cerimonia_Ultimo_Registro();
                        foreach (var item in difList)
                        {
                            Cerimonia_Produto cerimonia_Produto = new Cerimonia_Produto();
                            cerimonia_Produto.cerimonia_produto_servicos_id = item.pacote_servicos.pacote_servico_id;
                            cerimonia_Produto.cerimonia_produto_valor = item.pacote_servicos.pacote_servico_valor;
                            cerimonia_Produto.cerimonia__id = codigo_cerimonia;
                            servicesDBCerimonia_Produto.Salvar(cerimonia_Produto);
                        }
                    }
                }
                MessageBox.Show(servicesDBCerimonia.Statusmessagem);
            }
            catch (Exception exxx)
            {
                MessageBox.Show(servicesDBCerimonia.Statusmessagem);
            }
        }

        //public void cb_pacote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    int codigo = Convert.ToInt32(cb_pacote.SelectedValue);
        //    CarregarComboValor(codigo);
        //    //cb_pacote.SelectedValue = codigo_pacote;
        //}
        public void CarregarComboValor(int codigo)
        {
            total = 0;

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
            if ((!string.IsNullOrEmpty(txt_Desconto.Text)) && (!string.IsNullOrEmpty(total.ToString())))
            {
                if (Convert.ToDouble(txt_Desconto.Text) == 0)
                {
                    Calcula_Total();
                }
                else
                {
                    //lb_total.Content = Convert.ToDouble(total) - (Convert.ToDouble(txt_Desconto.Text));
                    Calcula_Total();
                }
            }
            else
            {
                //lb_total.Content = total;
                Calcula_Total();
            }
        }

        private void bt_pesquisar_cliente_Click(object sender, RoutedEventArgs e)
        {
            Tela_Consulta_Cliente tela = new Tela_Consulta_Cliente();
            if (tela.ShowDialog() == true)
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
                Pacote_Servicos pacote_Servicos = new Pacote_Servicos();

                ///Tentar Usar Este
                Pacote_Servicos pacote_Servicos1 = tela.dg_ConsultaPacote_Servico.SelectedItem as Pacote_Servicos;
                /// Tentar Usar Este

                var cellInfo = tela.dg_ConsultaPacote_Servico.SelectedCells[1];
                var nome = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
                pacote_Servicos.pacote_servico_nome = nome;

                var cellInfo2 = tela.dg_ConsultaPacote_Servico.SelectedCells[0];
                var codigo = (cellInfo2.Column.GetCellContent(cellInfo2.Item) as TextBlock).Text;
                pacote_Servicos.pacote_servico_id = int.Parse(codigo);

                var cellInfo3 = tela.dg_ConsultaPacote_Servico.SelectedCells[2];
                var valor = (cellInfo3.Column.GetCellContent(cellInfo3.Item) as TextBlock).Text;
                pacote_Servicos.pacote_servico_valor = Double.Parse(valor);



                Cerimonia_Produto cerimonia_Produto = new Cerimonia_Produto();
                cerimonia_Produto.pacote_servicos = pacote_Servicos;
                if (cerimonia_Produtos.Exists(a => a.pacote_servicos.pacote_servico_id == cerimonia_Produto.pacote_servicos.pacote_servico_id))
                {
                    MessageBox.Show("Esse serviço já foi adicionado!");
                    return;
                }
                cerimonia_Produtos.Add(cerimonia_Produto);
                lv_pacote_servico.ItemsSource = cerimonia_Produtos;
                lv_pacote_servico.Items.Refresh();

                Calcula_Total();

            }
            else
            {
                MessageBox.Show("Selecione um Serviço!");
            }
        }
        private void bt_Remover_Produto_Click(object sender, RoutedEventArgs e)
        {
            Cerimonia_Produto obj = lv_pacote_servico.SelectedItem as Cerimonia_Produto;
            if (obj == null)
            {
                MessageBox.Show("Selecione um Serviço!");
            }
            else
            {
                string selectID = obj.pacote_servicos.pacote_servico_id.ToString();
                MessageBox.Show("The ID is: " + selectID);
                cerimonia_Produtos.RemoveAt(lv_pacote_servico.SelectedIndex);
                lv_pacote_servico.ItemsSource = cerimonia_Produtos;
                lv_pacote_servico.Items.Refresh();

                Calcula_Total();
            }



        }
        public void Calcula_Total()
        {
            total = 0;
            foreach (Cerimonia_Produto o in lv_pacote_servico.Items)
            {
                total += o.pacote_servicos.pacote_servico_valor;
            }
            if (!string.IsNullOrEmpty(txt_Desconto.Text))
                total = total - Convert.ToDouble(txt_Desconto.Text);

            lb_total.Content = total;
            txt_valor_prestacao_do_servico.Text = total.ToString();
        }

    }
}
