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
    public partial class venPlantilla : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blPlantilla blPlantilla = new blPlantilla(sesion);
                IList<Plantilla> plantillas = blPlantilla.ObtenerTodos();
                resultado = new { correcto = true, plantillas = plantillas };
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
                blPlantilla blPlantilla = new blPlantilla(sesion);
                IList<Plantilla> plantillaes = blPlantilla.ObtenerActivos();
                resultado = new { correcto = true, plantillaes = plantillaes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Plantilla plantilla)
        {
            object resultado = new { };
            try
            {
                blPlantilla blPlantilla = new blPlantilla(sesion);
                blPlantilla.Agregar(plantilla);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Plantilla plantilla)
        {
            object resultado = new { };
            try
            {
                blPlantilla blPlantilla = new blPlantilla(sesion);
                blPlantilla.Actualizar(plantilla);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idPlantilla)
        {
            object resultado = new { };
            try
            {
                blPlantilla blPlantilla = new blPlantilla(sesion);
                blPlantilla.Eliminar(idPlantilla);
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