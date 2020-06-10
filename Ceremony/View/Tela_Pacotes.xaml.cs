using Ceremony.Dal;
using Ceremony.Model;
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
    /// Interaction logic for Tela_Pacotes.xaml
    /// </summary>
    public partial class Tela_Pacotes : Window
    {
        ServicesDBPacote servicesDBPacote = new ServicesDBPacote();

        public Tela_Pacotes()
        {
            InitializeComponent();
        }
        public Tela_Pacotes(Pacote pacote)
        {
            InitializeComponent();
            txt_id.Text = pacote.pacote_id.ToString();
            txt_nome.Text = pacote.pacote_nome;
            bt_Salvar.Content = "Alterar";
        }

        private void bt_Salvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pacote pacote = new Pacote();
                pacote.pacote_nome = txt_nome.Text;
                if (bt_Salvar.Content.ToString() == "Salvar")
                {
                    
                    servicesDBPacote.Salvar(pacote);
                }
                else
                {
                    pacote.pacote_id = int.Parse(txt_id.Text);
                    servicesDBPacote.Alterar(pacote);
                    this.DialogResult = true;
                }
                MessageBox.Show(servicesDBPacote.Statusmessagem);
                txt_nome.Text = "";
                txt_nome.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
