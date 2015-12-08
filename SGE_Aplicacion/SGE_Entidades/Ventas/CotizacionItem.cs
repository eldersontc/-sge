using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class CotizacionItem
    {
        public virtual int idCotizacionItem { get; set; }
        public virtual int idCotizacionGrupo { get; set; }
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
        public virtual decimal xFI { get; set; }
        public virtual decimal yFI { get; set; }
        public virtual decimal sX { get; set; }
        public virtual decimal sY { get; set; }
        public virtual int pliegos { get; set; }
        public virtual bool gp180 { get; set; }
        public virtual bool gi180 { get; set; }
        public virtual MetodoImpresion metodoImpresion { get; set; }
        public virtual int scntMat { get; set; }
        public virtual int cntDem { get; set; }
        public virtual int cntMat { get; set; }
        public virtual int cntPrd { get; set; }
        public virtual int cantidad { get; set; }
        public virtual int cntPs { get; set; }
        public virtual string observacion { get; set; }
        public virtual bool incPresupuesto { get; set; }
        public virtual bool prcPresupuesto { get; set; }
        public virtual decimal ttlMaq { get; set; }
        public virtual decimal ttlMat { get; set; }
        public virtual decimal ttlSrv { get; set; }
        public virtual decimal total { get; set; }
        public virtual List<CotizacionServicio> servicios { get; set; }
        public virtual List<int> idsServicios { get; set; }
    }
}
