﻿using SGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class SolCotizacion
    {
        public virtual int idSolCotizacion { get; set; }
        public virtual Numeracion numeracion { get; set; }
        public virtual string numero { get; set; }
        public virtual DateTime fechaCreacion { get; set; }
        public virtual string descripcion { get; set; }
        public virtual Cliente cliente { get; set; }
        public virtual Linea linea { get; set; }
        public virtual LpMaterial lpMaterial { get; set; }
        public virtual LpServicio lpServicio { get; set; }
        public virtual LpMaquina lpMaquina { get; set; }
        public virtual Moneda moneda { get; set; }
        public virtual Empleado vendedor { get; set; }
        public virtual FormaPago formaPago { get; set; }
        public virtual ClienteContacto contacto { get; set; }
        public virtual string observacion { get; set; }
        public virtual int estado { get; set; }
        public virtual List<SolCotizacionGrupo> grupos { get; set; }
    }
}