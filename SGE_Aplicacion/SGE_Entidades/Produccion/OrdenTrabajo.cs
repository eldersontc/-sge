using SGE.Entidades.Administracion;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Produccion
{
    public class OrdenTrabajo
    {
        public virtual int idOrdenTrabajo { get; set; }
        public virtual Numeracion numeracion { get; set; }
        public virtual string numero { get; set; }
        public virtual DateTime fechaCreacion { get; set; }
        public virtual string descripcion { get; set; }
        public virtual Cliente cliente { get; set; }
        public virtual LpMaterial lpMaterial { get; set; }
        public virtual LpServicio lpServicio { get; set; }
        public virtual LpMaquina lpMaquina { get; set; }
        public virtual Moneda moneda { get; set; }
        public virtual Empleado vendedor { get; set; }
        public virtual Empleado cotizador { get; set; }
        public virtual Empleado responsable { get; set; }
        public virtual ClienteContacto contacto { get; set; }
        public virtual Linea linea { get; set; }
        public virtual int cantidad { get; set; }
        public virtual string observacion { get; set; }
        public virtual Cotizacion cotizacion { get; set; }
        public virtual Presupuesto presupuesto { get; set; }
        public virtual DateTime fechaEntrega { get; set; }
        public virtual int prioridad { get; set; }
        public virtual int estado { get; set; }
        public virtual List<OrdenTrabajoGrupo> grupos { get; set; }
        public virtual List<int> idsGrupos { get; set; }
    }
}
