using Ceremony.Dal;
using System;
using System.Collections.Generic;
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
        ServicesDBCerimonia servicesDBCerimonia = new ServicesDBCerimonia();
        ServicesDBCliente servicesDBCliente = new ServicesDBCliente();
        public RelatorioCerimonia()
        {
            InitializeComponent();
        }
        public void ReportViewer_Load(object sender, EventArgs e)
        {
            var dataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetCliente", servicesDBCliente.BuscarCliente("Diogo dos Santos")); // servicesDBCliente.Editar(1));
            ReportViewer.LocalReport.DataSources.Add(dataSource);

            var dataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetCerimonia", servicesDBCerimonia.Buscar_Cerimonia_Por_Nome("Diogo")); // servicesDBCerimonia.Editar(2));
            ReportViewer.LocalReport.DataSources.Add(dataSource1);

            ReportViewer.LocalReport.ReportEmbeddedResource = "Ceremony.Relatorios.RelatorioCerimonia.rdlc";

            ReportViewer.RefreshReport();
        }
    }
}
