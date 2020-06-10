using Ceremony.Dal;
using Ceremony.Relatorios;
using Ceremony.Relatorios.CeremonyDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Ceremony.View
{
    /// <summary>
    /// Interaction logic for RelatorioCerimonia.xaml
    /// </summary>
    public partial class RelatorioCerimonia : Window
    {
        int Codigo_Cerimonia = 0;
        ServicesDBCerimonia servicesDBCerimonia = new ServicesDBCerimonia();
        ServicesDBCerimonia_Produto servicesDBCerimonia_Produto = new ServicesDBCerimonia_Produto();
        ServicesDBCliente servicesDBCliente = new ServicesDBCliente();
        public RelatorioCerimonia(int codigo)
        {
            InitializeComponent();
            Codigo_Cerimonia = codigo;
        }
        public void ReportViewer_Load(object sender, EventArgs e)
        {
            var dataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetCerimonia", GetDataTable(Codigo_Cerimonia));
            ReportViewer.LocalReport.DataSources.Add(dataSource1);
            var dataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", GetDataTableCerimonia_Produto(Codigo_Cerimonia));
            ReportViewer.LocalReport.DataSources.Add(dataSource2);
            ReportViewer.LocalReport.ReportEmbeddedResource = "Ceremony.Relatorios.RelatorioCerimonia.rdlc";
            ReportViewer.RefreshReport();
        }

        public DataTable GetDataTable(int codigo)
        {
            DataTable dt = new DataTable();
            Conexao conexao = new Conexao();
            string sql = "SELECT Cliente.*, Cerimonia.*, Tipo_Evento.*, Pacote.*FROM Cerimonia INNER JOIN" +
                        " Cliente ON Cerimonia.cerimonia_cliente_id = Cliente.cli_id INNER JOIN" +
                       "  Pacote ON Cerimonia.cerimonia_pacote_id = Pacote.pacote_id INNER JOIN" +
                        " Tipo_Evento ON Cerimonia.cerimonia_tipo_evento_id = Tipo_Evento.tipo_evento_id " +
                        "where Cerimonia.cerimonia_id = @codigo";
            SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=StudentDetails;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(sql, conexao.conectar());
            cmd.Parameters.AddWithValue("@codigo", codigo);
            SqlDataReader sqldataReader = null;
            sqldataReader = cmd.ExecuteReader();
            if(sqldataReader.HasRows)
                dt.Load(sqldataReader);
            return dt;
        }

        public DataTable GetDataTableCerimonia_Produto(int codigo)
        {
            DataTable dt = new DataTable();
            Conexao conexao = new Conexao();
            string sql = "SELECT        Cerimonia_Produto.cerimonia_produto_id, Cerimonia_Produto.cerimonia__id, Cerimonia_Produto.cerimonia_produto_pacote_servicos_id, Cerimonia_Produto.cerimonia_produto_valor, Pacote_Servicos.pacote_servico_id, " + 
            " Pacote_Servicos.pacote_servico_nome, Pacote_Servicos.pacote_servico_valor, Cerimonia.* FROM Cerimonia_Produto INNER JOIN " +
            " Pacote_Servicos ON Cerimonia_Produto.cerimonia_produto_pacote_servicos_id = Pacote_Servicos.pacote_servico_id INNER JOIN " +
            " Cerimonia ON Cerimonia_Produto.cerimonia__id = Cerimonia.cerimonia_id where Cerimonia.cerimonia_id = @codigo";
            SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=StudentDetails;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(sql, conexao.conectar());
            cmd.Parameters.AddWithValue("@codigo", codigo);
            SqlDataReader sqldataReader = null;
            sqldataReader = cmd.ExecuteReader();
            if (sqldataReader.HasRows)
                dt.Load(sqldataReader);
            return dt;
        }

    }
}
