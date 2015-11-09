using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Inventarios
{
    public class Unidad
    {
        public virtual int idUnidad { get; set; }
        public virtual string descripcion { get; set; }
        public virtual string abreviacion { get; set; }
        public virtual bool activo { get; set; }
    }
}
