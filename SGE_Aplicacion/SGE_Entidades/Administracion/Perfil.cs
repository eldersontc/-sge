using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class Perfil
    {
        public virtual int idPerfil { get; set; }
        public virtual string nombre { get; set; }
        public virtual bool activo { get; set; }
    }
}
