using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class LpServicioUnidad
    {
        public virtual int idLpServicioUnidad { get; set; }
        public virtual int idLpServicioItem { get; set; }
        public virtual Unidad unidad { get; set; }
        public virtual List<LpServicioEscala> escalas { get; set; }
        public virtual List<int> idsEscalas { get; set; }
    }
}
