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
    public class ServicesDBBuffet_Servicos
    {
        Buffet_Servicos buffet_servicos = new Buffet_Servicos();

        Conexao con = new Conexao();
        SqlCommand sqlcommand = new SqlCommand();
        SqlDataReader sqldataReader = null;
        public string Statusmessagem { get; set; }
        public List<Buffet_Servicos> Listar_Buffet_Servicos()
        {
            try
            {
                List<Buffet_Servicos> list = new List<Buffet_Servicos>();
                sqlcommand.CommandText = "select * from Buffet_Servicos";
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
                while (sqldataReader.Read())
                {
                    Buffet_Servicos buffet_servicos = new Buffet_Servicos();

                    buffet_servicos.buffet_servicos_id = int.Parse(sqldataReader["buffet_servicos_id"].ToString());
                    buffet_servicos.buffet_servicos_nome = sqldataReader["buffet_servicos_nome"].ToString();
                    buffet_servicos.buffet_servicos_valor = sqldataReader.GetDecimal(2);

                    list.Add(buffet_servicos);
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
        public void Salvar(Buffet_Servicos buffet_servicos)
        {
            sqlcommand.CommandText = "insert into Buffet_Servicos(buffet_servicos_nome,buffet_servicos_valor) values (@nome,@valor)";
            sqlcommand.Parameters.AddWithValue("@nome", buffet_servicos.buffet_servicos_nome);
            sqlcommand.Parameters.AddWithValue("@valor", buffet_servicos.buffet_servicos_valor);

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
        public void Alterar(Buffet_Servicos buffet_servicos)
        {
            try
            {
                sqlcommand.CommandText = "UPDATE Pacote_Servicos SET buffet_servicos_nome = '" + buffet_servicos.buffet_servicos_nome +
                    "' pacote_servico_valor =" + buffet_servicos.buffet_servicos_valor + "', pacote_id = " + buffet_servicos.buffet_servicos_id +
               "' WHERE buffet_servicos__id = '" + buffet_servicos.buffet_servicos_id;

                sqlcommand.Parameters.AddWithValue("@nome", buffet_servicos.buffet_servicos_nome);
                sqlcommand.Parameters.AddWithValue("@valor", buffet_servicos.buffet_servicos_valor);
                sqlcommand.Parameters.AddWithValue("@id", buffet_servicos.buffet_servicos_id);
                sqlcommand.CommandType = CommandType.Text;
                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();

                Statusmessagem = "Buffet_Servicos alterado com sucesso!";
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
