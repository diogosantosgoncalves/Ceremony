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
    public class ServicesDBCerimonia
    {
        Cerimonia cerimonia = new Cerimonia();

        Conexao con = new Conexao();
        SqlCommand sqlcommand = new SqlCommand();
        SqlDataReader sqldataReader = null;
        public string Statusmessagem { get; set; }
        public List<Cerimonia> Listar_Cerimonia()
        {
            try
            {
                List<Cerimonia> list = new List<Cerimonia>();
                sqlcommand.CommandText = "select * from Cerimonia";
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
                while (sqldataReader.Read())
                {
                    Cerimonia cerimonia = new Cerimonia();

                    cerimonia.cerimonia_id = int.Parse(sqldataReader["buffet_id"].ToString());
                    //buffet.buffet_data_evento = sqldataReader["buffet_data_evento"].ToString();
                    cerimonia.cerimonia_cidade_local = sqldataReader["buffet_cidade_local"].ToString();
                    cerimonia.cerimonia_total_convidados = int.Parse(sqldataReader["buffet_total_convidados"].ToString());
                    //buffet.buffet_horario_cerimonia = sqldataReader["buffet_horario_cerimonia"].ToString();
                    //buffet.buffet_inicio_festa = sqldataReader["buffet_inicio_festa"].ToString();
                    cerimonia.cerimonia_num_parcelas = int.Parse(sqldataReader["buffet_num_parcelas"].ToString());
                    //buffet.buffet_valor_parcelas = sqldataReader["buffet_valor_parcelas"].ToString();
                    //buffet.buffet_data_primeiro_vencimento = int.Parse(sqldataReader["buffet_data_primeiro_vencimento"].ToString());
                    //buffet.buffet_valor_total = sqldataReader["buffet_valor_total"].ToString();

                    list.Add(cerimonia);
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
        public void Salvar(Cerimonia cerimonia)
        {
            try
            {
                sqlcommand.CommandText = "insert into Cerimonia(cerimonia_data_evento,cerimonia_cidade_local,cerimonia_total_convidados," +
                "cerimonia_horario_cerimonia,cerimonia_inicio_festa,cerimonia_num_parcelas," +
                "cerimonia_valor_parcelas,cerimonia_data_primeiro_vencimento,cerimonia_valor_total,cerimonia_desconto,cerimonia_observacao," +
                "cerimonia_cliente_id,cerimonia_tipo_evento_id,cerimonia_pacote_id) " +

                "values (@cerimonia_data_evento,@cerimonia_cidade_local,@cerimonia_total_convidados," +
                "@cerimonia_horario_cerimonia,@cerimonia_inicio_festa,@cerimonia_num_parcelas," +
                "@cerimonia_valor_parcelas,@cerimonia_data_primeiro_vencimento,@cerimonia_valor_total,@cerimonia_desconto,@cerimonia_observacao," +
                "@cerimonia_cliente_id,@cerimonia_tipo_evento_id,@cerimonia_pacote_id); ";

            sqlcommand.Parameters.AddWithValue("@cerimonia_data_evento", cerimonia.cerimonia_data_evento);
            sqlcommand.Parameters.AddWithValue("@cerimonia_cidade_local", cerimonia.cerimonia_cidade_local);
            sqlcommand.Parameters.AddWithValue("@cerimonia_total_convidados", cerimonia.cerimonia_total_convidados);
            sqlcommand.Parameters.AddWithValue("@cerimonia_horario_cerimonia", cerimonia.cerimonia_horario_cerimonia);
            sqlcommand.Parameters.AddWithValue("@cerimonia_inicio_festa", cerimonia.cerimonia_inicio_festa);
            sqlcommand.Parameters.AddWithValue("@cerimonia_num_parcelas", cerimonia.cerimonia_num_parcelas);
            sqlcommand.Parameters.AddWithValue("@cerimonia_valor_parcelas", cerimonia.cerimonia_valor_parcelas);
            sqlcommand.Parameters.AddWithValue("@cerimonia_data_primeiro_vencimento", cerimonia.cerimonia_data_primeiro_vencimento);
            sqlcommand.Parameters.AddWithValue("@cerimonia_valor_total", cerimonia.cerimonia_valor_total);
            sqlcommand.Parameters.AddWithValue("@cerimonia_cliente_id", cerimonia.cerimonia_cliente_id);
            sqlcommand.Parameters.AddWithValue("@cerimonia_tipo_evento_id", cerimonia.cerimonia_tipo_evento_id);
            sqlcommand.Parameters.AddWithValue("@cerimonia_pacote_id", cerimonia.cerimonia_pacote_id);
            sqlcommand.Parameters.AddWithValue("@cerimonia_desconto", cerimonia.cerimonia_desconto);
            sqlcommand.Parameters.AddWithValue("@cerimonia_observacao", cerimonia.cerimonia_observacao);


            sqlcommand.Connection = con.conectar();
            sqlcommand.ExecuteNonQuery();

                Statusmessagem = "Cerimônia Cadastrada com Sucesso!";

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
        public List<Cerimonia> Buscar_Cerimonia_Por_Nome(string nome)
        {
            CultureInfo enUS = new CultureInfo("pt-BR");
            try
            {

                
                List<Cerimonia> lista_cerimonias = new List<Cerimonia>();
                sqlcommand.CommandText = "select * from Cerimonia ce inner join Cliente cl on " +
                    "cl.cli_id = ce.cerimonia_cliente_id inner join Tipo_Evento te on " +
                    "te.tipo_evento_id = ce.cerimonia_tipo_evento_id inner join Pacote pc on " +
                    "pc.pacote_id = ce.cerimonia_pacote_id where cl.cli_nome like '%" + nome + "%'";
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();

                while (sqldataReader.Read() == true)
                {
                    Cliente cli = new Cliente();
                    cli.cli_nome = sqldataReader["cli_nome"].ToString();
                    Tipo_Evento tipo_Evento = new Tipo_Evento();
                    tipo_Evento.tipo_evento_nome = sqldataReader["tipo_evento_nome"].ToString();
                    Pacote pac = new Pacote();
                    pac.pacote_id = int.Parse(sqldataReader["pacote_id"].ToString());
                    pac.pacote_nome = sqldataReader["pacote_nome"].ToString();
                    Cerimonia cerimonia = new Cerimonia();
                    cerimonia.cliente = cli;
                    cerimonia.pacot = pac;
                    cerimonia.tipo_evento = tipo_Evento;
                    cerimonia.cerimonia_id = int.Parse(sqldataReader["cerimonia_id"].ToString());
                    cerimonia.cerimonia_cliente_id = cli.cli_id;
                    cerimonia.cerimonia_pacote_id = pac.pacote_id;
                    cerimonia.cerimonia_tipo_evento_id = tipo_Evento.tipo_evento_id;
                    cerimonia.cliente.cli_nome = sqldataReader["cli_nome"].ToString();
                    //sqldataReader["inv_dtfechamento"].ToString().Length > 0 ? DateTime.Parse(sqldataReader["inv_dtfechamento"].ToString()) : DateTime.MinValue;
                    //cerimonia.cerimonia_data_evento = DateTime.Parse(sqldataReader["cerimonia_data_evento"].ToString());
                    cerimonia.cerimonia_cidade_local = sqldataReader["cerimonia_cidade_local"].ToString();
                    cerimonia.cerimonia_total_convidados = int.Parse(sqldataReader["cerimonia_total_convidados"].ToString());
                    cerimonia.cerimonia_horario_cerimonia = sqldataReader["cerimonia_horario_cerimonia"].ToString();
                    //buffet.buffet_inicio_festa = sqldataReader["buffet_inicio_festa"].ToString();
                    cerimonia.cerimonia_num_parcelas = int.Parse(sqldataReader["cerimonia_num_parcelas"].ToString());
                    //buffet.buffet_valor_parcelas = sqldataReader["buffet_valor_parcelas"].ToString();
                    //buffet.buffet_data_primeiro_vencimento = int.Parse(sqldataReader["buffet_data_primeiro_vencimento"].ToString());
                    //cerimonia.cerimonia_valor_total = sqldataReader["cerimonia_valor_total"].ToString();
                    cerimonia.cerimonia_valor_total = sqldataReader.GetDecimal(9);
                    lista_cerimonias.Add(cerimonia);
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



        public void Alterar(Cerimonia buffet)
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

                sqlcommand.Parameters.AddWithValue("@buffet_data_evento", cerimonia.cerimonia_data_evento);
                sqlcommand.Parameters.AddWithValue("@buffet_cidade_local", cerimonia.cerimonia_cidade_local);
                sqlcommand.Parameters.AddWithValue("@buffet_total_convidados", cerimonia.cerimonia_total_convidados);
                sqlcommand.Parameters.AddWithValue("@buffet_horario_cerimonia", cerimonia.cerimonia_horario_cerimonia);
                sqlcommand.Parameters.AddWithValue("@buffet_inicio_festa", cerimonia.cerimonia_inicio_festa);
                sqlcommand.Parameters.AddWithValue("@buffet_num_parcelas", cerimonia.cerimonia_num_parcelas);
                sqlcommand.Parameters.AddWithValue("@buffet_valor_parcelas", cerimonia.cerimonia_valor_parcelas);
                sqlcommand.Parameters.AddWithValue("@buffet_data_primeiro_vencimento", cerimonia.cerimonia_data_primeiro_vencimento);
                sqlcommand.Parameters.AddWithValue("@buffet_valor_total", cerimonia.cerimonia_valor_total);

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
        public Cerimonia Editar(int codigo)
        {
            try
            {
                Cerimonia cerimonia = new Cerimonia();
                sqlcommand.CommandText = "select * from Cerimonia where cerimonia_id =  @codigo";
                sqlcommand.Parameters.AddWithValue("@codigo", codigo);
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
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
    }
}
