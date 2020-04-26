using Ceremony.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceremony.Dal
{
    public class ServicesDBTipo_Evento
    {
        Tipo_Evento tipo_evento = new Tipo_Evento();

        Conexao con = new Conexao();
        SqlCommand sqlcommand = new SqlCommand();
        SqlDataReader sqldataReader = null;
        public string Statusmessagem { get; set; }
        public List<Tipo_Evento> Listar_Tipo_Evento()
        {
            try
            {
                List<Tipo_Evento> list = new List<Tipo_Evento>();
                sqlcommand.CommandText = "select * from Tipo_Evento";
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
                while (sqldataReader.Read())
                {
                    Tipo_Evento tipo_evento = new Tipo_Evento();

                    tipo_evento.tipo_evento_id = int.Parse(sqldataReader["tipo_evento_id"].ToString());
                    tipo_evento.tipo_evento_nome = sqldataReader["tipo_evento_nome"].ToString();

                    list.Add(tipo_evento);
                }

                return list;
            }
            catch (SqlException ex)
            {
                return null;
            }
            finally
            {
                sqldataReader.Close();
                sqlcommand.Parameters.Clear();
                con.desconectar();
            }

        }
        public void Salvar(Tipo_Evento tipo_evento)
        {
            try
            {
                sqlcommand.CommandText = "insert into Tipo_Evento(tipo_evento_nome) values (@nome)";
                sqlcommand.Parameters.AddWithValue("@nome", tipo_evento.tipo_evento_nome);

                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();
                Statusmessagem = "Evento Cadastro com Sucesso!";
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
