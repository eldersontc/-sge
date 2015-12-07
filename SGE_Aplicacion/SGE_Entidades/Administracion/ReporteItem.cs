using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class ReporteItem
    {
        public virtual int idReporteItem { get; set; }
        public virtual int idReporte { get; set; }
        public virtual string nombre { get; set; }
        public virtual bool asignarId { get; set; }
        public virtual string valor { get; set; }
    }
}
