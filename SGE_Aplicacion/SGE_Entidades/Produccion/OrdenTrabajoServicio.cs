using SGE.Entidades.Inventarios;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Produccion
{
    public class OrdenTrabajoServicio
    {
        public virtual int idOrdenTrabajoServicio { get; set; }
        public virtual int idOrdenTrabajoItem { get; set; }
        public virtual Servicio servicio { get; set; }
        public virtual Unidad unidad { get; set; }
        public virtual decimal cantidad { get; set; }
    }
}
