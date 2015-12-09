using SGE.Entidades.Administracion;
using SGE.Entidades.Ventas;
using SGE.Negocios.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Ventas
{
    public partial class venLpMaterial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blLpMaterial blLpMaterial = new blLpMaterial(sesion);
                IList<LpMaterial> listas = blLpMaterial.ObtenerActivos();
                resultado = new { correcto = true, listas = listas };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object ObtenerActivos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blLpMaterial blLpMaterial = new blLpMaterial(sesion);
                IList<LpMaterial> listaes = blLpMaterial.ObtenerActivos();
                resultado = new { correcto = true, listaes = listaes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, LpMaterial lista)
        {
            object resultado = new { };
            try
            {
                blLpMaterial blLpMaterial = new blLpMaterial(sesion);
                blLpMaterial.Agregar(lista);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, LpMaterial lista)
        {
            object resultado = new { };
            try
            {
                blLpMaterial blLpMaterial = new blLpMaterial(sesion);
                blLpMaterial.Actualizar(lista);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idLpMaterial)
        {
            object resultado = new { };
            try
            {
                blLpMaterial blLpMaterial = new blLpMaterial(sesion);
                blLpMaterial.Eliminar(idLpMaterial);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }
    }
}