using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class LpMaquinaItem
    {
        public virtual int idLpMaquinaItem { get; set; }
        public virtual int idLpMaquina { get; set; }
        public virtual Maquina maquina { get; set; }
        public virtual int factor { get; set; }
        public virtual List<LpMaquinaEscala> escalas { get; set; }
        public virtual List<int> idsEscalas { get; set; }
    }
}
