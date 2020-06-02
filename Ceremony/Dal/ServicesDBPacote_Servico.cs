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
    public class ServicesDBPacote_Servico
    {
        Pacote_Servicos pacote_servicos = new Pacote_Servicos();

        Conexao con = new Conexao();
        SqlCommand sqlcommand = new SqlCommand();
        SqlDataReader sqldataReader = null;
        public string Statusmessagem { get; set; }
        public List<Pacote_Servicos> Listar_Pacote_Servicos(string nome)
        {
            try
            {
                List<Pacote_Servicos> list = new List<Pacote_Servicos>();
                SqlCommand cmd = new SqlCommand(String.Format("select * from Pacote_Servicos where pacote_servico_nome like '%{0}%'", nome), con.conectar());
                sqldataReader = cmd.ExecuteReader();
                while (sqldataReader.Read())
                {
                    Pacote_Servicos pacote_servicos = new Pacote_Servicos();

                    pacote_servicos.pacote_servico_id = int.Parse(sqldataReader["pacote_servico_id"].ToString());
                    pacote_servicos.pacote_servico_nome = sqldataReader["pacote_servico_nome"].ToString();
                    pacote_servicos.pacote_servico_valor = double.Parse(sqldataReader["pacote_servico_valor"].ToString()); // sqldataReader.GetDouble(2);
                    pacote_servicos.pacote_id = int.Parse(sqldataReader["pacote_id"].ToString());

                    list.Add(pacote_servicos);
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
        public List<Pacote_Servicos> Listar_Pacote_Servicos_Por_Id(int pacote)
        {
            try
            {
                List<Pacote_Servicos> list = new List<Pacote_Servicos>();
                sqlcommand.CommandText = "select * from Pacote_Servicos where pacote_id = @pacote";
                sqlcommand.Parameters.AddWithValue("@pacote", pacote);
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
                while (sqldataReader.Read())
                {
                    Pacote_Servicos pacote_servicos = new Pacote_Servicos();

                    pacote_servicos.pacote_servico_id = int.Parse(sqldataReader["pacote_servico_id"].ToString());
                    pacote_servicos.pacote_servico_nome = sqldataReader["pacote_servico_nome"].ToString();
                    pacote_servicos.pacote_servico_valor = double.Parse(sqldataReader["pacote_servico_valor"].ToString()); // sqldataReader.GetDouble(2);
                    pacote_servicos.pacote_id = int.Parse(sqldataReader["pacote_id"].ToString());

                    list.Add(pacote_servicos);
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
        public void Salvar(Pacote_Servicos pacote_servicos)
        {
            try
            {
                sqlcommand.CommandText = "insert into Pacote_Servicos(pacote_servico_nome,pacote_servico_valor,pacote_id) values (@nome,@valor,@pacote)";
                sqlcommand.Parameters.AddWithValue("@nome", pacote_servicos.pacote_servico_nome);
                sqlcommand.Parameters.AddWithValue("@valor", pacote_servicos.pacote_servico_valor);
                sqlcommand.Parameters.AddWithValue("@pacote", pacote_servicos.pacote_id);

                Statusmessagem = "Serviço Cadastro com Sucesso!";

                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();
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
        public void Alterar(Pacote_Servicos pacote_servicos)
        {
            try
            {
                sqlcommand.CommandText = "UPDATE Pacote_Servicos SET pacote_servico_nome = '" + pacote_servicos.pacote_servico_nome +
                    "' pacote_servico_valor = " + pacote_servicos.pacote_servico_valor + " WHERE pacote_servico_id = '" + pacote_servicos.pacote_servico_id;

                sqlcommand.Parameters.AddWithValue("@nome", pacote_servicos.pacote_servico_nome);
                sqlcommand.Parameters.AddWithValue("@valor", pacote_servicos.pacote_servico_valor);
                sqlcommand.Parameters.AddWithValue("@id", pacote_servicos.pacote_id);
                sqlcommand.CommandType = CommandType.Text;
                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();

                Statusmessagem = "Pacote_Servicos alterado com sucesso!";
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
