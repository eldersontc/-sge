using SGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class Cliente
    {
        public virtual int idCliente { get; set; }
        public virtual string razonSocial { get; set; }
        public virtual DocumentoIdentidad documentoIdentidad { get; set; }
        public virtual string nombreComercial { get; set; }
        public virtual DateTime? fechaUltimaCompra { get; set; }
        public virtual string telefono { get; set; }
        public virtual string correo { get; set; }
        public virtual Empleado vendedor { get; set; }
        public virtual bool activo { get; set; }
        public virtual List<ClienteDireccion> direcciones { get; set; }
        public virtual List<ClienteContacto> contactos { get; set; }
        public virtual List<int> idsDirecciones { get; set; }
        public virtual List<int> idsContactos { get; set; }
    }
}
