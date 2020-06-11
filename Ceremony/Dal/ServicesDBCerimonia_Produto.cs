using Ceremony.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceremony.Dal
{
    public class ServicesDBCerimonia_Produto
    {
        Cerimonia_Produto cerimonia = new Cerimonia_Produto();

        Conexao con = new Conexao();
        SqlCommand sqlcommand = new SqlCommand();
        SqlDataReader sqldataReader = null;
        public string Statusmessagem { get; set; }

        public List<Cerimonia_Produto> Listar_Cerimonia()
        {
            try
            {
                List<Cerimonia_Produto> list = new List<Cerimonia_Produto>();
                sqlcommand.CommandText = "select * from Cerimonia_produto";
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
                while (sqldataReader.Read())
                {
                    Cerimonia_Produto cerimonia_produto = new Cerimonia_Produto();

                    cerimonia_produto.cerimonia_produto_id = int.Parse(sqldataReader["cerimonia_produto_id"].ToString());
                    cerimonia_produto.cerimonia_produto_servicos_id = int.Parse(sqldataReader["cerimonia_produto_servicos_id"].ToString());
                    cerimonia_produto.cerimonia__id = int.Parse(sqldataReader["cerimonia__id"].ToString());
                    cerimonia_produto.cerimonia_produto_valor = sqldataReader.GetDouble(3);

                    list.Add(cerimonia_produto);
                }
                sqlcommand.Parameters.Clear();
                con.desconectar();
                sqldataReader.Close();
                return list;
            }
            catch (SqlException ex)
            {
                return null;
            }
            finally
            {
                sqlcommand.Parameters.Clear();
                con.desconectar();
            }

        }

        public void Salvar(Cerimonia_Produto cerimonia_produto)
        {
            try
            {
                sqlcommand.CommandText = "insert into Cerimonia_Produto(cerimonia__id,cerimonia_produto_pacote_servicos_id,cerimonia_produto_valor) " +
                "values (@cerimonia__id,@cerimonia_produto_pacote_servicos_id,@cerimonia_produto_valor);"; 

                sqlcommand.Parameters.AddWithValue("@cerimonia__id", cerimonia_produto.cerimonia__id);
                sqlcommand.Parameters.AddWithValue("@cerimonia_produto_pacote_servicos_id", cerimonia_produto.cerimonia_produto_servicos_id);
                sqlcommand.Parameters.AddWithValue("@cerimonia_produto_valor", cerimonia_produto.cerimonia_produto_valor);
                
                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();

                Statusmessagem = "Cerimonia_Produto Cadastrada com Sucesso!";

            }
            catch (SqlException ex)
            {
                Statusmessagem = ex.Message;
            }
            finally
            {
                sqlcommand.Parameters.Clear();
                con.desconectar();
            }
        }
        public List<Cerimonia_Produto> Buscar_Cerimonia_Produto_Por_Codigo(int codigo)
        {
            CultureInfo enUS = new CultureInfo("pt-BR");
            try
            {

                List<Cerimonia_Produto> lista_cerimonias = new List<Cerimonia_Produto>();
                sqlcommand.CommandText = "select * from cerimonia_produto cp " +
                    "inner join Cerimonia ce on cp.cerimonia__id = ce.cerimonia_id " +
                    "inner join Pacote_Servicos ps on cp.cerimonia_produto_pacote_servicos_id = ps.pacote_servico_id " +
                    " where cp.cerimonia__id = " + codigo;
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
        
                while (sqldataReader.Read() == true)
                {
                    Cerimonia_Produto cerimonia_produto = new Cerimonia_Produto();
                    Cerimonia cerimonia = new Cerimonia();

                    cerimonia_produto.cerimonia_produto_servicos_id = int.Parse(sqldataReader["cerimonia_produto_pacote_servicos_id"].ToString());

                    cerimonia.cerimonia_id = int.Parse(sqldataReader["cerimonia_id"].ToString());
                    cerimonia_produto.cerimonia = cerimonia;
                    
                    

                    Pacote_Servicos pacote_servicos = new Pacote_Servicos();
                    pacote_servicos.pacote_servico_id = int.Parse(sqldataReader["pacote_servico_id"].ToString());
                    pacote_servicos.pacote_servico_nome = sqldataReader["pacote_servico_nome"].ToString();
                    pacote_servicos.pacote_servico_valor = double.Parse(sqldataReader["pacote_servico_valor"].ToString());
                    cerimonia_produto.pacote_servicos = pacote_servicos;
                    lista_cerimonias.Add(cerimonia_produto);
                }

                return lista_cerimonias;
            }
            catch (SqlException ex)
            {
                Statusmessagem = ex.Message;
                return null;
            }
            finally
            {
                sqlcommand.Parameters.Clear();
                con.desconectar();
                sqldataReader.Close();
            }
        }
  
        public void Alterar(Cerimonia_Produto cerimonia)
        {
            try
            {
                //sqlcommand.CommandText = "UPDATE Usuario SET cli_nome = '" + cliente.cli_nome + "', cli_nacionalidade = '" + cliente.cli_nacionalidade +
                //",cli_estavo_civil = '" + cliente.cli_estado_civil + "', cli_profissao = '" + cliente.cli_profissao + " cli_rg = '" + cliente.cli_rg +
                //",cli_cpf = '" + cliente.cli_cpf + "', cli_endereco = '" + cliente.cli_endereco + " cli_numero = '" + cliente.cli_numero +
                //",cli_complemento = '" + cliente.cli_complemento + "', cli_bairro = '" + cliente.cli_bairro + " cli_cidade = '" + cliente.cli_cidade +
                //",cli_uf = '" + cliente.cli_uf + "', cli_cep = '" + cliente.cli_cep + " cli_telefone_fixo = '" + cliente.cli_telefone_fixo +
                //",cli_celular1 = '" + cliente.cli_celular1 + "', cli_celular2 = '" + cliente.cli_celular2 + " cli_telefone_trabalho = '" + cliente.cli_telefone_trabalho +
                //",cli_email = '" + cliente.cli_email + "' WHERE cli_id = '" + cliente.cli_id;
                /*
                sqlcommand.Parameters.AddWithValue("@buffet_data_evento", cerimonia.cerimonia_data_evento);
                sqlcommand.Parameters.AddWithValue("@buffet_cidade_local", cerimonia.cerimonia_cidade_local);
                sqlcommand.Parameters.AddWithValue("@buffet_total_convidados", cerimonia.cerimonia_total_convidados);
                sqlcommand.Parameters.AddWithValue("@buffet_horario_cerimonia", cerimonia.cerimonia_horario_cerimonia);
                sqlcommand.Parameters.AddWithValue("@buffet_inicio_festa", cerimonia.cerimonia_inicio_festa);
                sqlcommand.Parameters.AddWithValue("@buffet_num_parcelas", cerimonia.cerimonia_num_parcelas);
                sqlcommand.Parameters.AddWithValue("@buffet_valor_parcelas", cerimonia.cerimonia_valor_parcelas);
                sqlcommand.Parameters.AddWithValue("@buffet_data_primeiro_vencimento", cerimonia.cerimonia_data_primeiro_vencimento);
                sqlcommand.Parameters.AddWithValue("@buffet_valor_total", cerimonia.cerimonia_valor_total);
                */
                sqlcommand.CommandType = CommandType.Text;
                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();

                Statusmessagem = "Cerimonia alterado com sucesso!";
            }
            catch (SqlException ex)
            {
                Statusmessagem = ex.Message;
            }
            finally
            {
                sqlcommand.Parameters.Clear();
                con.desconectar();
            }

        }

        public Cerimonia_Produto Editar(int codigo)
        {
            try
            {
                Cerimonia_Produto cerimonia = new Cerimonia_Produto();
                sqlcommand.CommandText = "select * from Cerimonia_Produto where cerimonia_id =  @codigo";
                sqlcommand.Parameters.AddWithValue("@codigo", codigo);
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
                /*
                if (sqldataReader.Read())
                {
                    cerimonia.cerimonia_id = int.Parse(sqldataReader["cerimonia_id"].ToString());
                    cerimonia.cerimonia_cliente_id = int.Parse(sqldataReader["cerimonia_cliente_id"].ToString());
                    cerimonia.cerimonia_data_evento = DateTime.Parse(sqldataReader["cerimonia_data_evento"].ToString());
                    cerimonia.cerimonia_tipo_evento_id = int.Parse(sqldataReader["cerimonia_tipo_evento_id"].ToString());
                    cerimonia.cerimonia_pacote_id = int.Parse(sqldataReader["cerimonia_pacote_id"].ToString());
                    cerimonia.cerimonia_cidade_local = sqldataReader["cerimonia_cidade_local"].ToString();
                    cerimonia.cerimonia_total_convidados = int.Parse(sqldataReader["cerimonia_total_convidados"].ToString());
                    cerimonia.cerimonia_horario_cerimonia = sqldataReader["cerimonia_horario_cerimonia"].ToString();
                    cerimonia.cerimonia_num_parcelas = int.Parse(sqldataReader["cerimonia_num_parcelas"].ToString());
                    cerimonia.cerimonia_inicio_festa = sqldataReader["cerimonia_inicio_festa"].ToString();
                    cerimonia.cerimonia_valor_total = sqldataReader.GetDecimal(9);
                    cerimonia.cerimonia_desconto = sqldataReader.GetDecimal(14);
                    cerimonia.cerimonia_num_parcelas = int.Parse(sqldataReader["cerimonia_num_parcelas"].ToString());
                    cerimonia.cerimonia_valor_parcelas = sqldataReader.GetDecimal(7);
                    cerimonia.cerimonia_data_primeiro_vencimento = DateTime.Parse(sqldataReader["cerimonia_data_primeiro_vencimento"].ToString());
                    cerimonia.cerimonia_observacao = sqldataReader["cerimonia_observacao"].ToString();
                }
                */
                return cerimonia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlcommand.Parameters.Clear();
                con.desconectar();
                sqldataReader.Close();
            }
        }

        public void Excluir(int codigo_servico,int codigo_cerimonia)
        {
            try
            {
                sqlcommand.CommandText = "delete Cerimonia_Produto where cerimonia_produto_pacote_servicos_id = @codigo_serivo and cerimonia__id = @codigo_cerimonia";
                sqlcommand.Parameters.AddWithValue("@codigo_serivo", codigo_servico);
                sqlcommand.Parameters.AddWithValue("@codigo_cerimonia", codigo_cerimonia);
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();

                Statusmessagem = "Cerimonia Produto Deletado!";
            }
            catch (SqlException ex)
            {
                Statusmessagem = ex.Message;
                throw new Exception(string.Format("Erro: {0} ", ex.Message));
            }
            finally
            {
                sqlcommand.Parameters.Clear();
                con.desconectar();
                sqldataReader.Close();
            }

        }
    }
}
