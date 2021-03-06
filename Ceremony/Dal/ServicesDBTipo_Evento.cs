﻿using Ceremony.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
        public List<Tipo_Evento> Listar_Tipo_Evento(string nome)
        {
            try
            {
                List<Tipo_Evento> list = new List<Tipo_Evento>();
                SqlCommand cmd = new SqlCommand(String.Format("select * from Tipo_Evento where tipo_evento_nome like '%{0}%'", nome), con.conectar());
                sqldataReader = cmd.ExecuteReader();
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
        public Tipo_Evento Editar(int codigo)
        {
            try
            {
                Tipo_Evento tipo_Evento = new Tipo_Evento();
                sqlcommand.CommandText = "select * from Tipo_Evento where tipo_evento_id =  @codigo";
                sqlcommand.Parameters.AddWithValue("@codigo", codigo);
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
                if (sqldataReader.Read())
                {
                    tipo_Evento.tipo_evento_id = int.Parse(sqldataReader["tipo_evento_id"].ToString());
                    tipo_Evento.tipo_evento_nome = sqldataReader["tipo_Evento_nome"].ToString();
                }
                return tipo_Evento;
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
        public void Alterar(Tipo_Evento tipo_Evento)
        {
            try
            {
                sqlcommand.CommandText = "UPDATE Tipo_Evento SET tipo_evento_nome = '" + tipo_Evento.tipo_evento_nome +
               "' WHERE tipo_evento_id = " + tipo_Evento.tipo_evento_id;
                sqlcommand.Parameters.AddWithValue("@nome", tipo_Evento.tipo_evento_nome);
                sqlcommand.CommandType = CommandType.Text;
                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();

                Statusmessagem = "Tipo de Evento alterado com sucesso!";
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
