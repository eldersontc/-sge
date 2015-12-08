using SGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Compras
{
    public class Proveedor
    {
        public virtual int idProveedor { get; set; }
        public virtual string razonSocial { get; set; }
        public virtual DocumentoIdentidad documentoIdentidad { get; set; }
        public virtual string nroDocumento { get; set; }
        public virtual string nombreComercial { get; set; }
        public virtual DateTime fechaUltimaCompra { get; set; }
        public virtual string telefono { get; set; }
        public virtual string correo { get; set; }
        public virtual bool activo { get; set; }
    }
}
