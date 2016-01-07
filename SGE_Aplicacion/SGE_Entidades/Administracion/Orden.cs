using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class Orden
    {
        public virtual string columna { get; set; }
        public virtual bool asc { get; set; }
    }
}
