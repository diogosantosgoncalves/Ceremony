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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ceremony.View
{
    /// <summary>
    /// Interaction logic for Cadastro_Cliente.xaml
    /// </summary>
    public partial class Cadastro_Cliente : Window
    {
        ServicesDBCliente servicesDBCliente = new ServicesDBCliente();
        public Cadastro_Cliente()
        {
            InitializeComponent();
            txt_nome.Focus();
        }
        public Cadastro_Cliente(Cliente cliente)
        {
            InitializeComponent();
            txt_id.Text = cliente.cli_id.ToString();
            txt_nome.Text = cliente.cli_nome;
            txt_nacionalidade.Text = cliente.cli_nacionalidade;
            txt_estado_civil.Text = cliente.cli_estado_civil;
            txt_profissao.Text = cliente.cli_profissao;
            txt_rg.Text = cliente.cli_rg;
            txt_cpf.Text = cliente.cli_cpf;
            txt_endereco.Text = cliente.cli_endereco;
            txt_numero.Text = cliente.cli_numero;
            txt_complemento.Text = cliente.cli_complemento;
            txt_bairro.Text = cliente.cli_bairro;
            txt_cidade.Text = cliente.cli_cidade;
            txt_uf.Text = cliente.cli_uf;
            txt_cep.Text = cliente.cli_cep;
            txt_fixo.Text = cliente.cli_telefone_fixo;
            txt_celular1.Text = cliente.cli_celular1;
            txt_celular2.Text = cliente.cli_celular2;
            txt_trabalho.Text = cliente.cli_telefone_trabalho;
            txt_email.Text = cliente.cli_email;
            bt_Salvar.Content = "Alterar";

        }

        private void Cadastrar_Cliente(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.cli_nome = txt_nome.Text;
                cliente.cli_nacionalidade = txt_nacionalidade.Text;
                cliente.cli_estado_civil = txt_estado_civil.Text;
                cliente.cli_profissao = txt_profissao.Text;
                cliente.cli_rg = txt_rg.Text;
                cliente.cli_cpf = txt_cpf.Text;
                cliente.cli_endereco = txt_endereco.Text;
                cliente.cli_numero = txt_numero.Text;
                cliente.cli_complemento = txt_complemento.Text;
                cliente.cli_bairro = txt_bairro.Text;
                cliente.cli_cidade = txt_cidade.Text;
                cliente.cli_uf = txt_uf.Text;
                cliente.cli_cep = txt_cep.Text;
                cliente.cli_telefone_fixo = txt_fixo.Text;
                cliente.cli_celular1 = txt_celular1.Text;
                cliente.cli_celular2 = txt_celular2.Text;
                cliente.cli_telefone_trabalho = txt_trabalho.Text;
                cliente.cli_email = txt_email.Text;

                if(bt_Salvar.Content.ToString() == "Salvar")
                {
                    servicesDBCliente.Salvar(cliente);
                    Limpar();
                }

                else
                {
                    cliente.cli_id =  int.Parse(txt_id.Text);
                    servicesDBCliente.AlterarCliente(cliente);
                    this.DialogResult = true;
                }

                MessageBox.Show(servicesDBCliente.Statusmessagem);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Limpar()
        {
            txt_nome.Text = "";
            txt_nacionalidade.Text = "";
            txt_estado_civil.Text = "";
            txt_profissao.Text = "";
            txt_rg.Text = "";
            txt_cpf.Text = "";
            txt_endereco.Text = "";
            txt_numero.Text = "";
            txt_complemento.Text = "";
            txt_bairro.Text = "";
            txt_cidade.Text = "";
            txt_uf.Text = "";
            txt_cep.Text = "";
            txt_fixo.Text = "";
            txt_celular1.Text = "";
            txt_celular2.Text = "";
            txt_trabalho.Text = "";
            txt_email.Text = "";
            txt_nome.Focus();
        }
    }
}
