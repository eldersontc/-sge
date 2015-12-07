using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Produccion
{
    public class OrdenProduccionItem
    {
        public virtual int idOrdenProduccionItem { get; set; }
        public virtual int idOrdenProduccion { get; set; }
        public virtual OrdenTrabajo ordenTrabajo { get; set; }
    }
}
