using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceremony.Model
{
    public class Cerimonia_Produto
    {
        public int cerimonia_produto_id { get; set; }
        public int cerimonia_produto_servicos_id { get; set; }
        public double cerimonia_produto_valor { get; set; }
        public int cerimonia__id { get; set; }

        public Cerimonia cerimonia { get; set; }
        public Pacote_Servicos pacote_servicos { get; set; }

        public Cerimonia_Produto()
        {
            cerimonia = new Cerimonia();
            pacote_servicos = new Pacote_Servicos();
        }
    }
}
