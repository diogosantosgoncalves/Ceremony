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
    public class ServicesDBCliente
    {
        Cliente cliente = new Cliente();
        
        Conexao con = new Conexao();
        SqlCommand sqlcommand = new SqlCommand();
        SqlDataReader sqldataReader = null;
        public string Statusmessagem { get; set; }
        public List<Cliente> Listar_Clientes()
        {
            try
            {
                List<Cliente> list = new List<Cliente>();
                sqlcommand.CommandText = "select * from Cliente";
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
                while (sqldataReader.Read())
                {
                    Cliente cliente = new Cliente();
                    
                    cliente.cli_id = int.Parse(sqldataReader["cli_id"].ToString());
                    cliente.cli_nome = sqldataReader["cli_nome"].ToString();
                    cliente.cli_nacionalidade = sqldataReader["cli_nacionalidade"].ToString();
                    cliente.cli_estado_civil = sqldataReader["cli_estavo_civil"].ToString();
                    cliente.cli_profissao = sqldataReader["cli_profissao"].ToString();
                    cliente.cli_rg = sqldataReader["cli_rg"].ToString();
                    cliente.cli_cpf = sqldataReader["cli_cpf"].ToString();
                    cliente.cli_endereco = sqldataReader["cli_endereco"].ToString();
                    cliente.cli_numero = sqldataReader["cli_numero"].ToString();
                    cliente.cli_complemento = sqldataReader["cli_complemento"].ToString();
                    cliente.cli_bairro = sqldataReader["cli_bairro"].ToString();
                    cliente.cli_cidade = sqldataReader["cli_cidade"].ToString();
                    cliente.cli_uf = sqldataReader["cli_uf"].ToString();
                    cliente.cli_cep = sqldataReader["cli_cep"].ToString();
                    cliente.cli_telefone_fixo = sqldataReader["cli_telefone_fixo"].ToString();
                    cliente.cli_celular1 = sqldataReader["cli_celular1"].ToString();
                    cliente.cli_celular2 = sqldataReader["cli_celular2"].ToString();
                    cliente.cli_telefone_trabalho = sqldataReader["cli_telefone_trabalho"].ToString();
                    cliente.cli_email = sqldataReader["cli_email"].ToString();

                    list.Add(cliente);
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
        public void Salvar(Cliente cliente)
        {
            try
            {

                sqlcommand.CommandText = "insert into Cliente(cli_nome,cli_nacionalidade,cli_estado_civil,cli_profissao,cli_rg,cli_cpf," +
                "cli_endereco,cli_numero,cli_complemento,cli_bairro,cli_cidade,cli_uf," +
                "cli_cep,cli_telefone_fixo,cli_celular1,cli_celular2,cli_telefone_trabalho,cli_email) "+
                "values (@cli_nome,@cli_nacionalidade,@cli_estado_civil,@cli_profissao,@cli_rg,@cli_cpf," +
                "@cli_endereco,@cli_numero,@cli_complemento,@cli_bairro,@cli_cidade,@cli_uf," +
                "@cli_cep,@cli_telefone_fixo,@cli_celular1,@cli_celular2,@cli_telefone_trabalho,@cli_email); ";

                sqlcommand.Parameters.AddWithValue("@cli_nome", cliente.cli_nome);
                sqlcommand.Parameters.AddWithValue("@cli_nacionalidade", cliente.cli_nacionalidade);
                sqlcommand.Parameters.AddWithValue("@cli_estado_civil", cliente.cli_estado_civil);
                sqlcommand.Parameters.AddWithValue("@cli_profissao", cliente.cli_profissao);
                sqlcommand.Parameters.AddWithValue("@cli_rg", cliente.cli_rg);
                sqlcommand.Parameters.AddWithValue("@cli_cpf", cliente.cli_cpf);
                sqlcommand.Parameters.AddWithValue("@cli_endereco", cliente.cli_endereco);
                sqlcommand.Parameters.AddWithValue("@cli_numero", cliente.cli_numero);
                sqlcommand.Parameters.AddWithValue("@cli_complemento", cliente.cli_complemento);
                sqlcommand.Parameters.AddWithValue("@cli_bairro", cliente.cli_bairro);
                sqlcommand.Parameters.AddWithValue("@cli_cidade", cliente.cli_cidade);
                sqlcommand.Parameters.AddWithValue("@cli_uf", cliente.cli_uf);
                sqlcommand.Parameters.AddWithValue("@cli_cep", cliente.cli_cep);
                sqlcommand.Parameters.AddWithValue("@cli_telefone_fixo", cliente.cli_telefone_fixo);
                sqlcommand.Parameters.AddWithValue("@cli_celular1", cliente.cli_celular1);
                sqlcommand.Parameters.AddWithValue("@cli_celular2", cliente.cli_celular2);
                sqlcommand.Parameters.AddWithValue("@cli_telefone_trabalho", cliente.cli_telefone_trabalho);
                sqlcommand.Parameters.AddWithValue("@cli_email", cliente.cli_email);

                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();
                Statusmessagem = "Cliente cadastrado com sucesso!";
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
        public List<Cliente> BuscarCliente(string nome)
        {
            try
            {
                List<Cliente> list = new List<Cliente>();
                SqlCommand cmd = new SqlCommand(String.Format("select * from Cliente where cli_nome like '%{0}%'", nome), con.conectar());
                //cmd.Parameters.AddWithValue("@nome", nome);
                //cmd.Connection = con.conectar();
                sqldataReader = cmd.ExecuteReader();

                while (sqldataReader.Read() == true)
                {
                    Cliente cliente = new Cliente();
                    cliente.cli_id = int.Parse(sqldataReader["cli_id"].ToString());
                    cliente.cli_nome = sqldataReader["cli_nome"].ToString();
                    cliente.cli_nacionalidade = sqldataReader["cli_nacionalidade"].ToString();
                    cliente.cli_estado_civil = sqldataReader["cli_estado_civil"].ToString();
                    cliente.cli_profissao = sqldataReader["cli_profissao"].ToString();
                    cliente.cli_rg = sqldataReader["cli_rg"].ToString();
                    cliente.cli_cpf = sqldataReader["cli_cpf"].ToString();
                    cliente.cli_endereco = sqldataReader["cli_endereco"].ToString();
                    cliente.cli_numero = sqldataReader["cli_numero"].ToString();
                    cliente.cli_complemento = sqldataReader["cli_complemento"].ToString();
                    cliente.cli_bairro = sqldataReader["cli_bairro"].ToString();
                    cliente.cli_cidade = sqldataReader["cli_cidade"].ToString();
                    cliente.cli_uf = sqldataReader["cli_uf"].ToString();
                    cliente.cli_cep = sqldataReader["cli_cep"].ToString();
                    cliente.cli_telefone_fixo = sqldataReader["cli_telefone_fixo"].ToString();
                    cliente.cli_celular1 = sqldataReader["cli_celular1"].ToString();
                    cliente.cli_celular2 = sqldataReader["cli_celular2"].ToString();
                    cliente.cli_telefone_trabalho = sqldataReader["cli_telefone_trabalho"].ToString();
                    cliente.cli_email = sqldataReader["cli_email"].ToString();

                    list.Add(cliente);
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
                sqlcommand.Parameters.Clear();
                con.desconectar();
                //sqldataReader.Close();
            }
        }

        public void AtivarProduto(string nome)
        {
            try
            {
                Cliente cliente = new Cliente();
                sqlcommand.CommandText = "update Produto set prod_inativo = 0 where prod_nome =  @produto";
                sqlcommand.Parameters.AddWithValue("@produto", nome);
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
            }
            catch (SqlException ex)
            {
                Statusmessagem = ex.Message;
            }
            finally
            {
                sqlcommand.Parameters.Clear();
                con.desconectar();
                sqldataReader.Close();
            }
        }

        public void Deletar(int codigousuario, string setor)
        {
            try
            {
                sqlcommand.CommandText = "delete su from SetorUsuario su inner join setor s on " +
                "su.setorusuario_setor_id = s.setor_id " +
                "where s.setor_nome = @setor and su.setorusuario_usu_id = @usuario";
                sqlcommand.Parameters.AddWithValue("@usuario", codigousuario);
                sqlcommand.Parameters.AddWithValue("@setor", setor);
                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlcommand.Parameters.Clear();
                con.desconectar();
            }
        }

        public void AlterarCliente(Cliente cliente)
        {
            try
            {
                sqlcommand.CommandText = "UPDATE Usuario SET cli_nome = '" + cliente.cli_nome + "', cli_nacionalidade = '" + cliente.cli_nacionalidade +
                ",cli_estavo_civil = '" + cliente.cli_estado_civil + "', cli_profissao = '" + cliente.cli_profissao + " cli_rg = '" + cliente.cli_rg +
                ",cli_cpf = '" + cliente.cli_cpf + "', cli_endereco = '" + cliente.cli_endereco + " cli_numero = '" + cliente.cli_numero +
                ",cli_complemento = '" + cliente.cli_complemento + "', cli_bairro = '" + cliente.cli_bairro + " cli_cidade = '" + cliente.cli_cidade +
                ",cli_uf = '" + cliente.cli_uf + "', cli_cep = '" + cliente.cli_cep + " cli_telefone_fixo = '" + cliente.cli_telefone_fixo +
                ",cli_celular1 = '" + cliente.cli_celular1 + "', cli_celular2 = '" + cliente.cli_celular2 + " cli_telefone_trabalho = '" + cliente.cli_telefone_trabalho +
                ",cli_email = '" + cliente.cli_email + "' WHERE cli_id = '" + cliente.cli_id;
                sqlcommand.Parameters.AddWithValue("@cli_id", cliente.cli_id);
                sqlcommand.Parameters.AddWithValue("@cli_nome", cliente.cli_nome);
                sqlcommand.Parameters.AddWithValue("@cli_nacionalidade", cliente.cli_nacionalidade);
                sqlcommand.Parameters.AddWithValue("@cli_estado_civil", cliente.cli_estado_civil);
                sqlcommand.Parameters.AddWithValue("@cli_profissao", cliente.cli_profissao);
                sqlcommand.Parameters.AddWithValue("@cli_rg", cliente.cli_rg);
                sqlcommand.Parameters.AddWithValue("@cli_cpf", cliente.cli_cpf);
                sqlcommand.Parameters.AddWithValue("@cli_endereco", cliente.cli_endereco);
                sqlcommand.Parameters.AddWithValue("@cli_numero", cliente.cli_numero);
                sqlcommand.Parameters.AddWithValue("@cli_complemento", cliente.cli_complemento);
                sqlcommand.Parameters.AddWithValue("@cli_bairro", cliente.cli_bairro);
                sqlcommand.Parameters.AddWithValue("@cli_cidade", cliente.cli_cidade);
                sqlcommand.Parameters.AddWithValue("@cli_uf", cliente.cli_uf);
                sqlcommand.Parameters.AddWithValue("@cli_cep", cliente.cli_cep);
                sqlcommand.Parameters.AddWithValue("@cli_telefone_fixo", cliente.cli_telefone_fixo);
                sqlcommand.Parameters.AddWithValue("@cli_celular1", cliente.cli_celular1);
                sqlcommand.Parameters.AddWithValue("@cli_celular2", cliente.cli_celular2);
                sqlcommand.Parameters.AddWithValue("@cli_telefone_trabalho", cliente.cli_telefone_trabalho);
                sqlcommand.Parameters.AddWithValue("@cli_email", cliente.cli_email);
                sqlcommand.CommandType = CommandType.Text;
                sqlcommand.Connection = con.conectar();
                sqlcommand.ExecuteNonQuery();

                Statusmessagem = "Cliente alterado com sucesso!";
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
        public Cliente Editar(int codigo)
        {
            Cliente cliente = new Cliente();
            sqlcommand.CommandText = "select * from Cliente where cli_id =  @id";
            sqlcommand.Parameters.AddWithValue("@id", codigo);
            sqlcommand.Connection = con.conectar();
            sqldataReader = sqlcommand.ExecuteReader();
            if (sqldataReader.Read())
            {
                cliente.cli_id = int.Parse(sqldataReader["cli_id"].ToString());
                cliente.cli_nome = sqldataReader["cli_nome"].ToString();
            }
            sqlcommand.Parameters.Clear();
            con.desconectar();
            sqldataReader.Close();
            return cliente;
        }
        public void Excluir(int id)
        {
            try
            {
                sqlcommand.CommandText = "delete Cliente where cli_id = @codigo";
                sqlcommand.Parameters.AddWithValue("@codigo", id);
                sqlcommand.Connection = con.conectar();
                sqldataReader = sqlcommand.ExecuteReader();
                sqlcommand.Parameters.Clear();
                con.desconectar();
                sqldataReader.Close();
                Statusmessagem = "Cliente Deletado!";
            }
            catch (SqlException ex)
            {
                Statusmessagem = ex.Message;
                throw new Exception(string.Format("Erro: {0} ", ex.Message));
            }

        }
    }
}
