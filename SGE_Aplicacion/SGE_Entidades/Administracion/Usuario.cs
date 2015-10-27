using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class Usuario
    {
        public virtual int idUsuario { get; set; }
        public virtual string usuario { get; set; }
        public virtual string clave { get; set; }
        public virtual Perfil perfil { get; set; }
        public virtual bool activo { get; set; }
    }
}
