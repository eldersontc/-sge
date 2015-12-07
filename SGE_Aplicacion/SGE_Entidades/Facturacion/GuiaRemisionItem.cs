using SGE.Entidades.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Facturacion
{
    public class GuiaRemisionItem
    {
        public virtual int idGuiaRemisionItem { get; set; }
        public virtual int idGuiaRemision { get; set; }
        public virtual OrdenTrabajo ordenTrabajo { get; set; }
        public virtual int cantidad { get; set; }
    }
}
