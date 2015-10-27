using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class Menu
    {
        public virtual int idMenu { get; set; }
        public virtual int idMenuPadre { get; set; }
        public virtual string descripcion { get; set; }
        public virtual string url { get; set; }
        public virtual string icono { get; set; }
        public virtual List<Menu> subMenus { get; set; }
    }
}
