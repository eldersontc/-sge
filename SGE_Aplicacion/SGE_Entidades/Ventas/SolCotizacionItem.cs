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
        public virtual bool conMdA { get; set; }
        public virtual bool conMdC { get; set; }
        public virtual bool conTyr { get; set; }
        public virtual bool conGrf { get; set; }
        public virtual bool conMat { get; set; }
        public virtual bool conSrv { get; set; }
        public virtual bool conFnd { get; set; }
        public virtual decimal xMa { get; set; }
        public virtual decimal yMa { get; set; }
        public virtual decimal xMc { get; set; }
        public virtual decimal yMc { get; set; }
        public virtual decimal tC { get; set; }
        public virtual decimal rC { get; set; }
        public virtual decimal fnd { get; set; }
        public virtual string acabados { get; set; }
    }
}
