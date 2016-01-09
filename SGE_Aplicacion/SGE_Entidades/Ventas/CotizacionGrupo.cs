using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class CotizacionGrupo
    {
        public virtual int idCotizacionGrupo { get; set; }
        public virtual int idCotizacion { get; set; }
        public virtual string titulo { get; set; }
        public virtual int cantidad { get; set; }
        public virtual List<CotizacionItem> items { get; set; }
        public virtual List<int> idsItems { get; set; }

        public CotizacionGrupo()
        {
            items = new List<CotizacionItem>();
            idsItems = new List<int>();
        }
    }
}
