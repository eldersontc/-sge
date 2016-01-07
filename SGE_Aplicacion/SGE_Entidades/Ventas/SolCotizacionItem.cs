using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class SolCotizacionItem
    {
        public virtual int idSolCotizacionItem { get; set; }
        public virtual int idSolCotizacionGrupo { get; set; }
        public virtual string titulo { get; set; }
        public virtual Servicio servicio { get; set; }
        public virtual Maquina maquina { get; set; }
        public virtual Material material { get; set; }
        public virtual bool flagMA { get; set; }
        public virtual bool flagMC { get; set; }
        public virtual bool flagTYR { get; set; }
        public virtual bool flagGRF { get; set; }
        public virtual bool flagMAT { get; set; }
        public virtual bool flagSRV { get; set; }
        public virtual bool flagFND { get; set; }
        public virtual decimal valXMA { get; set; }
        public virtual decimal valYMA { get; set; }
        public virtual decimal valXMC { get; set; }
        public virtual decimal valYMC { get; set; }
        public virtual decimal valTC { get; set; }
        public virtual decimal valRT { get; set; }
        public virtual decimal valFND { get; set; }
        public virtual string acabados { get; set; }
    }
}
