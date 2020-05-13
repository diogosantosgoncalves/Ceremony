using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceremony.Model
{
    public class Cerimonia
    {
        public int cerimonia_id { get; set; }
        public DateTime cerimonia_data_evento { get; set; }
        public string cerimonia_cidade_local { get; set; }
        public int cerimonia_total_convidados { get; set; }
        public string cerimonia_horario_cerimonia { get; set; }
        public string cerimonia_inicio_festa { get; set; }
        public int cerimonia_num_parcelas { get; set; }
        public decimal cerimonia_valor_parcelas { get; set; }
        public DateTime cerimonia_data_primeiro_vencimento { get; set; }
        public decimal cerimonia_valor_total { get; set; }
        public string cerimonia_observacao { get; set; }
        public int cerimonia_cliente_id { get; set; }
        public int cerimonia_tipo_evento_id { get; set; }
        public int cerimonia_pacote_id { get; set; }
        public decimal cerimonia_desconto { get; set; }
        public Cliente cliente { get; set; }
        public Tipo_Evento tipo_evento { get; set; }
        public Pacote pacot { get; set; }

        public Cerimonia(){
            cliente = new Cliente();
            tipo_evento = new Tipo_Evento();
            pacot = new Pacote();
        }

    }
}
