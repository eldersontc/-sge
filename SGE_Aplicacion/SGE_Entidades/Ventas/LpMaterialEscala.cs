using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class LpMaterialEscala
    {
        public virtual int idLpMaterialEscala { get; set; }
        public virtual int idLpMaterialUnidad { get; set; }
        public virtual int desde { get; set; }
        public virtual int hasta { get; set; }
        public virtual int precio { get; set; }
    }
}
