﻿using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class PlantillaItem
    {
        public virtual int idPlantillaItem { get; set; }
        public virtual int idPlantillaGrupo { get; set; }
        public virtual string titulo { get; set; }
        public virtual Servicio servicio { get; set; }
        public virtual Material material { get; set; }
        public virtual bool conMdA { get; set; }
        public virtual bool conMdC { get; set; }
        public virtual bool conTyr { get; set; }
        public virtual bool conGrf { get; set; }
        public virtual bool conMat { get; set; }
        public virtual bool conSrv { get; set; }
        public virtual bool conFnd { get; set; }
    }
}
