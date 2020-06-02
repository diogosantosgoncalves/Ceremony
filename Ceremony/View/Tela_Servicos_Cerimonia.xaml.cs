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
    /// Interaction logic for Tela_Servicos.xaml
    /// </summary>
    public partial class Tela_Servicos_Cerimonia : Window
    {
        ServicesDBPacote servicesDBPacote = new ServicesDBPacote();
        ServicesDBPacote_Servico servicesDBPacote_Servico = new ServicesDBPacote_Servico();
        public Tela_Servicos_Cerimonia()
        {
            InitializeComponent();
            cb_pacote.ItemsSource = servicesDBPacote.Listar_Pacote("");
            txt_nome.Focus();
        }

        private void bt_Salvar_Servicos(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txt_nome.Text) || string.IsNullOrEmpty(txt_valor.Text)){
                MessageBox.Show("Preencha o Nome ou o Valor!");
                return;
            }
            if(cb_pacote.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione um Pacote!");
                return;
            }
            else
            {
                try
                {
                    Pacote_Servicos pacote_servicos = new Pacote_Servicos();
                    pacote_servicos.pacote_id = Convert.ToInt32(cb_pacote.SelectedValue);
                    pacote_servicos.pacote_servico_nome = txt_nome.Text;
                    pacote_servicos.pacote_servico_valor = Convert.ToDouble(txt_valor.Text);
                    servicesDBPacote_Servico.Salvar(pacote_servicos);
                    MessageBox.Show(servicesDBPacote_Servico.Statusmessagem);

                    txt_nome.Text = "";
                    txt_valor.Text = "";
                   

                    txt_nome.Focus();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
