using SGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class ClienteDireccion
    {
        public virtual int idClienteDireccion { get; set; }
        public virtual int idCliente { get; set; }
        public virtual Departamento departamento { get; set; }
        public virtual Provincia provincia { get; set; }
        public virtual Distrito distrito { get; set; }
        public virtual string direccion { get; set; }
    }
}
