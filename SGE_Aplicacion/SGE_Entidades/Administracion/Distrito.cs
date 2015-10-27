using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class Distrito
    {
        public virtual int idDistrito { get; set; }
        public virtual string nombre { get; set; }
        public virtual Provincia provincia { get; set; }
    }
}
