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
    public class ServicesDBPacote
    {
        Pacote cliente = new Pacote();

        Conexao con = new Conexao();
        SqlCommand sqlcommand = new SqlCommand();
        SqlDataReader sqldataReader = null;
        public string Statusmessagem { get; set; }
        public List<Pacote> Listar_Pacote(string nome = "")
        {
            try
            {
                List<Pacote> list = new List<Pacote>();
                SqlCommand cmd = new SqlCommand(String.Format("select * from Pacote where pacote_nome like '%{0}%'", nome), con.conectar());
                sqldataReader = cmd.ExecuteReader();

                while (sqldataReader.Read())
                {
                    Pacote pacote = new Pacote();

                    pacote.pacote_id = int.Parse(sqldataReader["pacote_id"].ToString());
                    pacote.pacote_nome = sqldataReader["pacote_nome"].ToString();

                    list.Add(pacote);
                }

                return list;
            }
            catch (SqlException ex)
            {
                Statusmessagem = ex.Message;
                return null;
            }
            finally
            {
                sqldataReader.Close();
                sqlcommand.Parameters.Clear();
                con.desconectar();
            }

        }
        public void Salvar(Pacote pacote)
        {
            try
            {
                sqlcommand.CommandText = "insert into Pacote(pacote_nome) values (@nome)";
                sqlcommand.Parameters.AddWithValue("@nome", pacote.pacote_nome);

                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();
                Statusmessagem = "Pacote Cadastro com Sucesso!";
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
        public void Alterar(Pacote pacote)
        {
            try
            {
                sqlcommand.CommandText = "UPDATE Pacote SET pacote_nome = '" + pacote.pacote_nome +
               "' WHERE pacote_id = '" + pacote.pacote_id + "'";
                sqlcommand.Parameters.AddWithValue("@nome", pacote.pacote_nome);
                sqlcommand.CommandType = CommandType.Text;
                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();

                Statusmessagem = "Pacote alterado com sucesso!";
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
        public Pacote Editar(int codigo)
        {
            try
            {
                Pacote pacote = new Pacote();
                sqlcommand.CommandText = "select * from Pacote where Pacote_id =  @codigo";
                sqlcommand.Parameters.AddWithValue("@codigo", codigo);
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
                if (sqldataReader.Read())
                {
                    pacote.pacote_id = int.Parse(sqldataReader["pacote_id"].ToString());
                    pacote.pacote_nome = sqldataReader["pacote_nome"].ToString();
                }
                return pacote;
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
