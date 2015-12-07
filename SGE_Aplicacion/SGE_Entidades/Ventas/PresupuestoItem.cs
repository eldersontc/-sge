using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class PresupuestoItem
    {
        public virtual int idPresupuestoItem { get; set; }
        public virtual int idPresupuesto { get; set; }
        public virtual Cotizacion cotizacion { get; set; }
        public virtual decimal ttlCot { get; set; }
        public virtual decimal recargo { get; set; }
        public virtual decimal total { get; set; }
    }
}
