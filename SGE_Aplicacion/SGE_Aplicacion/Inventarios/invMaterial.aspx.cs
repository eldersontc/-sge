using SGE.Entidades.Administracion;
using SGE.Entidades.Inventarios;
using SGE.Negocios.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Inventarios
{
    public partial class invMaterial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blMaterial blMaterial = new blMaterial(sesion);
                IList<Material> materiales = blMaterial.ObtenerTodos();
                resultado = new { correcto = true, materiales = materiales };
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
                blMaterial blMaterial = new blMaterial(sesion);
                IList<Material> materiales = blMaterial.ObtenerActivos();
                resultado = new { correcto = true, materiales = materiales };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Material material)
        {
            object resultado = new { };
            try
            {
                blMaterial blMaterial = new blMaterial(sesion);
                blMaterial.Agregar(material);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Material material)
        {
            object resultado = new { };
            try
            {
                blMaterial blMaterial = new blMaterial(sesion);
                blMaterial.Actualizar(material);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idMaterial)
        {
            object resultado = new { };
            try
            {
                blMaterial blMaterial = new blMaterial(sesion);
                blMaterial.Eliminar(idMaterial);
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