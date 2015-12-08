using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class LpMaterialUnidad
    {
        public virtual int idLpMaterialUnidad { get; set; }
        public virtual int idLpMaterialItem { get; set; }
        public virtual Unidad unidad { get; set; }
        public virtual List<LpMaterialEscala> escalas { get; set; }
        public virtual List<int> idsEscalas { get; set; }
    }
}
