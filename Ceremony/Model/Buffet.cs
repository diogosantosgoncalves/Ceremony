using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceremony.Model
{
    public class Buffet
    {
        public int buffet_id { get; set; }
        public DateTime buffet_data_evento { get; set; }
        public string buffet_cidade_local { get; set; }
        public int buffet_total_convidados { get; set; }
        public TimeZone buffet_horario_cerimonia { get; set; }
        public TimeZone buffet_inicio_festa { get; set; }
        public int buffet_num_parcelas { get; set; }
        public decimal buffet_valor_parcelas { get; set; }
        public DateTime buffet_data_primeiro_vencimento { get; set; }
        public decimal buffet_valor_total { get; set; }
    }
}
