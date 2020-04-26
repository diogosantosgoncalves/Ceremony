﻿using Ceremony.Dal;
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
    /// Interaction logic for Tela_Tipo_Evento.xaml
    /// </summary>
    public partial class Tela_Tipo_Evento : Window
    {
        ServicesDBTipo_Evento servicesDBTipo_Evento = new ServicesDBTipo_Evento();
        public Tela_Tipo_Evento()
        {
            InitializeComponent();
            txt_nome.Focus();
        }

        private void bt_Salvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tipo_Evento pacote = new Tipo_Evento();
                pacote.tipo_evento_nome = txt_nome.Text;
                servicesDBTipo_Evento.Salvar(pacote);
                MessageBox.Show(servicesDBTipo_Evento.Statusmessagem);
                txt_nome.Text = "";
                txt_nome.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
