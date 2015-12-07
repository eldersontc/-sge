using SGE.Entidades.Administracion;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Facturacion
{
    public class GuiaRemision
    {
        public virtual int idGuiaRemision { get; set; }
        public virtual Numeracion numeracion { get; set; }
        public virtual string numero { get; set; }
        public virtual DateTime fechaCreacion { get; set; }
        public virtual Cliente cliente { get; set; }
        public virtual Empleado responsable { get; set; }
        public virtual Empleado chofer { get; set; }
        public virtual string licencia { get; set; }
        public virtual string placa { get; set; }
        public virtual ClienteContacto contacto { get; set; }
        public virtual Departamento departamento { get; set; }
        public virtual Provincia provincia { get; set; }
        public virtual Distrito distrito { get; set; }
        public virtual string direccion { get; set; }
        public virtual string observacion { get; set; }
        public virtual List<GuiaRemisionItem> items { get; set; }
    }
}
