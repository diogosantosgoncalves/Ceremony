using Ceremony.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            sqlcommand.CommandText = "insert into Cliente(cli_nome,cli_nacionalidade,cli_estavo_civil,cli_profissao,cli_rg,cli_cpf," +
                "cli_endereco,cli_numero,cli_complemento,cli_bairro,cli_cidade,cli_uf," +
                "cli_cep,cli_telefone_fixo,cli_celular1,cli_celular2,cli_telefone_trabalho,cli_email) " +
                "values (@cli_id,@cli_nome,@cli_nacionalidade,@cli_estavo_civil,@cli_profissao,@cli_rg,@cli_cpf,@" +
                "@cli_endereco,@cli_numero,@cli_complemento,,@cli_bairro,@cli_cidade,@cli_uf," +
                "@cli_cep,@cli_telefone_fixo,@cli_celular1,@cli_celular2,@cli_telefone_trabalho,@cli_email)); ";

            sqlcommand.Parameters.AddWithValue("@buffet_data_evento", cerimonia.cerimonia_data_evento);
            sqlcommand.Parameters.AddWithValue("@buffet_cidade_local", cerimonia.cerimonia_cidade_local);
            sqlcommand.Parameters.AddWithValue("@buffet_total_convidados", cerimonia.cerimonia_total_convidados);
            sqlcommand.Parameters.AddWithValue("@buffet_horario_cerimonia", cerimonia.cerimonia_horario_cerimonia);
            sqlcommand.Parameters.AddWithValue("@buffet_inicio_festa", cerimonia.cerimonia_inicio_festa);
            sqlcommand.Parameters.AddWithValue("@buffet_num_parcelas", cerimonia.cerimonia_num_parcelas);
            sqlcommand.Parameters.AddWithValue("@buffet_valor_parcelas", cerimonia.cerimonia_valor_parcelas);
            sqlcommand.Parameters.AddWithValue("@buffet_data_primeiro_vencimento", cerimonia.cerimonia_data_primeiro_vencimento);
            sqlcommand.Parameters.AddWithValue("@buffet_valor_total", cerimonia.cerimonia_valor_total);

            try
            {
                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();
                sqlcommand.Parameters.Clear();
                con.desconectar();
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
        public Cerimonia Buscar_Cerimonia(string nome)
        {
            try
            {
                Cerimonia buffet = new Cerimonia();
                sqlcommand.CommandText = "select * from Cerimonia where cli_nome =  @nome";
                sqlcommand.Parameters.AddWithValue("@nome", nome);
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();

                if (sqldataReader.Read() == true)
                {
                    buffet.cerimonia_id = int.Parse(sqldataReader["buffet_id"].ToString());
                    //buffet.buffet_data_evento = sqldataReader["buffet_data_evento"].ToString();
                    buffet.cerimonia_cidade_local = sqldataReader["buffet_cidade_local"].ToString();
                    buffet.cerimonia_total_convidados = int.Parse(sqldataReader["buffet_total_convidados"].ToString());
                    //buffet.buffet_horario_cerimonia = sqldataReader["buffet_horario_cerimonia"].ToString();
                    //buffet.buffet_inicio_festa = sqldataReader["buffet_inicio_festa"].ToString();
                    buffet.cerimonia_num_parcelas = int.Parse(sqldataReader["buffet_num_parcelas"].ToString());
                    //buffet.buffet_valor_parcelas = sqldataReader["buffet_valor_parcelas"].ToString();
                    //buffet.buffet_data_primeiro_vencimento = int.Parse(sqldataReader["buffet_data_primeiro_vencimento"].ToString());
                    //buffet.buffet_valor_total = sqldataReader["buffet_valor_total"].ToString();

                    return buffet;
                }
                else
                {
                    return null;
                }
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
    }
}
