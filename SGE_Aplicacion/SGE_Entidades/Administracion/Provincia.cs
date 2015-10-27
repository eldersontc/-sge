using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class Provincia
    {
        public virtual int idProvincia { get; set; }
        public virtual string nombre { get; set; }
        public virtual Departamento departamento { get; set; }
    }
}
