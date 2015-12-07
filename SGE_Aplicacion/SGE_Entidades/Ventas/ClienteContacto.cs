using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class ClienteContacto
    {
        public virtual int idClienteContacto { get; set; }
        public virtual int idCliente { get; set; }
        public virtual string nombre { get; set; }
        public virtual string cargo { get; set; }
        public virtual string telefono { get; set; }
        public virtual string correo { get; set; }
    }
}
