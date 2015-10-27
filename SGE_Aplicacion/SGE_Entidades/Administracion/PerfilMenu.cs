using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class PerfilMenu
    {
        public virtual int idPerfilMenu { get; set; }
        public virtual Perfil perfil { get; set; }
        public virtual Menu menu { get; set; }
    }
}
