using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class LpServicioEscala
    {
        public virtual int idLpServicioEscala { get; set; }
        public virtual int idLpServicioUnidad { get; set; }
        public virtual int desde { get; set; }
        public virtual int hasta { get; set; }
        public virtual decimal precio { get; set; }
    }
}
