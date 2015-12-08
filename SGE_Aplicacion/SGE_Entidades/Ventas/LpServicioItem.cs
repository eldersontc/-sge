using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class LpServicioItem
    {
        public virtual int idLpServicioItem { get; set; }
        public virtual int idLpServicio { get; set; }
        public virtual Servicio servicio { get; set; }
        public virtual List<LpServicioUnidad> unidades { get; set; }
        public virtual List<int> idsUnidades { get; set; }
    }
}
