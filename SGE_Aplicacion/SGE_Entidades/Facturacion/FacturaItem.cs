using SGE.Entidades.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Facturacion
{
    public class FacturaItem
    {
        public virtual int idFacturaItem { get; set; }
        public virtual int idFactura { get; set; }
        public virtual OrdenTrabajo ordenTrabajo { get; set; }
        public virtual int cantidad { get; set; }
        public virtual decimal precio { get; set; }
        public virtual decimal total { get; set; }
    }
}
