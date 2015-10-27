using SGE.Entidades.Administracion;
using SGE.Negocios.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Administracion
{
    public partial class admDocumentoIdentidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos()
        {
            object resultado = new { };
            try
            {
                blDocumentoIdentidad blDocumentoIdentidad = new blDocumentoIdentidad();
                IList<DocumentoIdentidad> documentosIdentidad = blDocumentoIdentidad.ObtenerTodos();
                resultado = new { correcto = true, documentosIdentidad = documentosIdentidad };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(DocumentoIdentidad documentoIdentidad)
        {
            object resultado = new { };
            try
            {
                blDocumentoIdentidad blDocumentoIdentidad = new blDocumentoIdentidad();
                blDocumentoIdentidad.Agregar(documentoIdentidad);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(DocumentoIdentidad documentoIdentidad)
        {
            object resultado = new { };
            try
            {
                blDocumentoIdentidad blDocumentoIdentidad = new blDocumentoIdentidad();
                blDocumentoIdentidad.Actualizar(documentoIdentidad);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(int idDocumentoIdentidad)
        {
            object resultado = new { };
            try
            {
                blDocumentoIdentidad blDocumentoIdentidad = new blDocumentoIdentidad();
                blDocumentoIdentidad.Eliminar(idDocumentoIdentidad);
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