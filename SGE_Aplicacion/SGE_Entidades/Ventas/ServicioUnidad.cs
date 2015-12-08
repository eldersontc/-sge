using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class ServicioUnidad
    {
        public virtual int idServicioUnidad { get; set; }
        public virtual int idServicio { get; set; }
        public virtual Unidad unidad { get; set; }
        public virtual int factor { get; set; }
    }
}
