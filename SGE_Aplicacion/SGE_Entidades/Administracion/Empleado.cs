using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class Empleado
    {
        public virtual int idEmpleado { get; set; }
        public virtual string codigo { get; set; }
        public virtual string nombre { get; set; }
        public virtual string apellidoPaterno { get; set; }
        public virtual string apellidoMaterno { get; set; }
        public virtual DocumentoIdentidad documentoIdentidad { get; set; }
        public virtual string numeroDocumento { get; set; }
        public virtual bool activo { get; set; }
    }
}
