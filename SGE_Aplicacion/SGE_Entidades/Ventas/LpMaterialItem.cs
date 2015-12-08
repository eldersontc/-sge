using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class LpMaterialItem
    {
        public virtual int idLpMaterialItem { get; set; }
        public virtual int idLpMaterial { get; set; }
        public virtual Material material { get; set; }
        public virtual List<LpMaterialUnidad> unidades { get; set; }
        public virtual List<int> idsUnidades { get; set; }
    }
}
