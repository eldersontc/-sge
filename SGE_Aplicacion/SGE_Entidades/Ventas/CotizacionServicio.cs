using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class CotizacionServicio
    {
        public virtual int idCotizacionServicio { get; set; }
        public virtual int idCotizacionItem { get; set; }
        public virtual Servicio servicio { get; set; }
        public virtual Unidad unidad { get; set; }
        public virtual bool precioM { get; set; }
        public virtual decimal precio { get; set; }
        public virtual decimal cantidad { get; set; }
        public virtual decimal total { get; set; }
    }
}
