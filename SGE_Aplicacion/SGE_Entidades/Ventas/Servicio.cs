using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class Servicio
    {
        public virtual int idServicio { get; set; }
        public virtual string codigo { get; set; }
        public virtual string descripcion { get; set; }
        public virtual bool activo { get; set; }
        public virtual List<ServicioUnidad> unidades { get; set; }
        public virtual List<int> idsUnidades { get; set; }
    }
}
